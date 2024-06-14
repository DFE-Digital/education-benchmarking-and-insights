using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Domain.Insight;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Web.App.ViewModels;

public record UserDefinedSchoolCharacteristicViewModel() : IValidatableObject
{
    public UserDefinedSchoolCharacteristicViewModel(SchoolCharacteristic? characteristic) : this()
    {
        FinanceType = "Both";
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
        LaSelection = "All";
    }

    // default characteristics
    [Required(ErrorMessage = "Select a school type")]
    public string? FinanceType { get; set; }

    [Required(ErrorMessage = "Select at least one school category")]
    public string?[]? OverallPhase { get; set; }

    [Required(ErrorMessage = "Select a local authority")]
    public string? LaSelection { get; set; }
    public string? LaInput { get; set; }
    public string? Code { get; set; }
    public string[] LaNames { get; set; } = [];
    public bool? LaNamesMutated { get; set; }

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

    // number of schools
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

    // spld
    public string? SpecificLearningDifficulty { get; init; }

    [Display(Name = "Specific learning difficulty from")]
    [RequiredDepends(nameof(SpecificLearningDifficulty), "true", ErrorMessage = "Enter the specific learning difficulty from")]
    [Range(0, 100, ErrorMessage = "Enter specific learning difficulty from between 0 and 100")]
    public decimal? SpecificLearningDifficultyFrom { get; init; }

