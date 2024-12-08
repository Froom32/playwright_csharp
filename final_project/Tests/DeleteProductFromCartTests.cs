using final_project;
using final_project.Pages;
using Microsoft.Playwright;

namespace final_project.Tests
{
    [TestFixture]
    public class DeleteProductFromCartTests : UITestFixture
    {
        [Test]
        public async Task DeleteProductFromCartTest()
        {
            //Arrange
            var homePage = new HomePage(page);
            var cartPage = new CartPage(page);

            //Act
            await homePage.AddFirstProductToCart();
            await homePage.ClickViewCart();
            await cartPage.DeleteProductFromCart();

            //Assert
            await Assertions.Expect(cartPage.GetCartInfoLocator()).ToContainTextAsync("Cart is empty! Click here to buy products.");
        }

    }
    
}
