using Microsoft.Playwright;
using final_project.Models;

namespace final_project.Pages
{
    internal class ProductPage
    {
        private readonly IPage _page;

        public ProductPage(IPage page)
        {
            _page = page;
        }

        public ILocator GetProductInfoLocator()
        {
            return _page.Locator("section");
        }

        public async Task FillProductQuantity(int quantity) 
        {
            await _page.Locator("#quantity").FillAsync($"{quantity}");
        }

        public async Task ClickAddToCart()
        {
            await _page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" }).ClickAsync();
        }
    }
        
}
