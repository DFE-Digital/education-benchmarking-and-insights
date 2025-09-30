using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels;

public class TrustComparisonSubCategoriesViewModel
{
    public List<BenchmarkingViewModelCostSubCategory<TrustComparisonDatum>> Items { get; set; } = [];

    public TrustComparisonSubCategoriesViewModel(string companyNumber, TrustItSpend[] expenditures,
        ItSpendingCategories.SubCategoryFilter[] filters)
    {
        filters = filters.Length > 0 ? filters : ItSpendingCategories.All;

        foreach (var filter in filters)
        {
            AddItSubCategory(companyNumber, filter, expenditures);
        }
    }

    private void AddItSubCategory(string companyNumber, ItSpendingCategories.SubCategoryFilter filter, TrustItSpend[] expenditures)
    {
        var data = expenditures.GroupBy(e => e, (g, enumerable) => new TrustComparisonDatum
        {
            CompanyNumber = g.CompanyNumber,
            TrustName = g.TrustName,
            Expenditure = enumerable.Select(filter.GetSelector()).FirstOrDefault(),
        }).ToArray();

        var uuid = Guid.NewGuid().ToString();
        var filteredData = data
            .Where(x => x.CompanyNumber == companyNumber || x.Expenditure > 0)
            .OrderByDescending(x => x.Expenditure)
            .ToArray();

        Items.Add(new BenchmarkingViewModelCostSubCategory<TrustComparisonDatum>
        {
            Uuid = uuid,
            SubCategory = filter.GetHeadingForTrust(),
            Data = filteredData
        });
    }
}