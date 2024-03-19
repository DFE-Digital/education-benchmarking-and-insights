using Microsoft.Playwright;
using Web.E2ETests.Pages.School;

namespace Web.E2ETests.Pages;

public class FindOrganisationPage(IPage page)
{
    private const string ArrowDownKey = "ArrowDown";
    private const string EnterKey = "Enter";
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator Breadcrumbs => page.Locator(Selectors.GovBreadcrumbs);
    private ILocator OrganisationsTypeRadios => page.Locator(Selectors.GovRadios);
    private ILocator SchoolSearchInputField => page.Locator(Selectors.SchoolSearchInput);
    private ILocator ContinueButton => page.Locator(Selectors.GovButton, new PageLocatorOptions { HasText = "Continue" });
    private ILocator SchoolRadioButton => page.Locator(Selectors.SchoolRadio);

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await Breadcrumbs.ShouldBeVisible();
        await OrganisationsTypeRadios.ShouldBeVisible();
        await ContinueButton.ShouldBeVisible().ShouldBeEnabled();
    }

    public async Task<HomePage> SelectSchoolFromSuggester(string text)
    {
        await SchoolSearchInputField.PressSequentially(text).Press(ArrowDownKey).Press(EnterKey);
        return new HomePage(page);
    }

    public async Task SelectOrganisationType(string type)
    {
        var radioButton = type switch
        {
            "school" => SchoolRadioButton,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
        await radioButton.Check();
    }

}