using final_project.Pages;
using Microsoft.Playwright;

namespace final_project
{
    public class UITestFixture
    {
        public IPage page { get; private set; }
        private IBrowser browser;
        public IBrowserContext context;
        public string email = "testemailfor126@gmail.com";
        public string password = "1234test";

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var playwrightDriver = await Playwright.CreateAsync();
            browser = await playwrightDriver.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });
            context = await browser.NewContextAsync();
            page = await context.NewPageAsync();

            var homePage = new HomePage(page);
            var createAccountBody = context.APIRequest.CreateFormData();
            createAccountBody.Append("name", "testName");
            createAccountBody.Append("email", email);
            createAccountBody.Append("password", password);
            createAccountBody.Append("title", "Mr");
            createAccountBody.Append("birth_date", "20");
            createAccountBody.Append("birth_month", "June");
            createAccountBody.Append("birth_year", "1993");
            createAccountBody.Append("firstname", "testName");
            createAccountBody.Append("lastname", "testLastName");
            createAccountBody.Append("company", "testCompany");
            createAccountBody.Append("address1", "testAddress1");
            createAccountBody.Append("address2", "testAddress2");
            createAccountBody.Append("country", "Canada");
            createAccountBody.Append("zipcode", "123456");
            createAccountBody.Append("state", "testState");
            createAccountBody.Append("city", "testCity");
            createAccountBody.Append("mobile_number", "1234567890");
            await homePage.Open();
            await homePage.AcceptDataPolicy();
            await page.APIRequest.PostAsync("https://automationexercise.com/api/createAccount",
                new() { Form = createAccountBody });
        }

        [SetUp]
        public async Task Setup()
        {
            var playwrightDriver = await Playwright.CreateAsync();
            browser = await playwrightDriver.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            string subPath = "../../../playwright/auth";
            string filePath = "../../../playwright/auth/state.json";

            if(!Directory.Exists(subPath))
            {
                Directory.CreateDirectory(subPath);
            }

            if(!File.Exists(filePath))
            {
                File.AppendAllText(filePath, "{}");
            }

            context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = 1920,
                    Height = 1080
                },
                StorageStatePath = "../../../playwright/auth/state.json"
            });

            await context.Tracing.StartAsync(new()
            {
                Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            page = await context.NewPageAsync();
            var homePage = new HomePage(page);
            await homePage.Open();
            if(await homePage.GetLoginPageLocator().IsVisibleAsync()) 
            {
                await homePage.AcceptDataPolicy();
                await homePage.Login(email, password);
                
                await context.StorageStateAsync(new()
                {
                    Path = "../../../playwright/auth/state.json"
                });
            }
            
        }

        [OneTimeTearDown]
        public async Task DeleteAccountAndTeardown()
        {
            var deleteAccountBody = context.APIRequest.CreateFormData();
            deleteAccountBody.Append("email", email);
            deleteAccountBody.Append("password", password);
            await page.APIRequest.DeleteAsync("https://automationexercise.com/api/deleteAccount",
                new() { Form = deleteAccountBody });
            await page.CloseAsync();
            await browser.CloseAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await context.Tracing.StopAsync(new()
            {
                Path = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            )
            });

            await page.CloseAsync();
            await browser.CloseAsync();
        }
    }
}
