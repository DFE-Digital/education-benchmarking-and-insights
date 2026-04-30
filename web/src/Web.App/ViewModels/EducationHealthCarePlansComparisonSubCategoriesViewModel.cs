using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels;

public class EducationHealthCarePlansComparisonSubCategoriesViewModel
{
    public List<BenchmarkingViewModelCostSubCategory<EducationHealthCarePlansComparisonDatum>> Items { get; set; } = [];

    public EducationHealthCarePlansComparisonSubCategoriesViewModel(EducationHealthCarePlans[] plans,
        EducationHealthCarePlansCategories.SubCategoryFilter[] filters,
        string code)
    {
        filters = filters.Length > 0 ? filters : EducationHealthCarePlansCategories.All;

        foreach (var filter in filters)
        {
            AddSubCategory(filter, plans, code);
        }
    }

    private void AddSubCategory(EducationHealthCarePlansCategories.SubCategoryFilter filter, EducationHealthCarePlans[] plans, string code)
    {
        var data = plans
            .Select(p => new EducationHealthCarePlansComparisonDatum
            {
                Code = p.Code,
                Name = p.Name,
                Plans = filter.GetValue(p),
                TotalPupils = p.TotalPupils
            })
            .Where(x => x.Code == code || x.Plans != null)
            .OrderByDescending(x => x.Plans)
            .ToArray();

        var uuid = Guid.NewGuid().ToString();

        Items.Add(new BenchmarkingViewModelCostSubCategory<EducationHealthCarePlansComparisonDatum>
        {
            Uuid = uuid,
            SubCategory = filter.GetHeading(),
            TableHeading = filter.GetTableHeading(),
            Data = data
        });
    }
}