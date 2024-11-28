using LambdatestEcom;
using Microsoft.Playwright;

namespace final_project.Tests
{
    [TestFixture]
    public class DeleteProductFromCartTests : UITestFixture
    {
        [Test]
        public async Task DeleteProductFromCartTest()
        {
            await page.GotoAsync("https://automationexercise.com/");
            await page.GetByLabel("Consent", new() { Exact = true }).ClickAsync();
            await page.Locator("//*[@class='single-products']//a").First.HoverAsync();
            await page.Locator("//*[@class='single-products']//a").First.ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "View Cart" }).ClickAsync();
            await page.Locator(".cart_quantity_delete").ClickAsync();
            await Assertions.Expect(page.Locator("#cart_info")).ToContainTextAsync("Cart is empty! Click here to buy products.");
        }

    }
    
}
