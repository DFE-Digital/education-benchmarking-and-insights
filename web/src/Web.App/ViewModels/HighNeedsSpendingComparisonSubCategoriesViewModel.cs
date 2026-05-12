using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels;

public class HighNeedsSpendingComparisonSubCategoriesViewModel
{
    public List<HighNeedsSpendingComparisonGroup> Groups { get; } = [];

    public HighNeedsSpendingComparisonSubCategoriesViewModel(
        HighNeedsSpending[] expenditures,
        HighNeedsSpendingCategories.SubCategoryFilter[] filters,
        string code)
    {
        filters = filters.Length > 0 ? filters : HighNeedsSpendingCategories.All;

        foreach (var (group, children) in HighNeedsSpendingCategories.Groups)
        {
            var activeChildren = children.Where(filters.Contains).ToArray();
            if (activeChildren.Length == 0)
                continue;

            var groupVm = new HighNeedsSpendingComparisonGroup
            {
                Group = group.GetCategoryGroupDescription(),
                AdditionalText = group.GetCategoryGroupAdditionalText(),
                Items = []
            };

            foreach (var filter in activeChildren)
            {
                var sub = BuildSubCategory(filter, expenditures, code);
                groupVm.Items.Add(sub);
            }

            Groups.Add(groupVm);
        }
    }

    private static BenchmarkingViewModelCostSubCategory<HighNeedsSpendingComparisonDatum> BuildSubCategory(
        HighNeedsSpendingCategories.SubCategoryFilter filter,
        HighNeedsSpending[] expenditures,
        string code)
    {
        var data = expenditures
            .Select(e => new HighNeedsSpendingComparisonDatum
            {
                Code = e.Code,
                Name = e.Name,
                Expenditure = filter.GetValue(e),
                TotalPupils = e.TotalPupils
            })
            .Where(x => x.Code == code || x.Expenditure != null)
            .OrderByDescending(x => x.Expenditure)
            .ToArray();

        return new BenchmarkingViewModelCostSubCategory<HighNeedsSpendingComparisonDatum>
        {
            Uuid = Guid.NewGuid().ToString(),
            SubCategory = filter.GetHeading(),
            SubCategoryId = (int)filter,
            LineCodes = filter.GetLineCodes(),
            AdditionalInfo = filter.GetAdditionalInfo(),
            Data = data
        };
    }
}

public class HighNeedsSpendingComparisonGroup
{
    public string Group { get; init; } = "";
    public string? AdditionalText { get; set; }
    public List<BenchmarkingViewModelCostSubCategory<HighNeedsSpendingComparisonDatum>> Items { get; init; } = [];
    public int SelectedCount(HashSet<int> selectedIds) =>
        Items.Count(i => i.SubCategoryId.HasValue && selectedIds.Contains(i.SubCategoryId.Value));
    public IEnumerable<BenchmarkingViewModelCostSubCategory<HighNeedsSpendingComparisonDatum>> SelectedItems(HashSet<int> selectedIds) =>
        Items.Where(i => i.SubCategoryId.HasValue && selectedIds.Contains(i.SubCategoryId.Value));
}