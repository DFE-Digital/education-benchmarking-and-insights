using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels;

public class TrustComparisonSubCategoriesViewModel
{
    public List<TrustBenchmarkingViewModelCostSubCategory> Items { get; set; } = [];

    public TrustComparisonSubCategoriesViewModel(
        string companyNumber,
        TrustItSpend[] expenditures,
        TrustItSpendForecastYear[]? forecasts,
        ItSpendingCategories.SubCategoryFilter[] filters)
    {
        filters = filters.Length > 0 ? filters : ItSpendingCategories.All;

        foreach (var filter in filters)
        {
            AddItSubCategory(companyNumber, filter, expenditures, forecasts);
        }
    }

    private void AddItSubCategory(string companyNumber, ItSpendingCategories.SubCategoryFilter filter, TrustItSpend[] expenditures, TrustItSpendForecastYear[]? forecasts)
    {
        var data = expenditures.GroupBy(e => e, (g, enumerable) => new TrustComparisonDatum
        {
            CompanyNumber = g.CompanyNumber,
            TrustName = g.TrustName,
            Expenditure = enumerable.Select(filter.GetSelector()).FirstOrDefault()
        }).ToArray();

        var uuid = Guid.NewGuid().ToString();
        var filteredData = data
            .Where(x => x.CompanyNumber == companyNumber || x.Expenditure > 0)
            .OrderByDescending(x => x.Expenditure)
            .ToArray();

        var forecastData = forecasts?
            .Where(f => f.Year != null)
            .OrderBy(f => f.Year)
            .Select(f => new TrustForecastDatum { Year = (int)f.Year!, Expenditure = filter.GetSelector()(f) })
            .Where(d => d.Expenditure != null)
            .ToArray();

        Items.Add(new TrustBenchmarkingViewModelCostSubCategory
        {
            Uuid = uuid,
            SubCategory = filter.GetHeadingForTrust(),
            Data = filteredData,
            ForecastData = forecastData
        });
    }
}

public class TrustBenchmarkingViewModelCostSubCategory : BenchmarkingViewModelCostSubCategory<TrustComparisonDatum>
{
    public string? ForecastChartSvg { get; set; }
    public TrustForecastDatum[]? ForecastData { get; init; }
}