using Microsoft.Playwright;

namespace Web.E2ETests.Pages.LocalAuthority;

public class EducationHealthCarePlanBenchmarkingPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Benchmark education, health care plans"
        });
    private ILocator TotalEhcPlansSection => page.Locator("#cost-sub-category-total-pupils-with-ehc-plans");
    private ILocator MainstreamSection => page.Locator("#cost-sub-category-placement-of-pupils-with-ehc-plans-in-mainstream-schools-or-academies");
    private ILocator ResourcedSection => page.Locator("#cost-sub-category-placement-of-pupils-with-ehc-plans-resourced-provision-or-sen-units");
    private ILocator SpecialSection => page.Locator("#cost-sub-category-placement-of-pupils-with-ehc-plans-maintained-special-school-or-special-academies");
    private ILocator IndependentSection => page.Locator("#cost-sub-category-placement-of-pupils-with-ehc-plans-nmss-or-independent-schools");
    private ILocator HospitalSection => page.Locator("#cost-sub-category-placement-of-pupils-with-ehc-plans-in-hospital-schools-or-alternative-provisions");
    private ILocator Post16Section => page.Locator("#cost-sub-category-placement-of-pupils-with-ehc-plans-in-post-16");
    private ILocator OtherSection => page.Locator("#cost-sub-category-placement-of-pupils-with-ehc-plans-in-other-types-of-provisions");

    private ILocator Tables => page.Locator(Selectors.GovTable);
    private ILocator ViewAsTableRadio => page.Locator(Selectors.ViewAsTable);
    private ILocator ApplyCta => page.Locator(Selectors.GovButton, new PageLocatorOptions
    {
        HasText = "Apply"

    });
    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task AreAllChartsDisplayed()
    {
        await TotalEhcPlansSection.ShouldBeVisible();
        await MainstreamSection.ShouldBeVisible();
        await ResourcedSection.ShouldBeVisible();
        await SpecialSection.ShouldBeVisible();
        await IndependentSection.ShouldBeVisible();
        await HospitalSection.ShouldBeVisible();
        await Post16Section.ShouldBeVisible();
        await OtherSection.ShouldBeVisible();
    }

    public async Task ClickViewAsTableAndApply()
    {
        await ViewAsTableRadio.Click();
        await ApplyCta.Nth(1).Click();
        await Tables.First.ShouldBeVisible();
    }

    public async Task IsTableDataForChartDisplayed(string chartHeading, List<List<string>> expectedData)
    {
        var table = page.Locator("h3", new PageLocatorOptions { HasText = chartHeading })
            .Locator("../..")
            .Locator(Selectors.GovTable)
            .First;

        await table.ShouldBeVisible();
        await table.ShouldHaveTableContent(expectedData, true);
    }

}