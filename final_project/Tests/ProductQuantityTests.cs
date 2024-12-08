using final_project;
using final_project.Pages;
using Microsoft.Playwright;

namespace final_project.Tests
{
    [TestFixture]
    public class ProductQuantityTests : UITestFixture
    {
        [Test]
        public async Task ProductQuantityTest()
        {
            //Arrange
            var homePage = new HomePage(page);
            var productPage = new ProductPage(page);
            var cartPage = new CartPage(page);

            //Act
            await homePage.OpenFirstProduct();
            await productPage.FillProductQuantity(4);
            await productPage.ClickAddToCart();
            await homePage.ClickViewCart();

            //Assert
            await Assertions.Expect(cartPage.GetFirstProductLocator()).ToContainTextAsync("4");
        }

    }
    
}
