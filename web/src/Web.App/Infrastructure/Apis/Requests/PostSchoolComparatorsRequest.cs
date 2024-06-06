using Web.App.Domain;
using Web.App.ViewModels;
// ReSharper disable InconsistentNaming
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
            _ =>
            [
                EstablishmentTypes.Maintained
            ]
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

    // todo: support multiple LAs (see #212642)
    public CharacteristicList LAName => new(viewModel.LaSelection switch
    {
        "Choose" => viewModel.LaInput!,
        "This" => laName!,
        _ => "All"
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

    // currently unmapped
    public CharacteristicRange? PercentWithVI => null;
    public CharacteristicRange? PercentWithSPLD => null;
    public CharacteristicRange? PercentWithSLD => null;
    public CharacteristicRange? PercentWithSLCN => null;
    public CharacteristicRange? PercentWithSEMH => null;
    public CharacteristicRange? PercentWithPMLD => null;
    public CharacteristicRange? PercentWithPD => null;
    public CharacteristicRange? PercentWithOTH => null;
    public CharacteristicRange? PercentWithMSI => null;
    public CharacteristicRange? PercentWithMLD => null;
    public CharacteristicRange? PercentWithHI => null;
    public CharacteristicRange? PercentWithASD => null;

    // missing
    // Deficit

    private static bool IsSelected(string? value) => bool.TrueString.Equals(value, StringComparison.OrdinalIgnoreCase);
}

public record CharacteristicList(params string[] Values)
{
}

public record CharacteristicValueBool(bool Values)
{
}

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