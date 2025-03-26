using Web.App.Domain;

namespace Web.App.ViewModels.Components;

public class PriorityCostCategoryViewModel(
    string? id,
    string uuid,
    string urn,
    bool isPartOfTrust,
    bool isCustomData,
    bool hasIncompleteData,
    bool hasNegativeOrZeroValues,
    CostCategory? costCategory,
    bool isFirstItem,
    string? chart)
{
    private CostCodes SourceCostCodes => new(isPartOfTrust);

    public string? Id => id;
    public string Uuid => uuid;
    public string Urn => urn;
    public bool IsCustomData => isCustomData;
    public bool HasIncompleteData => hasIncompleteData;
    public bool HasNegativeOrZeroValues => hasNegativeOrZeroValues;
    public CostCategory? Category => costCategory;
    public bool IsFirstItem => isFirstItem;
    public string? Chart => chart;

    public string[] CostCodes => Category?.SubCategories
        .Select(SourceCostCodes.GetCostCode)
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .Cast<string>()
        .ToArray() ?? [];
}