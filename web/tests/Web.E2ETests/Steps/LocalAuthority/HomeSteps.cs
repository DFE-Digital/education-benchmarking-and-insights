﻿using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;
namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority homepage")]
public class HomeSteps(PageDriver driver)
{
    private CompareYourCostsPage? _compareYourCostsPage;
    private HomePage? _localAuthorityHomePage;

    [Given("I am on local authority homepage for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHomepageForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHomeUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _localAuthorityHomePage = new HomePage(page);
        await _localAuthorityHomePage.IsDisplayed();
    }

    [When("I click on compare your costs")]
    public async Task WhenIClickOnCompareYourCosts()
    {
        Assert.NotNull(_localAuthorityHomePage);
        _compareYourCostsPage = await _localAuthorityHomePage.ClickCompareYourCosts();
    }

    [Then("the compare your costs page is displayed")]
    public async Task ThenTheCompareYourCostsPageIsDisplayed()
    {
        Assert.NotNull(_compareYourCostsPage);
        await _compareYourCostsPage.IsDisplayed();
    }

    private static string LocalAuthorityHomeUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}";
}