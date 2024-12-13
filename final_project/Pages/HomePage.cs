using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Playwright;

namespace final_project.Pages
{
    internal class HomePage
    {
        private readonly IPage _page;

        public HomePage(IPage page)
        {
            _page = page;
        }

        public string homePageURL = "https://automationexercise.com/";

        public async Task Open()
        {
            await _page.GotoAsync(homePageURL);
        }

        public async Task AcceptDataPolicy()
        {
            await _page.GetByLabel("Consent", new() { Exact = true }).ClickAsync();
        }

        public async Task Login(string email, string password)
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "Signup / Login" }).ClickAsync();
            await _page.Locator("form").Filter(new() { HasText = "Login" }).GetByPlaceholder("Email Address").FillAsync(email);
            await _page.GetByPlaceholder("Password").FillAsync(password);
            await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        }

        public async Task OpenContuctUs()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "Contact us" }).ClickAsync();
        }

        public async Task OpenProductsPage()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "Products" }).ClickAsync();
        }

        public async Task AddFirstProductToCart() 
        {
            await _page.Locator("//*[contains(@class, 'productinfo')]//a").First.HoverAsync();
            await _page.Locator("//*[contains(@class, 'productinfo')]//a").First.ClickAsync();
        }

        public async Task OpenFirstProduct() 
        {
            await _page.Locator(".choose").First.ClickAsync();
        }

        public ILocator GetLoginPageLocator()
        {
            return _page.GetByRole(AriaRole.Link, new() { Name = "Signup / Login" });
        }

        public ILocator GetDeleteAccountPageLocator()
        {
            return _page.GetByRole(AriaRole.Link, new() { Name = "Delete Account" });
        }

        

        public ILocator GetFirstProductInfoLocator()
        {
            return _page.Locator("//*[contains(@class, 'productinfo')]").First;
        }

        public async Task<string> GetNameOfFirstProduct()
        {
            return await _page.Locator("//*[contains(@class, 'productinfo')]//p").First.TextContentAsync();
        }   

        public async Task<string> GetPriceOfFirstProduct()
        {
            return await _page.Locator("//*[contains(@class, 'productinfo')]//h2").First.TextContentAsync();
        }   
        
        public async Task ClickViewCart()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "View Cart" }).ClickAsync();
        }

        public async Task SearchProduct(string productName)
        {
            await _page.GetByPlaceholder("Search Product").FillAsync(productName);
            await _page.Locator("//button[@id='submit_search']").ClickAsync();
        }

    }
        
}
