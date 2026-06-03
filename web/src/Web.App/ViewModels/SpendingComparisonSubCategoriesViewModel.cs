using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Domain.Schools;

namespace Web.App.ViewModels;

public class SpendingComparisonSubCategoriesViewModel
{
    public List<SchoolSpendingComparisonGroup> Groups { get; } = [];

    public SpendingComparisonSubCategoriesViewModel(
        SchoolExpenditure[] buildingResult,
        SchoolExpenditure[] pupilResult,
        SchoolSpendingCategories.SubCategoryFilter[] filters,
        string urn,
        CostCodes costCodes)
    {
        filters = filters.Length > 0 ? filters : SchoolSpendingCategories.All;

        foreach (var (group, children) in SchoolSpendingCategories.Groups)
        {
            var activeChildren = children.Where(filters.Contains).ToArray();
            if (activeChildren.Length == 0)
                continue;

            var comparatorSetType = group.GetCategoryGroupSetType();

            var groupVm = new SchoolSpendingComparisonGroup
            {
                Group = group.GetCategoryGroupDescription(),
                ComparatorSetType = comparatorSetType,
                Items = []
            };

            var expenditures = comparatorSetType == ComparatorSetTypes.Building ? buildingResult : pupilResult;

            foreach (var filter in activeChildren)
            {
                var codes = filter.GetCostCodes(costCodes);
                var sub = BuildSubCategory(filter, expenditures, urn, codes);
                groupVm.Items.Add(sub);
            }

            Groups.Add(groupVm);
        }
    }

    private static BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum> BuildSubCategory(
        SchoolSpendingCategories.SubCategoryFilter filter,
        SchoolExpenditure[] expenditures,
        string urn,
        string[] costCodes)
    {
        var data = expenditures
            .Select(e => new SchoolComparisonDatum
            {
                Urn = e.URN,
                SchoolName = e.SchoolName,
                LAName = e.LAName,
                SchoolType = e.SchoolType,
                Expenditure = filter.GetValue(e),
                TotalPupils = e.TotalPupils,
                PeriodCoveredByReturn = e.PeriodCoveredByReturn
            })
            .Where(x => x.Urn == urn || x.Expenditure != null)
            .OrderByDescending(x => x.Expenditure)
            .ToArray();

        return new BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum>
        {
            Uuid = Guid.NewGuid().ToString(),
            SubCategory = filter.GetHeading(),
            SubCategoryId = (int)filter,
            Data = data,
            LineCodes = costCodes
        };
    }
}

public class SchoolSpendingComparisonGroup
{
    public string Group { get; init; } = "";
    public string ComparatorSetType { get; init; } = "";
    public List<BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum>> Items { get; init; } = [];
    public int SelectedCount(HashSet<int> selectedIds) =>
        Items.Count(i => i.SubCategoryId.HasValue && selectedIds.Contains(i.SubCategoryId.Value));
    public IEnumerable<BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum>> SelectedItems(HashSet<int> selectedIds) =>
        Items.Where(i => i.SubCategoryId.HasValue && selectedIds.Contains(i.SubCategoryId.Value));
}
