using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.Trust;

public enum AccordionsNames
{
    ContactRatio,
    AverageClassSize,
    InYearBalance
}
public class CurriculumFinancialPlanningPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator ShowHideAllSections => page.Locator(Selectors.GovShowAllLinkText);
    private ILocator AccordionHeadings => page.Locator(Selectors.AccordionHeadingText);
    private ILocator Sections => page.Locator(Selectors.GovAccordionSection);
    private ILocator AccordionsTable(string accordionContentNumber) => page.Locator($"{Selectors.AccordionSchoolContent}{accordionContentNumber}");
    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await ShowHideAllSections.ShouldBeVisible();
        var headings = await AccordionHeadings.AllAsync();
        Assert.Equal(3, headings.Count());
        foreach (var heading in headings)
        {
            await heading.ShouldBeVisible();
        }

    }

    public async Task ClickShowAllSections()
    {
        await ShowHideAllSections.Click();
    }

    public async Task AreSectionsExpanded()
    {
        var sections = await Sections.AllAsync();
        foreach (var section in sections)
        {
            await section.AssertLocatorClass("govuk-accordion__section govuk-accordion__section--expanded");
        }
    }

    public async Task IsTableDataDisplayed(AccordionsNames accordionName, List<List<string>> expected)
    {
        await AccordionsTable(accordionName).ShouldBeVisible();
        await AccordionsTable(accordionName).ShouldHaveTableContent(expected, true);
    }
    private ILocator AccordionsTable(AccordionsNames accordionsName)
    {
        var accordionTable = accordionsName switch
        {
            AccordionsNames.ContactRatio => AccordionsTable("1"),
            AccordionsNames.AverageClassSize => AccordionsTable("2"),
            AccordionsNames.InYearBalance => AccordionsTable("3"),
            _ => throw new ArgumentOutOfRangeException(nameof(accordionsName))
        };

        return accordionTable;
    }
}