    [Display(Name = "Specific learning difficulty to")]
    [RequiredDepends(nameof(SpecificLearningDifficulty), "true", ErrorMessage = "Enter the specific learning difficulty to")]
    [Range(0, 100, ErrorMessage = "Enter specific learning difficulty to between 0 and 100")]
    [CompareDecimalValue(nameof(SpecificLearningDifficultyFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? SpecificLearningDifficultyTo { get; init; }

    // mld
    public string? ModerateLearningDifficulty { get; init; }

    [Display(Name = "Moderate learning difficulty from")]
    [RequiredDepends(nameof(ModerateLearningDifficulty), "true", ErrorMessage = "Enter the moderate learning difficulty from")]
    [Range(0, 100, ErrorMessage = "Enter moderate learning difficulty from between 0 and 100")]
    public decimal? ModerateLearningDifficultyFrom { get; init; }

    [Display(Name = "Moderate learning difficulty to")]
    [RequiredDepends(nameof(ModerateLearningDifficulty), "true", ErrorMessage = "Enter the moderate learning difficulty to")]
    [Range(0, 100, ErrorMessage = "Enter moderate learning difficulty to between 0 and 100")]
    [CompareDecimalValue(nameof(ModerateLearningDifficultyFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? ModerateLearningDifficultyTo { get; init; }

    // sld
    public string? SevereLearningDifficulty { get; init; }

    [Display(Name = "Severe learning difficulty from")]
    [RequiredDepends(nameof(SevereLearningDifficulty), "true", ErrorMessage = "Enter the severe learning difficulty from")]
    [Range(0, 100, ErrorMessage = "Enter severe learning difficulty from between 0 and 100")]
    public decimal? SevereLearningDifficultyFrom { get; init; }

    [Display(Name = "Severe learning difficulty to")]
    [RequiredDepends(nameof(SevereLearningDifficulty), "true", ErrorMessage = "Enter the severe learning difficulty to")]
    [Range(0, 100, ErrorMessage = "Enter severe learning difficulty to between 0 and 100")]
    [CompareDecimalValue(nameof(SevereLearningDifficultyFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? SevereLearningDifficultyTo { get; init; }

    // pmld
    public string? ProfoundMultipleLearningDifficulty { get; init; }

    [Display(Name = "Profound and multiple learning difficulty")]
    [RequiredDepends(nameof(ProfoundMultipleLearningDifficulty), "true", ErrorMessage = "Enter the profound and multiple learning difficulty from")]
    [Range(0, 100, ErrorMessage = "Enter profound and multiple learning difficulty from between 0 and 100")]
    public decimal? ProfoundMultipleLearningDifficultyFrom { get; init; }

    [Display(Name = "Profound and multiple learning difficulty to")]
    [RequiredDepends(nameof(ProfoundMultipleLearningDifficulty), "true", ErrorMessage = "Enter the profound and multiple learning difficulty to")]
    [Range(0, 100, ErrorMessage = "Enter profound and multiple learning difficulty to between 0 and 100")]
    [CompareDecimalValue(nameof(ProfoundMultipleLearningDifficultyFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? ProfoundMultipleLearningDifficultyTo { get; init; }

    // semh
    public string? SocialEmotionalMentalHealth { get; init; }

    [Display(Name = "Social, emotional and mental health from")]
    [RequiredDepends(nameof(SocialEmotionalMentalHealth), "true", ErrorMessage = "Enter the social, emotional and mental health from")]
    [Range(0, 100, ErrorMessage = "Enter social, emotional and mental health from between 0 and 100")]
    public decimal? SocialEmotionalMentalHealthFrom { get; init; }

    [Display(Name = "Social, emotional and mental health to")]
    [RequiredDepends(nameof(SocialEmotionalMentalHealth), "true", ErrorMessage = "Enter the social, emotional and mental health to")]
    [Range(0, 100, ErrorMessage = "Enter social, emotional and mental health to between 0 and 100")]
    [CompareDecimalValue(nameof(SocialEmotionalMentalHealthFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? SocialEmotionalMentalHealthTo { get; init; }

    // slcn
    public string? SpeechLanguageCommunication { get; init; }

    [Display(Name = "Speech, language and communications needs from")]
    [RequiredDepends(nameof(SpeechLanguageCommunication), "true", ErrorMessage = "Enter the speech, language and communications needs from")]
    [Range(0, 100, ErrorMessage = "Enter speech, language and communications needs from between 0 and 100")]
    public decimal? SpeechLanguageCommunicationFrom { get; init; }

    [Display(Name = "Speech, language and communications needs to")]
    [RequiredDepends(nameof(SpeechLanguageCommunication), "true", ErrorMessage = "Enter the speech, language and communications needs to")]
    [Range(0, 100, ErrorMessage = "Enter speech, language and communications needs to between 0 and 100")]
    [CompareDecimalValue(nameof(SpeechLanguageCommunicationFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? SpeechLanguageCommunicationTo { get; init; }

    // hi
    public string? HearingImpairment { get; init; }

    [Display(Name = "Hearing impairment from")]
    [RequiredDepends(nameof(HearingImpairment), "true", ErrorMessage = "Enter the hearing impairment from")]
    [Range(0, 100, ErrorMessage = "Enter hearing impairment from between 0 and 100")]
    public decimal? HearingImpairmentFrom { get; init; }

    [Display(Name = "Hearing impairment to")]
    [RequiredDepends(nameof(HearingImpairment), "true", ErrorMessage = "Enter the hearing impairment to")]
    [Range(0, 100, ErrorMessage = "Enter hearing impairment to between 0 and 100")]
    [CompareDecimalValue(nameof(HearingImpairmentFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? HearingImpairmentTo { get; init; }

    // vi
    public string? VisualImpairment { get; init; }

    [Display(Name = "Visual impairment from")]
    [RequiredDepends(nameof(VisualImpairment), "true", ErrorMessage = "Enter the visual impairment from")]
    [Range(0, 100, ErrorMessage = "Enter visual impairment from between 0 and 100")]
    public decimal? VisualImpairmentFrom { get; init; }

    [Display(Name = "Visual impairment to")]
    [RequiredDepends(nameof(VisualImpairment), "true", ErrorMessage = "Enter the visual impairment to")]
    [Range(0, 100, ErrorMessage = "Enter visual impairment to between 0 and 100")]
    [CompareDecimalValue(nameof(VisualImpairmentFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? VisualImpairmentTo { get; init; }

    // msi
    public string? MultiSensoryImpairment { get; init; }

    [Display(Name = "Multi-sensory impairment from")]
    [RequiredDepends(nameof(MultiSensoryImpairment), "true", ErrorMessage = "Enter the multi-sensory impairment from")]
    [Range(0, 100, ErrorMessage = "Enter multi-sensory impairment from between 0 and 100")]
    public decimal? MultiSensoryImpairmentFrom { get; init; }

    [Display(Name = "Multi-sensory impairment to")]
    [RequiredDepends(nameof(MultiSensoryImpairment), "true", ErrorMessage = "Enter the multi-sensory impairment to")]
    [Range(0, 100, ErrorMessage = "Enter multi-sensory impairment to between 0 and 100")]
    [CompareDecimalValue(nameof(MultiSensoryImpairmentFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? MultiSensoryImpairmentTo { get; init; }

    // pd
    public string? PhysicalDisability { get; init; }

    [Display(Name = "Physical disability from")]
    [RequiredDepends(nameof(PhysicalDisability), "true", ErrorMessage = "Enter the physical disability from")]
    [Range(0, 100, ErrorMessage = "Enter physical disability from between 0 and 100")]
    public decimal? PhysicalDisabilityFrom { get; init; }

    [Display(Name = "Physical disability to")]
    [RequiredDepends(nameof(PhysicalDisability), "true", ErrorMessage = "Enter the physical disability to")]
    [Range(0, 100, ErrorMessage = "Enter physical disability to between 0 and 100")]
    [CompareDecimalValue(nameof(PhysicalDisabilityFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? PhysicalDisabilityTo { get; init; }

    // asd
    public string? AutisticSpectrumDisorder { get; init; }

    [Display(Name = "Autistic spectrum disorder from")]
    [RequiredDepends(nameof(AutisticSpectrumDisorder), "true", ErrorMessage = "Enter the autistic spectrum disorder from")]
    [Range(0, 100, ErrorMessage = "Enter autistic spectrum disorder from between 0 and 100")]
    public decimal? AutisticSpectrumDisorderFrom { get; init; }

    [Display(Name = "Autistic spectrum disorder to")]
    [RequiredDepends(nameof(AutisticSpectrumDisorder), "true", ErrorMessage = "Enter the autistic spectrum disorder to")]
    [Range(0, 100, ErrorMessage = "Enter autistic spectrum disorder to between 0 and 100")]
    [CompareDecimalValue(nameof(AutisticSpectrumDisorderFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? AutisticSpectrumDisorderTo { get; init; }

    // other
    public string? OtherLearningDifficulty { get; init; }

    [Display(Name = "Other learning difficulty from")]
    [RequiredDepends(nameof(OtherLearningDifficulty), "true", ErrorMessage = "Enter the other learning difficulty from")]
    [Range(0, 100, ErrorMessage = "Enter other learning difficulty from between 0 and 100")]
    public decimal? OtherLearningDifficultyFrom { get; init; }

    [Display(Name = "Other learning difficulty to")]
    [RequiredDepends(nameof(OtherLearningDifficulty), "true", ErrorMessage = "Enter the other learning difficulty to")]
    [Range(0, 100, ErrorMessage = "Enter other learning difficulty to between 0 and 100")]
    [CompareDecimalValue(nameof(OtherLearningDifficultyFrom), Operator.GreaterThanOrEqualTo)]
    public decimal? OtherLearningDifficultyTo { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (LaSelection == "Choose" &&
            !(!string.IsNullOrWhiteSpace(LaInput) && !string.IsNullOrWhiteSpace(Code) || LaNames.Length > 0))
        {
            yield return new ValidationResult("Select a local authority from the suggester", [nameof(Code)]);
        }
    }
}