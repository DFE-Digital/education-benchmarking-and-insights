using Web.App.Domain;
using Web.App.Domain.Charts;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels;

public class SchoolComparisonItSpendViewModel(
    School school,
    SchoolComparisonSubCategoriesViewModel subCategories,
    SchoolItSpend[] expenditures)
{
    public static readonly Dimensions.ResultAsOptions[] FilterDimensions =
    [
        Dimensions.ResultAsOptions.SpendPerPupil,
        Dimensions.ResultAsOptions.Actuals,
        Dimensions.ResultAsOptions.PercentExpenditure,
        Dimensions.ResultAsOptions.PercentIncome
    ];

    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public List<BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum>> SubCategories => subCategories.Items;

    public SchoolChartTooltipData[] TooltipData => expenditures.Select(x => new SchoolChartTooltipData
    {
        Urn = x.URN,
        SchoolName = x.SchoolName,
        LAName = x.LAName,
        TotalPupils = x.TotalPupils,
        PeriodCoveredByReturn = x.PeriodCoveredByReturn
    }).ToArray();

    public SchoolChartPartYearData[] PartYearData => SubCategories
        .Where(c => c.Data != null)
        .SelectMany(c => c.Data!.Where(d => d.PeriodCoveredByReturn is not 12))
        .DistinctBy(x => x.Urn)
        .Select(x => new SchoolChartPartYearData
        {
            SchoolName = x.SchoolName,
            PeriodCoveredByReturn = x.PeriodCoveredByReturn
        }).ToArray();

    public Views.ViewAsOptions ViewAs { get; set; } = Views.ViewAsOptions.Chart;
    public Dimensions.ResultAsOptions ResultAs { get; set; } = Dimensions.ResultAsOptions.SpendPerPupil;
    public ItSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; set; } = [];
}

public class SchoolComparisonItSpendTableViewModel(
    string? urn,
    Dimensions.ResultAsOptions resultAs,
    BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum> subCategory)
{
    public string? Urn => urn;
    public Dimensions.ResultAsOptions ResultAs => resultAs;
    public BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum> SubCategory => subCategory;
}

public class SchoolComparisonItSpendChartViewModel(
    Guid uuid,
    BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum> subCategory)
{
    public Guid Uuid => uuid;
    public BenchmarkingViewModelCostSubCategory<SchoolComparisonDatum> SubCategory => subCategory;
}

public record SchoolChartPartYearData
{
    public string? SchoolName { get; init; }
    public int? PeriodCoveredByReturn { get; init; }
}