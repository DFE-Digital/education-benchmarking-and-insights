using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.Trust.Benchmarking;

public class TrustBenchmarkSpendingPage (IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator ShowHideAllSectionsLink => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator ViewOrChangeSetLink => page.Locator("a:has-text('View and change your set of trusts')");
    private ILocator SpendingTab => page.Locator(Selectors.TrustBenchmarkingSpendingTab);
    private ILocator Balance => page.Locator(Selectors.TrustBenchmarkingBalanceTab);
    private ILocator ViewAsChartRadio => page.Locator(Selectors.GovRadios).Locator("#spending-mode-chart");
    private ILocator ViewCentralSpendRadio => page.Locator(Selectors.GovRadios).Locator("#spending-include-breakdown");
    private ILocator SaveAsImageBtns => page.Locator(Selectors.GovButton, new PageLocatorOptions{HasText = "Save "});
    private ILocator TotalExpenditureDimension => page.Locator(Selectors.TotalExpenditureDimension);
    private ILocator Sections => page.Locator(Selectors.GovAccordionSection);
    private ILocator AllCharts => page.Locator(Selectors.ReactChartContainer);


    
    public async Task IsDisplayed()
    {
       await PageH1Heading.ShouldBeVisible();
       await ViewOrChangeSetLink.ShouldBeVisible();
       await SpendingTab.ShouldBeVisible().ShouldHaveAttribute("tabindex", "0");
       await Balance.ShouldBeVisible().ShouldHaveAttribute("tabindex", "-1");
      await ViewAsChartRadio.ShouldBeVisible().ShouldBeChecked();
      await ViewCentralSpendRadio.ShouldBeVisible().ShouldBeChecked();
      await SaveAsImageBtns.Nth(0).ShouldBeVisible();
      await TotalExpenditureDimension.ShouldBeVisible();
      await ShowHideAllSectionsLink.ShouldBeVisible();
      foreach (var sec in await Sections.AllAsync())
      {
         await sec.ShouldBeVisible();
      }
      Assert.Equal(9, await Sections.Count());
      await AllCharts.Nth(0).ShouldBeVisible();
    }
    
}