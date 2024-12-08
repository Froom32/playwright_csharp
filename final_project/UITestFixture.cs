using final_project.Pages;
using Microsoft.Playwright;

namespace final_project
{
    public class UITestFixture
    {
        public IPage page { get; private set; }
        private IBrowser browser;
        public IBrowserContext context;

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
                await homePage.Login("bilikmike@gmail.com", "@vvwrER6FT5PgU9");
                
                await context.StorageStateAsync(new()
                {
                    Path = "../../../playwright/auth/state.json"
                });
            }
            
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
