using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;
using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparatorsByCharacteristicViewModel(School school, SchoolCharacteristic? characteristic)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolCharacteristic? Characteristic => characteristic;
    public UserDefinedCharacteristicViewModel Data => new()
    {
        FinanceType = characteristic?.FinanceType,
        OverallPhase = [Characteristic?.OverallPhase],
        LaSelection = characteristic == null ? null : "This"
    };
}

public record UserDefinedCharacteristicViewModel
{
    // default characteristics
    [Required(ErrorMessage = "Select a school type")]
    public string? FinanceType { get; init; }

    [Required(ErrorMessage = "Select at least one school category")]
    public string?[]? OverallPhase { get; init; }

    [Required(ErrorMessage = "Select a local authority")]
    public string? LaSelection { get; init; }

    [RequiredDepends(nameof(LaSelection), "Choose", ErrorMessage = "Select a local authority from the suggester")]
    public string? LaInput { get; init; }

    [RequiredDepends(nameof(LaSelection), "Choose", ErrorMessage = "Select a local authority from the suggester")]
    public string? Code { get; init; }

    // number of pupils
    public string? TotalPupils { get; init; }

    [Display(Name = "Number of pupils from")]
    [RequiredDepends(nameof(TotalPupils), "true", ErrorMessage = "Enter the number of pupils from")]
    [Range(1, 10000, ErrorMessage = "Enter number of pupils from between 1 and 10,000")]
    public int? TotalPupilsFrom { get; init; }

    [Display(Name = "Number of pupils to")]
    [RequiredDepends(nameof(TotalPupils), "true", ErrorMessage = "Enter the number of pupils to")]
    [Range(1, 10000, ErrorMessage = "Enter number of pupils to between 1 and 10,000")]
    [CompareIntValue(nameof(TotalPupilsFrom), Operator.GreaterThanOrEqualTo)]
    public int? TotalPupilsTo { get; init; }

    // fsm
    public string? FreeSchoolMeals { get; init; }

    [Display(Name = "Free school meals eligibility from")]
    [RequiredDepends(nameof(FreeSchoolMeals), "true", ErrorMessage = "Enter the free school meals eligibility from")]
    [Range(0, 100, ErrorMessage = "Enter free school meals eligibility from between 0 and 100")]
    public decimal? FreeSchoolMealsFrom { get; init; }

    [Display(Name = "Free school meals eligibility to")]
    [RequiredDepends(nameof(FreeSchoolMeals), "true", ErrorMessage = "Enter the free school meals eligibility to")]
    [Range(0, 100, ErrorMessage = "Enter free school meals eligibility to between 0 and 100")]
    [CompareDecimalValue(nameof(FreeSchoolMealsFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? FreeSchoolMealsTo { get; init; }

    // sen
    public string? SpecialEducationalNeeds { get; init; }

    [Display(Name = "Special educational needs from")]
    [RequiredDepends(nameof(SpecialEducationalNeeds), "true", ErrorMessage = "Enter the special educational needs eligibility from")]
    [Range(0, 100, ErrorMessage = "Enter special educational needs from between 0 and 100")]
    public decimal? SpecialEducationalNeedsFrom { get; init; }

    [Display(Name = "Special educational needs to")]
    [RequiredDepends(nameof(SpecialEducationalNeeds), "true", ErrorMessage = "Enter the special educational needs eligibility to")]
    [Range(0, 100, ErrorMessage = "Enter special educational needs to between 0 and 100")]
    [CompareDecimalValue(nameof(SpecialEducationalNeedsFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? SpecialEducationalNeedsTo { get; init; }
}