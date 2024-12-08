using Microsoft.Playwright;
using final_project.Pages;

namespace final_project.Tests
{
    [TestFixture]
    public class CreateAndDeleteTests
    {
        public IPage page { get; private set; }
        private IBrowser browser;
        public IBrowserContext context;

        public string email;

        public string password;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var playwrightDriver = await Playwright.CreateAsync();
            browser = await playwrightDriver.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = 1920,
                    Height = 1080
                }
            });
            page = await context.NewPageAsync();
        }

        [Test]
        public async Task CreateAccountTest()
        {
            var homePage = new HomePage(page);
            var createAccountBody = context.APIRequest.CreateFormData();
            email = "testemailfor126@gmail.com";
            password = "1234test";
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

            // Act
            await homePage.Open();
            await homePage.AcceptDataPolicy();
            await page.APIRequest.PostAsync("https://automationexercise.com/api/createAccount",
                new() { Form = createAccountBody });
            await homePage.Login(email, password);

            //Assert
            await Assertions.Expect(homePage.GetDeleteAccountPageLocator()).ToBeVisibleAsync();
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
    }
    
}
