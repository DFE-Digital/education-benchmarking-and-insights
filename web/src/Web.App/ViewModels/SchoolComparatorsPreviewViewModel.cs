using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparatorsPreviewViewModel(
    School school,
    SchoolCharacteristic[]? characteristics,
    long? closestSchools,
    long? totalSchools,
    UserDefinedCharacteristicViewModel? userDefinedCharacteristics)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;

    public SchoolCharacteristic[]? Characteristics => characteristics;
    public long? ClosestSchools => closestSchools;
    public long? TotalSchools => totalSchools;

    public bool AnyUniqueGroupings => AllAcademies == true || AllMaintained == true || AllNursery == true || AllPrimary == true
                                      || AllSecondary == true || AllPupilReferralUnit == true || AllSpecial == true || AllPfi == true;

    public bool? AllAcademies => characteristics?.All(c => c.FinanceType == EstablishmentTypes.Academies);
    public bool? AllMaintained => characteristics?.All(c => c.FinanceType == EstablishmentTypes.Maintained);

    public bool? AllNursery => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.Nursery);
    public bool? AllPrimary => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.Primary);
    public bool? AllSecondary => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.Secondary);
    public bool? AllPupilReferralUnit => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.PupilReferralUnit);
    public bool? AllSpecial => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.Special);

    public string? AllInLaName => AllLaNames?.Count() == 1 ? AllLaNames.Single() : null;
    public bool? AllPfi => characteristics?.All(c => c.IsPFISchool == true);

    public UserDefinedCharacteristicViewModel? UserDefinedCharacteristics => userDefinedCharacteristics;

    private IEnumerable<string>? AllLaNames => characteristics?
        .Where(c => !string.IsNullOrWhiteSpace(c.LAName))
        .GroupBy(c => c.LAName)
        .Select(g => g.Key)
        .OfType<string>();
}