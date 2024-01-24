using EducationBenchmarking.Web.E2ETests.Hooks;
using EducationBenchmarking.Web.E2ETests.TestSupport;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Pages;

public class SearchOrganizationPage
{
    private readonly IPage _page;

    public SearchOrganizationPage(PageHook page)
    {
        _page = page.Current;
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

    public async Task WaitForPage()
    {
        await _page.WaitForURLAsync(Config.BaseUrl + "/find-organisation");
    }
}