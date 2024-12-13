using final_project;
using final_project.Pages;
using Microsoft.Playwright;

namespace final_project.Tests
{
    [TestFixture]
    public class SearchTests : UITestFixture
    {
        [Test]
        public async Task SearchProductTest()
        {
            //Arrange
            var homePage = new HomePage(page);
            var productName = "Winter Top";

            //Act
            await homePage.OpenProductsPage();
            await homePage.SearchProduct(productName);

            //Assert
            await Assertions.Expect(homePage.GetFirstProductInfoLocator()).ToBeVisibleAsync();
            await Assertions.Expect(homePage.GetFirstProductInfoLocator()).ToHaveCountAsync(1);
            await Assertions.Expect(homePage.GetFirstProductInfoLocator()).ToContainTextAsync(productName);
        }

    }
    
}
