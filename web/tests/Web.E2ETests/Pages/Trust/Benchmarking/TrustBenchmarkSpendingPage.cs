using Microsoft.Playwright;

namespace Web.E2ETests.Pages.Trust.Benchmarking;

public class TrustBenchmarkSpendingPage (IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator ShowHideAllSectionsLink => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator ViewOrChangeSetLink => page.Locator("a:has-text('View and change your set of trusts')");
    private ILocator SpendingTab => page.Locator(Selectors.TrustBenchmarkingSpendingTab);
    private ILocator Balance => page.Locator(Selectors.TrustBenchmarkingSpendingTab);
    
    public async Task IsDisplayed()
    {
       await PageH1Heading.ShouldBeVisible();
       
       //todo increase coverage here
    }
}