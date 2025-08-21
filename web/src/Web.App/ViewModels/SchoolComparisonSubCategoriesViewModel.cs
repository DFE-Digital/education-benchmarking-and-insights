using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels;

public class SchoolComparisonViewModelCostSubCategory<T>
{
    public string? Uuid { get; init; }
    public string? SubCategory { get; init; }
    public string? ChartSvg { get; set; }
    public T[]? Data { get; init; }
}

public class SchoolComparisonSubCategoriesViewModel : List<SchoolComparisonViewModelCostSubCategory<SchoolComparisonDatum>>
{
    public SchoolComparisonSubCategoriesViewModel(string urn, SchoolItSpend[] expenditures,
        ItSpendingCategories.SubCategoryFilter[] filters)
    {
        filters = filters.Length > 0 ? filters : ItSpendingCategories.All;

        foreach (var filter in filters)
        {
            AddItSubCategory(urn, filter, expenditures);
        }
    }

    private void AddItSubCategory(string urn, ItSpendingCategories.SubCategoryFilter filter, SchoolItSpend[] expenditures)
    {
        var data = expenditures.GroupBy(e => e, (g, enumerable) => new SchoolComparisonDatum
        {
            Urn = g.URN,
            SchoolName = g.SchoolName,
            Expenditure = enumerable.Select(filter.GetSelector()).FirstOrDefault(),
            LAName = g.LAName,
            SchoolType = g.SchoolType,
            TotalPupils = g.TotalPupils,
            PeriodCoveredByReturn = g.PeriodCoveredByReturn
        }).ToArray();

        var uuid = Guid.NewGuid().ToString();
        var filteredData = data
            .Where(x => x.Urn == urn || x.Expenditure > 0)
            .OrderByDescending(x => x.Expenditure)
            .ToArray();

        Add(new SchoolComparisonViewModelCostSubCategory<SchoolComparisonDatum>
        {
            Uuid = uuid,
            SubCategory = filter.GetHeading(),
            Data = filteredData
        });
    }
}