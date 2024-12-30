using Microsoft.Playwright;
using Xunit;
namespace Web.E2ETests.Pages.Trust;

public class TrustForecastPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);

    private ILocator SaveAsImageButton => page.Locator(Selectors.GovButton, new PageLocatorOptions
    {
        HasText = "Save"
    });

    private ILocator ChartDimension => page.Locator(Selectors.ForecastRisksDimension);
    private ILocator GraphImage => page.Locator(Selectors.Charts);
    private ILocator VarianceDetails =>
        page.Locator(Selectors.GovDetailsSummaryText,
            new PageLocatorOptions
            {
                HasText = "Variance status thresholds"
            });

    private ILocator ForecastSummaryTable => page.Locator(Selectors.GovDetails + " ~ " + Selectors.GovTable);
    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible().ShouldHaveText("Forecast and risks");
        await SaveAsImageButton.ShouldBeVisible();
        await ChartDimension.ShouldBeVisible();
        var dimensionOptions = await ChartDimension.InnerTextAsync();
        var expectedOptions = new[] { "actuals", "£ per pupil" };
        foreach (var dimensionValue in expectedOptions)
        {
            Assert.Contains(dimensionValue, dimensionOptions);
        }

        await GraphImage.ShouldBeVisible();
        await VarianceDetails.ShouldBeVisible();

    }

    public async Task IsForbidden()
    {
        await PageH1Heading.ShouldBeVisible().ShouldHaveText("Access denied");
    }

    public async Task IsTableDataDisplayed(List<List<string>> expected)
    {
        await ForecastSummaryTable.ShouldBeVisible();
        await ForecastSummaryTable.ShouldHaveTableContent(expected, true);

    }
}