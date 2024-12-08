using final_project;
using final_project.Models;
using final_project.Pages;
using Microsoft.Playwright;

namespace final_project.Tests
{
    [TestFixture]
    public class ContactUsTests : UITestFixture
    {
        [Test]
        public async Task ContactUsTest()
        {
            //Arrange
            var homePage = new HomePage(page);
            var contactUsPage = new ContactUsPage(page);
            var contactInfo = new ContactInfo()
            {
                UserName = "John",
                UserEmail = "testmail@gmail.com",
                Subject = "test subject",
                FilePath = "../../../TestFiles/test_file.txt",
                Message = "This is the test message for testing 'Contact Us' form"
            };

            //Act
            await homePage.OpenContuctUs();
            await contactUsPage.FillContactInfo(contactInfo);
            await contactUsPage.SubmitContactInfo();

            //Assert
            await Assertions.Expect(contactUsPage.GetContactInfoLocator()).ToContainTextAsync("Success! Your details have been submitted successfully.");
            await contactUsPage.ClickSuccessButton();
            StringAssert.EndsWith(homePage.homePageURL, page.Url);
        }
    }
    
}
