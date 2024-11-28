using Microsoft.Playwright;

namespace LambdatestEcom
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

        [TearDown]
        public async Task Teardown()
        {
            await page.CloseAsync();
            await browser.CloseAsync();
        }
    }
}
