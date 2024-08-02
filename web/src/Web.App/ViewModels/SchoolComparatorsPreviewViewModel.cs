using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparatorsPreviewViewModel(
    School school,
    SchoolCharacteristic[]? characteristics,
    long? closestSchools,
    long? totalSchools,
    UserDefinedSchoolCharacteristicViewModel? userDefinedCharacteristics)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;

    public SchoolCharacteristic[]? Characteristics => characteristics;
    public long? ClosestSchools => closestSchools;
    public long? TotalSchools => totalSchools;

    public bool AnyUniqueGroupings => AllAcademies == true || AllMaintained == true || AllNursery == true ||
                                      AllPrimary == true
                                      || AllSecondary == true || AllPupilReferralUnit == true || AllSpecial == true ||
                                      AllPfi == true
                                      || AllAlternativeProvision == true || AllAllThrough == true ||
                                      AllPostSixteen == true || AllUniversityTechnicalCollege == true;

    public bool? AllAcademies => characteristics?.All(c => c.FinanceType == EstablishmentTypes.Academies);
    public bool? AllMaintained => characteristics?.All(c => c.FinanceType == EstablishmentTypes.Maintained);

    public bool? AllNursery => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.Nursery);
    public bool? AllPrimary => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.Primary);
    public bool? AllSecondary => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.Secondary);
    public bool? AllPupilReferralUnit => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.PupilReferralUnit);
    public bool? AllSpecial => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.Special);
    public bool? AllAlternativeProvision => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.AlternativeProvision);
    public bool? AllAllThrough => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.AllThrough);
    public bool? AllPostSixteen => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.PostSixteen);
    public bool? AllUniversityTechnicalCollege => characteristics?.All(c => c.OverallPhase == OverallPhaseTypes.UniversityTechnicalCollege);

    public string? AllInLaName => AllLaNames?.Count() == 1 ? AllLaNames.Single() : null;
    public bool? AllPfi => characteristics?.All(c => c.IsPFISchool == true);

    public UserDefinedSchoolCharacteristicViewModel? UserDefinedCharacteristics => userDefinedCharacteristics;
    public bool TotalPupilsSelected => IsSelected(userDefinedCharacteristics?.TotalPupils);
    public bool FreeSchoolMealsSelected => IsSelected(userDefinedCharacteristics?.FreeSchoolMeals);
    public bool SpecialEducationalNeedsSelected => IsSelected(userDefinedCharacteristics?.SpecialEducationalNeeds);
    public bool LondonWeightingSelected => IsSelected(userDefinedCharacteristics?.LondonWeighting);
    public bool AverageBuildingAgeSelected => IsSelected(userDefinedCharacteristics?.AverageBuildingAge);
    public bool InternalFloorAreaSelected => IsSelected(userDefinedCharacteristics?.InternalFloorArea);
    public bool OfstedRatingSelected => IsSelected(userDefinedCharacteristics?.OfstedRating);
    public bool SchoolsInTrustSelected => IsSelected(userDefinedCharacteristics?.SchoolsInTrust);
    public bool DeficitSelected => IsSelected(userDefinedCharacteristics?.Deficit);
    public bool PrivateFinanceInitiativeSelected => IsSelected(userDefinedCharacteristics?.PrivateFinanceInitiative);
    public bool TotalPupilsSixthFormSelected => IsSelected(userDefinedCharacteristics?.TotalPupilsSixthForm);
    public bool KeyStage2ProgressSelected => IsSelected(userDefinedCharacteristics?.KeyStage2Progress);
    public bool KeyStage4ProgressSelected => IsSelected(userDefinedCharacteristics?.KeyStage4Progress);
    public bool SpecificLearningDifficultySelected => IsSelected(userDefinedCharacteristics?.SpecificLearningDifficulty);
    public bool ModerateLearningDifficultySelected => IsSelected(userDefinedCharacteristics?.ModerateLearningDifficulty);
    public bool SevereLearningDifficultySelected => IsSelected(userDefinedCharacteristics?.SevereLearningDifficulty);
    public bool ProfoundMultipleLearningDifficultySelected => IsSelected(userDefinedCharacteristics?.ProfoundMultipleLearningDifficulty);
    public bool SocialEmotionalMentalHealthSelected => IsSelected(userDefinedCharacteristics?.SocialEmotionalMentalHealth);
    public bool SpeechLanguageCommunicationSelected => IsSelected(userDefinedCharacteristics?.SpeechLanguageCommunication);
    public bool HearingImpairmentSelected => IsSelected(userDefinedCharacteristics?.HearingImpairment);
    public bool VisualImpairmentSelected => IsSelected(userDefinedCharacteristics?.VisualImpairment);
    public bool MultiSensoryImpairmentSelected => IsSelected(userDefinedCharacteristics?.MultiSensoryImpairment);
    public bool PhysicalDisabilitySelected => IsSelected(userDefinedCharacteristics?.PhysicalDisability);
    public bool AutisticSpectrumDisorderSelected => IsSelected(userDefinedCharacteristics?.AutisticSpectrumDisorder);
    public bool OtherLearningDifficultySelected => IsSelected(userDefinedCharacteristics?.OtherLearningDifficulty);
    public bool? HasAdditionalCharacteristics => TotalPupilsSelected
                                                 || FreeSchoolMealsSelected
                                                 || SpecialEducationalNeedsSelected
                                                 || LondonWeightingSelected
                                                 || AverageBuildingAgeSelected
                                                 || InternalFloorAreaSelected
                                                 || OfstedRatingSelected
                                                 || SchoolsInTrustSelected
                                                 || DeficitSelected
                                                 || PrivateFinanceInitiativeSelected
                                                 || TotalPupilsSixthFormSelected
                                                 || KeyStage2ProgressSelected
                                                 || KeyStage4ProgressSelected
                                                 || SpecificLearningDifficultySelected
                                                 || ModerateLearningDifficultySelected
                                                 || SevereLearningDifficultySelected
                                                 || ProfoundMultipleLearningDifficultySelected
                                                 || SocialEmotionalMentalHealthSelected
                                                 || SpeechLanguageCommunicationSelected
                                                 || HearingImpairmentSelected
                                                 || VisualImpairmentSelected
                                                 || MultiSensoryImpairmentSelected
                                                 || PhysicalDisabilitySelected
                                                 || AutisticSpectrumDisorderSelected
                                                 || OtherLearningDifficultySelected;

    private IEnumerable<string>? AllLaNames => characteristics?
        .Where(c => !string.IsNullOrWhiteSpace(c.LAName))
        .GroupBy(c => c.LAName)
        .Select(g => g.Key)
        .OfType<string>();

    private static bool IsSelected(string? value) => bool.TrueString.Equals(value, StringComparison.OrdinalIgnoreCase);
}