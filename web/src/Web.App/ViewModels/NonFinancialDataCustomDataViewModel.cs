using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;

namespace Web.App.ViewModels;

public interface INonFinancialDataCustomDataViewModel
{
    decimal? TotalNumberOfTeachersFte { get; } // todo: replace with FTE pupils
    decimal? FreeSchoolMealPercent { get; }
    decimal? SpecialEducationalNeedsPercent { get; }
    int? FloorArea { get; }
}

public record NonFinancialDataCustomDataViewModel : INonFinancialDataCustomDataViewModel
{
    public decimal? TotalNumberOfTeachersFte { get; init; } // todo: replace with FTE pupils

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.FreeSchoolMealPercent)]
    public decimal? FreeSchoolMealPercent { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.SpecialEducationalNeedsPercent)]
    public decimal? SpecialEducationalNeedsPercent { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.FloorArea)]
    public int? FloorArea { get; init; }
}