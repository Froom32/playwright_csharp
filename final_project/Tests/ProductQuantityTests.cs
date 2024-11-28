using LambdatestEcom;
using Microsoft.Playwright;

namespace final_project.Tests
{
    [TestFixture]
    public class ProductQuantityTests : UITestFixture
    {
        [Test]
        public async Task ProductQuantityTest()
        {
            await page.GotoAsync("https://automationexercise.com/");
            await page.GetByLabel("Consent", new() { Exact = true }).ClickAsync();
            await page.Locator(".choose").First.ClickAsync();
            await page.Locator("#quantity").FillAsync("4");
            await page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "View Cart" }).ClickAsync();
            await Assertions.Expect(page.Locator("#product-1")).ToContainTextAsync("4");
        }

    }
    
}
