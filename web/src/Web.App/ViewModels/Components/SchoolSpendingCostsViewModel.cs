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
    IEnumerable<GroupedResources>? resources = null)
{
    public List<SchoolSpendingCostsViewModelCostCategory<PriorityCostCategoryDatum>> Costs => categories;
    public string? Id => id;
    public string? Urn => urn;
    public bool HasIncompleteData => hasIncompleteData;
    public bool IsCustomData => isCustomData;
    public bool IsPartOfTrust => isPartOfTrust;
    public CostCodes CostCodes => new(IsPartOfTrust);
    public IEnumerable<GroupedResources>? Resources => resources;
}

public class SchoolSpendingCostsViewModelCostCategory<T>
{
    public string? Uuid { get; set; }
    public CostCategory? Category { get; set; }
    public string? ChartSvg { get; set; }
    public bool HasNegativeOrZeroValues { get; set; }
    public T[]? Data { get; set; }
}