using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels.Components;

public class SchoolComparisonViewModelCostSubCategory<T>
{
    public string? Uuid { get; init; }
    public string? SubCategory { get; init; }
    public string? ChartSvg { get; set; }
    public bool HasNegativeOrZeroValues { get; init; }
    public T[]? Data { get; init; }
}

public class SchoolComparisonViewModelCostSubCategories : List<SchoolComparisonViewModelCostSubCategory<SchoolComparisonDatum>>
{
    public SchoolComparisonViewModelCostSubCategories(string urn, SchoolItSpend[] expenditures)
    {
        AddItSubCategory(urn, "Administration software and systems E20D", s => s.AdministrationSoftwareAndSystems, expenditures);
        AddItSubCategory(urn, "Connectivity E20A", s => s.Connectivity, expenditures);
        AddItSubCategory(urn, "IT learning resources E20C", s => s.ItLearningResources, expenditures);
        AddItSubCategory(urn, "IT support E20G", s => s.ItSupport, expenditures);
        AddItSubCategory(urn, "Laptops, desktops and tablets E20E ", s => s.LaptopsDesktopsAndTablets, expenditures);
        AddItSubCategory(urn, "Onsite servers E20B", s => s.OnsiteServers, expenditures);
        AddItSubCategory(urn, "Other hardware E20F", s => s.OtherHardware, expenditures);
    }

    private void AddItSubCategory(string urn, string subCategoryName, Func<SchoolItSpend, decimal?> selector, SchoolItSpend[] expenditures)
    {

        var data = expenditures.GroupBy(e => e, (g, enumerable) => new SchoolComparisonDatum
        {
            Urn = g.URN,
            SchoolName = g.SchoolName,
            Expenditure = enumerable.Select(selector).FirstOrDefault()
        }).ToArray();

        var uuid = Guid.NewGuid().ToString();
        var filteredData = data.Where(x => x.Urn == urn || x.Expenditure > 0).ToArray();
        var hasNegativeOrZeroValues = data.Length > filteredData.Length;
        Add(new SchoolComparisonViewModelCostSubCategory<SchoolComparisonDatum>
        {
            Uuid = uuid,
            SubCategory = subCategoryName,
            HasNegativeOrZeroValues = hasNegativeOrZeroValues,
            Data = filteredData
        });
    }
}