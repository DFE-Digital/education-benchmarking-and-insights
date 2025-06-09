using Web.App.Domain;
using Web.App.ViewComponents;

namespace Web.App.ViewModels.Components;

public class SchoolSpendingCostsViewModel(
    string? id,
    string urn,
    bool isPartOfTrust,
    bool isCustomData,
    bool hasIncompleteData,
    List<SchoolSpendingCostsViewModelCostCategory<PriorityCostCategoryDatum>> categories,
    Dictionary<string, CommercialResourceLink[]> resources)
{
    public List<SchoolSpendingCostsViewModelCostCategory<PriorityCostCategoryDatum>> Costs => categories;
    public string? Id => id;
    public string Urn => urn;
    public bool HasIncompleteData => hasIncompleteData;
    public bool IsCustomData => isCustomData;
    public bool IsPartOfTrust => isPartOfTrust;
    public CostCodes CostCodes => new(IsPartOfTrust);
    public Dictionary<string, CommercialResourceLink[]> Resources => resources;
}

public class SchoolSpendingCostsViewModelCostCategory<T>
{
    public string? Uuid { get; set; }
    public CostCategory? Category { get; set; }
    public string? ChartSvg { get; set; }
    public bool HasNegativeOrZeroValues { get; set; }
    public T[]? Data { get; set; }
}

public class SchoolSpendingCostsViewModelCostCategories : List<SchoolSpendingCostsViewModelCostCategory<PriorityCostCategoryDatum>>
{
    public SchoolSpendingCostsViewModelCostCategories(string urn, IEnumerable<CostCategory> costs)
    {
        foreach (var costCategory in costs)
        {
            var data = costCategory.Values.Select(x => new PriorityCostCategoryDatum { Urn = x.Key, Amount = x.Value.Value }).ToArray();
            var filteredData = data.Where(x => x.Urn == urn || x.Amount > 0).ToArray();
            var hasNegativeOrZeroValues = data.Length > filteredData.Length;
            var uuid = Guid.NewGuid().ToString();

            Add(new SchoolSpendingCostsViewModelCostCategory<PriorityCostCategoryDatum>
            {
                Uuid = uuid,
                Category = costCategory,
                HasNegativeOrZeroValues = hasNegativeOrZeroValues,
                Data = filteredData
            });
        }
    }
}