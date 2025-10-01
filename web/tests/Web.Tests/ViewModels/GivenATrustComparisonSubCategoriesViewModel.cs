using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenATrustComparisonSubCategoriesViewModel
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldFlattenAllSubCategoriesForTrustItSpendWithoutForecast()
    {
        const string companyNumber = nameof(companyNumber);
        var expenditures = _fixture.Build<TrustItSpend>().CreateMany().ToArray();

        var actual = new TrustComparisonSubCategoriesViewModel(companyNumber, expenditures, null, ItSpendingCategories.All);

        Assert.Equal(7, actual.Items.Count);
        AssertSubCategory(actual.Items.ElementAt(0), "ICT costs: Administration software and systems", companyNumber, s => s.AdministrationSoftwareAndSystems, expenditures);
        AssertSubCategory(actual.Items.ElementAt(1), "ICT costs: Connectivity", companyNumber, s => s.Connectivity, expenditures);
        AssertSubCategory(actual.Items.ElementAt(2), "ICT costs: IT learning resources", companyNumber, s => s.ItLearningResources, expenditures);
        AssertSubCategory(actual.Items.ElementAt(3), "ICT costs: IT support", companyNumber, s => s.ItSupport, expenditures);
        AssertSubCategory(actual.Items.ElementAt(4), "ICT costs: Laptops, desktops and tablets", companyNumber, s => s.LaptopsDesktopsAndTablets, expenditures);
        AssertSubCategory(actual.Items.ElementAt(5), "ICT costs: Onsite servers", companyNumber, s => s.OnsiteServers, expenditures);
        AssertSubCategory(actual.Items.ElementAt(6), "ICT costs: Other hardware", companyNumber, s => s.OtherHardware, expenditures);
    }

    [Fact]
    public void ShouldFlattenAllSubCategoriesForTrustItSpendWithForecast()
    {
        const string companyNumber = nameof(companyNumber);
        var expenditures = _fixture.Build<TrustItSpend>().CreateMany().ToArray();
        var forecasts = _fixture.Build<TrustItSpendForecastYear>().CreateMany().ToArray();

        var actual = new TrustComparisonSubCategoriesViewModel(companyNumber, expenditures, forecasts, ItSpendingCategories.All);

        Assert.Equal(7, actual.Items.Count);
        AssertSubCategory(actual.Items.ElementAt(0), "ICT costs: Administration software and systems", companyNumber, s => s.AdministrationSoftwareAndSystems, expenditures, forecasts);
        AssertSubCategory(actual.Items.ElementAt(1), "ICT costs: Connectivity", companyNumber, s => s.Connectivity, expenditures, forecasts);
        AssertSubCategory(actual.Items.ElementAt(2), "ICT costs: IT learning resources", companyNumber, s => s.ItLearningResources, expenditures, forecasts);
        AssertSubCategory(actual.Items.ElementAt(3), "ICT costs: IT support", companyNumber, s => s.ItSupport, expenditures, forecasts);
        AssertSubCategory(actual.Items.ElementAt(4), "ICT costs: Laptops, desktops and tablets", companyNumber, s => s.LaptopsDesktopsAndTablets, expenditures, forecasts);
        AssertSubCategory(actual.Items.ElementAt(5), "ICT costs: Onsite servers", companyNumber, s => s.OnsiteServers, expenditures, forecasts);
        AssertSubCategory(actual.Items.ElementAt(6), "ICT costs: Other hardware", companyNumber, s => s.OtherHardware, expenditures, forecasts);
    }

    private static void AssertSubCategory(
        TrustBenchmarkingViewModelCostSubCategory actual,
        string name,
        string companyNumber,
        Func<ItSpend, decimal?> selector,
        TrustItSpend[] expenditures,
        TrustItSpendForecastYear[]? forecasts = null)
    {
        Assert.Null(actual.ChartSvg);
        Assert.Equal(name, actual.SubCategory);
        Assert.NotNull(actual.Uuid);

        var expected = expenditures
            .Select(e => new TrustComparisonDatum
            {
                CompanyNumber = e.CompanyNumber,
                TrustName = e.TrustName,
                Expenditure = selector(e)
            })
            .Where(x => x.CompanyNumber == companyNumber || x.Expenditure > 0);

        Assert.Equivalent(expected, actual.Data);

        if (forecasts == null)
        {
            Assert.Null(actual.ForecastData);
            return;
        }

        var expectedForecast = forecasts
            .OrderBy(f => f.Year)
            .Select(f => new KeyValuePair<int, decimal?>((int)f.Year!, selector(f)))
            .ToDictionary();
        Assert.Equal(expectedForecast.Keys, actual.ForecastData?.Years);
        foreach (var (year, value) in expectedForecast)
        {
            Assert.Equal(value, actual.ForecastData?[year]);
        }
    }
}