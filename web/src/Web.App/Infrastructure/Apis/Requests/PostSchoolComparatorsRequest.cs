using Web.App.Domain;
using Web.App.ViewModels;
// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
namespace Web.App.Infrastructure.Apis;

public class PostSchoolComparatorsRequest(string urn, string? laName, UserDefinedCharacteristicViewModel viewModel)
{
    public string Target => urn;

    public CharacteristicList? FinanceType => string.IsNullOrWhiteSpace(viewModel.FinanceType)
        ? null
        : new CharacteristicList(viewModel.FinanceType switch
        {
            "Both" =>
            [
                EstablishmentTypes.Academies,
                EstablishmentTypes.Maintained
            ],
            "Academies" =>
            [
                EstablishmentTypes.Academies
            ],
            "Maintained schools" =>
            [
                EstablishmentTypes.Maintained
            ],
            _ => throw new ArgumentOutOfRangeException(nameof(FinanceType))
        });

    public CharacteristicList? OverallPhase => viewModel.OverallPhase is { Length: > 0 }
        ? new CharacteristicList(viewModel.OverallPhase
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>()
            .Select(value => value switch
            {
                "Nursery" => OverallPhaseTypes.Nursery,
                "Primary" => OverallPhaseTypes.Primary,
                "Secondary" => OverallPhaseTypes.Secondary,
                "Pupil referral units or alternative provision schools" => OverallPhaseTypes.PupilReferralUnit,
                "Special" => OverallPhaseTypes.Special,
                _ => value
            })
            .ToArray())
        : null;

    public CharacteristicList? LAName => viewModel.LaSelection == "All"
        ? null
        : new CharacteristicList(viewModel.LaSelection switch
        {
            "Choose" => viewModel.LaNames,
            _ => [laName!]
        });

    public CharacteristicList? SchoolPosition => IsSelected(viewModel.Deficit)
        ? new CharacteristicList(viewModel.Deficits.Contains("Include schools in deficit") ? "Deficit" : "Surplus")
        : null;

    public CharacteristicValueBool? IsPFISchool => IsSelected(viewModel.PrivateFinanceInitiative)
        ? new CharacteristicValueBool(viewModel.PrivateFinanceInitiatives.Contains("Part of PFI"))
        : null;

    public CharacteristicList? LondonWeighting => IsSelected(viewModel.LondonWeighting)
        ? new CharacteristicList(viewModel.LondonWeightings)
        : null;

    public CharacteristicList? OfstedDescription => IsSelected(viewModel.OfstedRating)
        ? new CharacteristicList(viewModel.OfstedRatings)
        : null;

    public CharacteristicRange? TotalPupils => IsSelected(viewModel.TotalPupils)
        ? new CharacteristicRange(viewModel.TotalPupilsFrom, viewModel.TotalPupilsTo)
        : null;

    public CharacteristicRange? BuildingAverageAge => IsSelected(viewModel.AverageBuildingAge)
        ? new CharacteristicRange(viewModel.AverageBuildingAgeFrom, viewModel.AverageBuildingAgeTo)
        : null;

    public CharacteristicRange? TotalInternalFloorArea => IsSelected(viewModel.InternalFloorArea)
        ? new CharacteristicRange(viewModel.InternalFloorAreaFrom, viewModel.InternalFloorAreaTo)
        : null;

    public CharacteristicRange? PercentFreeSchoolMeals => IsSelected(viewModel.FreeSchoolMeals)
        ? new CharacteristicRange(viewModel.FreeSchoolMealsFrom, viewModel.FreeSchoolMealsTo)
        : null;

    public CharacteristicRange? PercentSpecialEducationNeeds => IsSelected(viewModel.SpecialEducationalNeeds)
        ? new CharacteristicRange(viewModel.SpecialEducationalNeedsFrom, viewModel.SpecialEducationalNeedsTo)
        : null;

    public CharacteristicRange? TotalPupilsSixthForm => IsSelected(viewModel.TotalPupilsSixthForm)
        ? new CharacteristicRange(viewModel.TotalPupilsSixthFormFrom, viewModel.TotalPupilsSixthFormTo)
        : null;

    public CharacteristicRange? KS2Progress => IsSelected(viewModel.KeyStage2Progress)
        ? new CharacteristicRange(viewModel.KeyStage2ProgressFrom, viewModel.KeyStage2ProgressTo)
        : null;

