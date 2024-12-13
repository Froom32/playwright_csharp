using Microsoft.Playwright;
using final_project.Models;

namespace final_project.Pages
{
    internal class ContactUsPage
    {
        private readonly IPage _page;

        public ContactUsPage(IPage page)
        {
            _page = page;
        }

        public async Task FillContactInfo(ContactInfo contactInfo)
        {
            await _page.GetByPlaceholder("Name").FillAsync(contactInfo.UserName);
            await _page.GetByPlaceholder("Email", new() { Exact = true }).FillAsync(contactInfo.UserEmail);
            await _page.GetByPlaceholder("Subject").FillAsync(contactInfo.Subject);
            await _page.Locator("input[name='upload_file']").SetInputFilesAsync(contactInfo.FilePath);
            await _page.GetByPlaceholder("Your Message Here").FillAsync(contactInfo.Message);
        }

        public async Task SubmitContactInfo()
        {
            _page.Dialog += async (_, dialog) =>
                {
                    await dialog.AcceptAsync();
                };
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await _page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
        }

        public ILocator GetContactInfoLocator()
        {
            return _page.Locator("#contact-page");
        }

        public async Task ClickSuccessButton()
        {
            await _page.Locator(".btn-success").ClickAsync();
        }

        public async Task ClickMyAccount()
        {
            await _page.GetByRole(AriaRole.Button, new() { Name = " My account" }).ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task SelectSearchResult(int index)
        {
            await _page.Locator(".image > a").Nth(index).ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }
        

    }
        
}
