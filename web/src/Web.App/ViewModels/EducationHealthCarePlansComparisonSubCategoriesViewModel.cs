using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels;

public class EducationHealthCarePlansComparisonSubCategoriesViewModel
{
    public List<BenchmarkingViewModelCostSubCategory<EducationHealthCarePlansComparisonDatum>> Items { get; set; } = [];

    public EducationHealthCarePlansComparisonSubCategoriesViewModel(EducationHealthCarePlans[] plans,
        EducationHealthCarePlansCategories.SubCategoryFilter[] filters)
    {
        filters = filters.Length > 0 ? filters : EducationHealthCarePlansCategories.All;

        foreach (var filter in filters)
        {
            AddSubCategory(filter, plans);
        }
    }

    private void AddSubCategory(EducationHealthCarePlansCategories.SubCategoryFilter filter, EducationHealthCarePlans[] plans)
    {
        var data = plans
            .Select(p => new EducationHealthCarePlansComparisonDatum
            {
                Name = p.Name,
                Plans = filter.GetValue(p),
                TotalPupils = p.TotalPupils
            })
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

// TODO: as part of chart wiring up move to Domain/Charts
public class EducationHealthCarePlansComparisonDatum
{
    public string? Name { get; set; }
    public decimal? Plans { get; set; }
    public decimal? TotalPupils { get; set; }
}