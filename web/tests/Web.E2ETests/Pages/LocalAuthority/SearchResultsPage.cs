using Microsoft.Playwright;

namespace Web.E2ETests.Pages.LocalAuthority;

public class SearchResultsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator SearchTermInputField => page.Locator(Selectors.SearchTermInput);
    private ILocator SearchButton => page.Locator($"{Selectors.GovButton}[name='action'][value='reset']");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await SearchTermInputField.ShouldBeVisible();
        await SearchButton.ShouldBeVisible().ShouldBeEnabled();
    }
}