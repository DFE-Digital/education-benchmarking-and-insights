using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Web.App.ViewModels;

public record UserDefinedTrustCharacteristicViewModel : IValidatableObject
{
    // number of pupils
    public string? TotalPupils { get; init; }

    [Display(Name = "Number of pupils from")]
    [RequiredDepends(nameof(TotalPupils), "true", ErrorMessage = "Enter the number of pupils from")]
    [Range(0, 100_000, ErrorMessage = "Enter number of pupils from between 0 and 100,000")]
    public int? TotalPupilsFrom { get; init; }

    [Display(Name = "Number of pupils to")]
    [RequiredDepends(nameof(TotalPupils), "true", ErrorMessage = "Enter the number of pupils to")]
    [Range(0, 100_000, ErrorMessage = "Enter number of pupils to between 0 and 100,000")]
    [CompareIntValue(nameof(TotalPupilsFrom), Operator.GreaterThanOrEqualTo)]
    public int? TotalPupilsTo { get; init; }

    // number of schools
    public string? SchoolsInTrust { get; init; }

    [Display(Name = "Number of schools from")]
    [RequiredDepends(nameof(SchoolsInTrust), "true", ErrorMessage = "Enter the number of schools from")]
    [Range(0, 1_000, ErrorMessage = "Enter number of schools from between 0 and 1,000")]
    public int? SchoolsInTrustFrom { get; init; }

    [Display(Name = "Number of schools to")]
    [RequiredDepends(nameof(SchoolsInTrust), "true", ErrorMessage = "Enter the number of schools to")]
    [Range(0, 1_000, ErrorMessage = "Enter number of schools to between 0 and 1,000")]
    [CompareIntValue(nameof(SchoolsInTrustFrom), Operator.GreaterThanOrEqualTo)]
    public int? SchoolsInTrustTo { get; init; }

    // income
    public string? TotalIncome { get; init; }

    [Display(Name = "Total income from")]
    [RequiredDepends(nameof(TotalIncome), "true", ErrorMessage = "Enter the total income from")]
    [Range(0, 500_000_000, ErrorMessage = "Enter total income from between 0 and 500,000,000")]
    public int? TotalIncomeFrom { get; init; }

    [Display(Name = "Total income to")]
    [RequiredDepends(nameof(TotalIncome), "true", ErrorMessage = "Enter the total income to")]
    [Range(0, 500_000_000, ErrorMessage = "Enter total income to between 0 and 500,000,000")]
    [CompareIntValue(nameof(TotalIncomeFrom), Operator.GreaterThanOrEqualTo)]
    public int? TotalIncomeTo { get; init; }

    // floor area
    public string? InternalFloorArea { get; init; }

    [Display(Name = "Gross internal floor area from")]
    [RequiredDepends(nameof(InternalFloorArea), "true", ErrorMessage = "Enter the gross internal floor area from")]
    [Range(0, 100_000, ErrorMessage = "Enter gross internal floor area from between 0 and 100,000")]
    public int? InternalFloorAreaFrom { get; init; }

    [Display(Name = "Gross internal floor area to")]
    [RequiredDepends(nameof(InternalFloorArea), "true", ErrorMessage = "Enter the gross internal floor area to")]
    [Range(0, 100_000, ErrorMessage = "Enter gross internal floor area to between 0 and 100,000")]
    [CompareIntValue(nameof(InternalFloorAreaFrom), Operator.GreaterThanOrEqualTo)]
    public int? InternalFloorAreaTo { get; init; }

    // overall phase
    public string? OverallPhase { get; init; }

    [RequiredDepends(nameof(OverallPhase), "true", ErrorMessage = "Select at least one school phase")]
    public string?[]? OverallPhases { get; set; }

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

    // income
    public string? FormationYear { get; init; }

    [Display(Name = "Formation year from")]
    [RequiredDepends(nameof(FormationYear), "true", ErrorMessage = "Enter the formation year from")]
    [Range(2000, 2050, ErrorMessage = "Enter formation year from between 2000 and 2050")]
    public int? FormationYearFrom { get; init; }

    [Display(Name = "Formation year to")]
    [RequiredDepends(nameof(FormationYear), "true", ErrorMessage = "Enter the formation year to")]
    [Range(2000, 2050, ErrorMessage = "Enter formation year to between 2000 and 2050")]
    [CompareIntValue(nameof(FormationYearFrom), Operator.GreaterThanOrEqualTo)]
    public int? FormationYearTo { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var selectableFields = new[]
        {
            TotalPupils,
            SchoolsInTrust,
            TotalIncome,
            InternalFloorArea,
            OverallPhase,
            FreeSchoolMeals,
            SpecialEducationalNeeds,
            FormationYear
        };

        if (selectableFields.All(f => f != "true"))
        {
            yield return new ValidationResult("Select one or more characteristics");
        }
    }
}