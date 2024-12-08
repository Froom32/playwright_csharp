using final_project;
using final_project.Pages;
using Microsoft.Playwright;

namespace final_project.Tests
{
    [TestFixture]
    public class ProductTests : UITestFixture
    {
        [Test]
        public async Task ProductDetailTest()
        {
            //Arrange
            var homePage = new HomePage(page);
            var productPage = new ProductPage(page);

            //Act
            await homePage.OpenProductsPage();
            var productName = await homePage.GetNameOfFirstProduct();
            var productPrice = await homePage.GetPriceOfFirstProduct();
            await homePage.OpenFirstProduct();

            //Assert
            await Assertions.Expect(productPage.GetProductInfoLocator()).ToContainTextAsync(productName);
            await Assertions.Expect(productPage.GetProductInfoLocator()).ToContainTextAsync("Category:");
            await Assertions.Expect(productPage.GetProductInfoLocator()).ToContainTextAsync(productPrice);
            await Assertions.Expect(productPage.GetProductInfoLocator()).ToContainTextAsync("Availability:");
            await Assertions.Expect(productPage.GetProductInfoLocator()).ToContainTextAsync("Brand:");
        }

    }
    
}
