using Microsoft.Playwright;
using final_project.Models;

namespace final_project.Pages
{
    internal class CartPage
    {
        private readonly IPage _page;

        public CartPage(IPage page)
        {
            _page = page;
        }

        public async Task DeleteProductFromCart() 
        {
            await _page.Locator(".cart_quantity_delete").ClickAsync();
        }

        public ILocator GetCartInfoLocator()
        {
            return _page.Locator("#cart_info");
        }

        public ILocator GetFirstProductLocator()
        {
            return _page.Locator("#product-1");
        }
    }
        
}
