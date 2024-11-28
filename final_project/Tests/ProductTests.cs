using LambdatestEcom;
using Microsoft.Playwright;

namespace final_project.Tests
{
    [TestFixture]
    public class ProductTests : UITestFixture
    {
        [Test]
        public async Task ProductDetailTest()
        {
            await page.GotoAsync("https://automationexercise.com/");
            await page.GetByLabel("Consent", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Products" }).ClickAsync();
            await page.Locator(".choose").First.ClickAsync();
            await Assertions.Expect(page.Locator("section")).ToContainTextAsync("Blue Top");
            await Assertions.Expect(page.Locator("section")).ToContainTextAsync("Category:");
            await Assertions.Expect(page.Locator("section")).ToContainTextAsync("Rs.");
            await Assertions.Expect(page.Locator("section")).ToContainTextAsync("Availability:");
            await Assertions.Expect(page.Locator("section")).ToContainTextAsync("Brand:");
        }

    }
    
}
