using LambdatestEcom;
using Microsoft.Playwright;

namespace final_project.Tests
{
    [TestFixture]
    public class SearchTests : UITestFixture
    {
        [Test]
        public async Task SearchProductTest()
        {
            await page.GotoAsync("https://automationexercise.com/");
            await page.GetByLabel("Consent", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Products" }).ClickAsync();
            await page.GetByPlaceholder("Search Product").FillAsync("Winter Top");
            await page.Locator("//button[@id='submit_search']").ClickAsync();
            await Assertions.Expect(page.Locator(".productinfo")).ToBeVisibleAsync();
            await Assertions.Expect(page.Locator(".productinfo")).ToContainTextAsync("Winter Top");
        }

    }
    
}
