using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;
using Web.App.Domain;
namespace Web.App.ViewModels;

public record UserDefinedCharacteristicViewModel()
{
    public UserDefinedCharacteristicViewModel(SchoolCharacteristic? characteristic) : this()
    {
        // default characteristics
        FinanceType = characteristic?.FinanceType switch
        {
            EstablishmentTypes.Academies => "Academies",
            EstablishmentTypes.Maintained => "Maintained schools",
            _ => "Both"
        };

        OverallPhase =
        [
            characteristic?.OverallPhase switch
            {
                OverallPhaseTypes.Nursery => "Nursery",
                OverallPhaseTypes.Primary => "Primary",
                OverallPhaseTypes.Secondary => "Secondary",
                OverallPhaseTypes.PupilReferralUnit => "Pupil referral units or alternative provision schools",
                OverallPhaseTypes.Special => "Special",
                _ => "University technical college"
            }
        ];

        LaSelection = characteristic == null ? null : "This";
    }

    // default characteristics
    [Required(ErrorMessage = "Select a school type")]
    public string? FinanceType { get; set; }

    [Required(ErrorMessage = "Select at least one school category")]
    public string?[]? OverallPhase { get; set; }

    // todo: support multiple LAs (see #212642)
    [Required(ErrorMessage = "Select a local authority")]
    public string? LaSelection { get; set; }

    [RequiredDepends(nameof(LaSelection), "Choose", ErrorMessage = "Select a local authority from the suggester")]
    public string? LaInput { get; init; }

    [RequiredDepends(nameof(LaSelection), "Choose", ErrorMessage = "Select a local authority from the suggester")]
    public string? Code { get; init; }

    // number of pupils
    public string? TotalPupils { get; init; }

    [Display(Name = "Number of pupils from")]
    [RequiredDepends(nameof(TotalPupils), "true", ErrorMessage = "Enter the number of pupils from")]
    [Range(0, 10_000, ErrorMessage = "Enter number of pupils from between 0 and 10,000")]
    public int? TotalPupilsFrom { get; init; }

    [Display(Name = "Number of pupils to")]
    [RequiredDepends(nameof(TotalPupils), "true", ErrorMessage = "Enter the number of pupils to")]
    [Range(0, 10_000, ErrorMessage = "Enter number of pupils to between 0 and 10,000")]
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

    // london weighting
    public string? LondonWeighting { get; init; }

    [RequiredDepends(nameof(LondonWeighting), "true", ErrorMessage = "Select one or more London weightings")]
    public string[] LondonWeightings { get; init; } = [];

    // building age
    public string? AverageBuildingAge { get; init; }

    [Display(Name = "Average building age from")]
    [RequiredDepends(nameof(AverageBuildingAge), "true", ErrorMessage = "Enter the average building age from")]
    [Range(0, 100, ErrorMessage = "Enter average building age from between 0 and 100")]
    public int? AverageBuildingAgeFrom { get; init; }

    [Display(Name = "Average building age to")]
    [RequiredDepends(nameof(AverageBuildingAge), "true", ErrorMessage = "Enter the average building age to")]
    [Range(0, 100, ErrorMessage = "Enter average building age to between 0 and 100")]
    [CompareIntValue(nameof(AverageBuildingAgeFrom), Operator.GreaterThanOrEqualTo)]
    public int? AverageBuildingAgeTo { get; init; }

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

    // ofsted
    public string? OfstedRating { get; init; }

    [RequiredDepends(nameof(OfstedRating), "true", ErrorMessage = "Select one or more Ofsted ratings")]
    public string[] OfstedRatings { get; init; } = [];

    // number of pupils
    public string? SchoolsInTrust { get; init; }

    [Display(Name = "Number of schools within trust from")]
    [RequiredDepends(nameof(SchoolsInTrust), "true", ErrorMessage = "Enter the number of schools within trust from")]
    [Range(0, 1_000, ErrorMessage = "Enter number of schools within trust from between 0 and 1,000")]
    public int? SchoolsInTrustFrom { get; init; }

    [Display(Name = "Number of schools within trust to")]
    [RequiredDepends(nameof(SchoolsInTrust), "true", ErrorMessage = "Enter the number of schools within trust to")]
    [Range(0, 1_000, ErrorMessage = "Enter number of schools within trust to between 0 and 1,000")]
    [CompareIntValue(nameof(SchoolsInTrustFrom), Operator.GreaterThanOrEqualTo)]
    public int? SchoolsInTrustTo { get; init; }

    // deficit
    public string? Deficit { get; init; }

    [RequiredDepends(nameof(Deficit), "true", ErrorMessage = "Select whether in deficit")]
    public string[] Deficits { get; init; } = [];

    // pfi
    public string? PrivateFinanceInitiative { get; init; }

    [RequiredDepends(nameof(PrivateFinanceInitiative), "true", ErrorMessage = "Select whether part of PFI")]
    public string[] PrivateFinanceInitiatives { get; init; } = [];

    // number of sixth form pupils
    public string? TotalPupilsSixthForm { get; init; }

    [Display(Name = "Number of sixth form pupils from")]
    [RequiredDepends(nameof(TotalPupilsSixthForm), "true", ErrorMessage = "Enter the number of sixth form pupils from")]
    [Range(0, 10_000, ErrorMessage = "Enter number of sixth form pupils from between 0 and 10,000")]
    public int? TotalPupilsSixthFormFrom { get; init; }

    [Display(Name = "Number of sixth form pupils to")]
    [RequiredDepends(nameof(TotalPupilsSixthForm), "true", ErrorMessage = "Enter the number of sixth form pupils to")]
    [Range(0, 10_000, ErrorMessage = "Enter number of sixth form pupils to between 0 and 10,000")]
    [CompareIntValue(nameof(TotalPupilsSixthFormFrom), Operator.GreaterThanOrEqualTo)]
    public int? TotalPupilsSixthFormTo { get; init; }

    // ks2
    public string? KeyStage2Progress { get; init; }

    [Display(Name = "Key stage 2 progress from")]
    [RequiredDepends(nameof(KeyStage2Progress), "true", ErrorMessage = "Enter the key stage 2 progress from")]
    [Range(-20, 20, ErrorMessage = "Enter key stage 2 progress from between -20 and 20")]
    public decimal? KeyStage2ProgressFrom { get; init; }

    [Display(Name = "Key stage 2 progress to")]
    [RequiredDepends(nameof(KeyStage2Progress), "true", ErrorMessage = "Enter the key stage 2 progress to")]
    [Range(-20, 20, ErrorMessage = "Enter key stage 2 progress to between -20 and 20")]
    [CompareDecimalValue(nameof(KeyStage2ProgressFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? KeyStage2ProgressTo { get; init; }

    // ks4
    public string? KeyStage4Progress { get; init; }

    [Display(Name = "Key stage 4 progress from")]
    [RequiredDepends(nameof(KeyStage4Progress), "true", ErrorMessage = "Enter the key stage 4 progress from")]
    [Range(-20, 20, ErrorMessage = "Enter key stage 4 progress from between -20 and 20")]
    public decimal? KeyStage4ProgressFrom { get; init; }

    [Display(Name = "Key stage 4 progress to")]
    [RequiredDepends(nameof(KeyStage4Progress), "true", ErrorMessage = "Enter the key stage 4 progress to")]
    [Range(-20, 20, ErrorMessage = "Enter key stage 4 progress to between -20 and 20")]
    [CompareDecimalValue(nameof(KeyStage4ProgressFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? KeyStage4ProgressTo { get; init; }
}