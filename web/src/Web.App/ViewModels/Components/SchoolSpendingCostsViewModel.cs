using Web.App.Domain;

namespace Web.App.ViewModels.Components;

public class SchoolSpendingCostsViewModel(
    string? id,
    string urn,
    bool isPartOfTrust,
    bool isCustomData,
    bool hasIncompleteData,
    List<SchoolSpendingCostsViewModelCostCategory> categories)
{
    public List<SchoolSpendingCostsViewModelCostCategory> Costs => categories;
    public string? Id => id;
    public string? Urn => urn;
    public bool HasIncompleteData => hasIncompleteData;
    public bool IsCustomData => isCustomData;
    public bool IsPartOfTrust => isPartOfTrust;
    public CostCodes CostCodes => new(IsPartOfTrust);
}

public class SchoolSpendingCostsViewModelCostCategory
{
    public string? Uuid { get; set; }
    public CostCategory? Category { get; set; }
    public string? ChartSvg { get; set; }
    public bool HasNegativeOrZeroValues { get; set; }
}