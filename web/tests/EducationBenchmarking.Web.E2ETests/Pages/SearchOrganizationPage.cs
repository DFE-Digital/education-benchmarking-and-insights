using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class SearchOrganizationPage
{
    private readonly IPage _page;

    public SearchOrganizationPage(IPage page)
    {
        _page = page;
    }

    private ILocator PageH1Heading => _page.Locator("h1");
    private ILocator AcademyAndLocalAuthSchoolSearch => _page.Locator("#school-input");
    private ILocator Suggester => _page.Locator("#school-suggestions");
    private ILocator ContinueBtn => _page.Locator("#search-btn");
    private ILocator SelectFromSuggestions(string text) => _page.Locator($"#school-suggestions li:has-text(\"{text}\"):first-child");

    public async Task TypeInSearchSearchBar(string searchInput)
    {
        await AcademyAndLocalAuthSchoolSearch.FocusAsync();
        await _page.Keyboard.TypeAsync(searchInput);
    }

    public async Task ClickOnSuggestion(string searchInput)
    {
        await Suggester.IsVisibleAsync();
        await SelectFromSuggestions(searchInput).ClickAsync();
    }

    public async Task ClickContinueBtn()
    {
        await ContinueBtn.ClickAsync();
    }
}