    public CharacteristicRange? KS4Progress => IsSelected(viewModel.KeyStage4Progress)
        ? new CharacteristicRange(viewModel.KeyStage4ProgressFrom, viewModel.KeyStage4ProgressTo)
        : null;

    public CharacteristicRange? SchoolsInTrust => IsSelected(viewModel.SchoolsInTrust)
        ? new CharacteristicRange(viewModel.SchoolsInTrustFrom, viewModel.SchoolsInTrustTo)
        : null;

    public CharacteristicRange? PercentWithVI => IsSelected(viewModel.VisualImpairment)
        ? new CharacteristicRange(viewModel.VisualImpairmentFrom, viewModel.VisualImpairmentTo)
        : null;

    public CharacteristicRange? PercentWithSPLD => IsSelected(viewModel.SpecificLearningDifficulty)
        ? new CharacteristicRange(viewModel.SpecificLearningDifficultyFrom, viewModel.SpecificLearningDifficultyTo)
        : null;

    public CharacteristicRange? PercentWithSLD => IsSelected(viewModel.SevereLearningDifficulty)
        ? new CharacteristicRange(viewModel.SevereLearningDifficultyFrom, viewModel.SevereLearningDifficultyTo)
        : null;

    public CharacteristicRange? PercentWithSLCN => IsSelected(viewModel.SpeechLanguageCommunication)
        ? new CharacteristicRange(viewModel.SpeechLanguageCommunicationFrom, viewModel.SpeechLanguageCommunicationTo)
        : null;

    public CharacteristicRange? PercentWithSEMH => IsSelected(viewModel.SocialEmotionalMentalHealth)
        ? new CharacteristicRange(viewModel.SocialEmotionalMentalHealthFrom, viewModel.SocialEmotionalMentalHealthTo)
        : null;

    public CharacteristicRange? PercentWithPMLD => IsSelected(viewModel.ProfoundMultipleLearningDifficulty)
        ? new CharacteristicRange(viewModel.ProfoundMultipleLearningDifficultyFrom, viewModel.ProfoundMultipleLearningDifficultyTo)
        : null;

    public CharacteristicRange? PercentWithPD => IsSelected(viewModel.PhysicalDisability)
        ? new CharacteristicRange(viewModel.PhysicalDisabilityFrom, viewModel.PhysicalDisabilityTo)
        : null;

    public CharacteristicRange? PercentWithOTH => IsSelected(viewModel.OtherLearningDifficulty)
        ? new CharacteristicRange(viewModel.OtherLearningDifficultyFrom, viewModel.OtherLearningDifficultyTo)
        : null;

    public CharacteristicRange? PercentWithMSI => IsSelected(viewModel.MultiSensoryImpairment)
        ? new CharacteristicRange(viewModel.MultiSensoryImpairmentFrom, viewModel.MultiSensoryImpairmentTo)
        : null;

    public CharacteristicRange? PercentWithMLD => IsSelected(viewModel.ModerateLearningDifficulty)
        ? new CharacteristicRange(viewModel.ModerateLearningDifficultyFrom, viewModel.ModerateLearningDifficultyTo)
        : null;

    public CharacteristicRange? PercentWithHI => IsSelected(viewModel.HearingImpairment)
        ? new CharacteristicRange(viewModel.HearingImpairmentFrom, viewModel.HearingImpairmentTo)
        : null;

    public CharacteristicRange? PercentWithASD => IsSelected(viewModel.AutisticSpectrumDisorder)
        ? new CharacteristicRange(viewModel.AutisticSpectrumDisorderFrom, viewModel.AutisticSpectrumDisorderTo)
        : null;

    private static bool IsSelected(string? value) => bool.TrueString.Equals(value, StringComparison.OrdinalIgnoreCase);
}

public record CharacteristicList(params string[] Values);

public record CharacteristicValueBool(bool Values);

public record CharacteristicRange
{
    public CharacteristicRange(decimal? from, decimal? to)
    {
        From = from.GetValueOrDefault();
        To = to.GetValueOrDefault();
    }

    public CharacteristicRange(int? from, int? to)
    {
        From = Convert.ToDecimal(from);
        To = Convert.ToDecimal(to);
    }

    public decimal From { get; set; }
    public decimal To { get; set; }
}