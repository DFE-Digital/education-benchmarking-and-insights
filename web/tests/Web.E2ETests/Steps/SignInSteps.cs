using Microsoft.Playwright;
using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;

namespace Web.E2ETests.Steps;

[Binding]
public class SignInSteps(PageDriver driver)
{

    [Given("I have signed in with organisation '(.*)'")]
    public async Task GivenIHaveSignedInWithOrganisation(string organisation)
    {
        await SignInWithOrganisation(organisation);
    }

    [Given("I am on the service home")]
    public async Task GivenIAmOnTheServiceHome()
    {
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(TestConfiguration.ServiceUrl);

        var homePage = new HomePage(page);
        await homePage.IsDisplayed();
    }

    [Given("I am not logged in")]
    public async Task GivenIAmNotLoggedIn()
    {
        await SignOut();
    }
    private async Task SignInWithOrganisation(string organisation)
    {
        var page = await driver.Current;

        if (await page.IsSignedIn())
        {
            return;
        }

        await page.SignInLink().ClickAsync();

        await page.Locator("input[id='username']").Fill(TestConfiguration.LoginEmail);
        await page.Locator("button[type='submit']").Click();
        await page.Locator("h1:text-is('Enter your password')").CheckVisible();
        await page.Locator("input[id='password']").Fill(TestConfiguration.LoginPassword);
        await page.Locator("button[type='submit']").Click();
        await page.Locator("label", new PageLocatorOptions
        {
            HasTextString = organisation
        }).Check();
        await page.Locator("input[type='submit']").Click();

        await page.SignOutLink().IsVisibleAsync();
    }

    private async Task SignOut()
    {
        var page = await driver.Current;

        if (await page.SignOutLink().IsVisibleAsync())
        {
            await page.SignOutLink().ClickAsync();
            await page.WaitForURLAsync($"{TestConfiguration.ServiceUrl}/");
        }
    }
}