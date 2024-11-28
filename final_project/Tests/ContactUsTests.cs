using LambdatestEcom;
using Microsoft.Playwright;

namespace final_project.Tests
{
    [TestFixture]
    public class ContactUsTests : UITestFixture
    {
        [Test]
        public async Task ContactUsTest()
        {
            await page.GotoAsync("https://automationexercise.com/");
            await page.GetByLabel("Consent", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Contact us" }).ClickAsync();
            await page.GetByPlaceholder("Name").FillAsync("Mike");
            await page.GetByPlaceholder("Email", new() { Exact = true }).FillAsync("bilikmike@gmail.com");
            await page.GetByPlaceholder("Subject").FillAsync("test subject");
            await page.Locator("input[name='upload_file']").SetInputFilesAsync(["../../../TestFiles/test_file.txt"]);
            await page.GetByPlaceholder("Your Message Here").FillAsync("This is the test message for testing 'Contact Us' form");
            page.Dialog += async (_, dialog) =>
                {
                    await dialog.AcceptAsync();
                };
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await Assertions.Expect(page.Locator("#contact-page")).ToContainTextAsync("Success! Your details have been submitted successfully.");
            await page.Locator(".btn-success").ClickAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "AutomationExercise" })).ToBeVisibleAsync();
        }

    }
    
}
