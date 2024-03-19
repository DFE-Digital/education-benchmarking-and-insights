using Microsoft.Playwright;
using Web.E2ETests.Pages.School;

namespace Web.E2ETests.Pages;

public class FindOrganisationPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);
    private ILocator OrganisationsTypeRadios => page.Locator(Selectors.GovRadios);
    private ILocator SchoolSearchInputField => page.Locator(Selectors.SchoolSearchInput);
    private ILocator ContinueCta => page.Locator(Selectors.GovButton, new PageLocatorOptions { HasText = "Continue" });

    public async Task IsDisplayed()
    {
       await PageH1Heading.ShouldBeVisible();
       await Breadcrumbs.ShouldBeVisible();
       await OrganisationsTypeRadios.ShouldBeVisible();
       await ContinueCta.ShouldBeVisible().ShouldBeEnabled();
    }

    public async Task<HomePage> SelectSchoolFromSuggester(string text)
    {
       await SchoolSearchInputField.PressSequentially(text).Press("ArrowDown").Press("Enter");
       return new HomePage(page);
    }
    
}