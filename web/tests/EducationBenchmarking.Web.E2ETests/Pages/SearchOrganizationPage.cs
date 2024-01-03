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

    public async Task TypeInSearchSearchBar(string searchInput)
    {
        await AcademyAndLocalAuthSchoolSearch.FillAsync(searchInput);
        
    }
}