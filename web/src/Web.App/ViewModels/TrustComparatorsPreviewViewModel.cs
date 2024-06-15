using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustComparatorsPreviewViewModel(
    Trust trust,
    TrustCharacteristic[]? characteristics,
    long? closestTrusts,
    long? totalTrusts,
    UserDefinedTrustCharacteristicViewModel? userDefinedCharacteristics)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;

    public TrustCharacteristic[]? Characteristics => characteristics;
    public long? ClosestTrusts => closestTrusts;
    public long? TotalTrusts => totalTrusts;

    public bool TotalPupilsSelected => IsSelected(userDefinedCharacteristics?.TotalPupils);
    public bool SchoolsInTrustSelected => IsSelected(userDefinedCharacteristics?.SchoolsInTrust);
    public bool TotalIncomeSelected => IsSelected(userDefinedCharacteristics?.TotalIncome);
    public bool InternalFloorAreaSelected => IsSelected(userDefinedCharacteristics?.InternalFloorArea);
    public bool OverallPhaseSelected => IsSelected(userDefinedCharacteristics?.OverallPhase);
    public bool FreeSchoolMealsSelected => IsSelected(userDefinedCharacteristics?.FreeSchoolMeals);
    public bool SpecialEducationalNeedsSelected => IsSelected(userDefinedCharacteristics?.SpecialEducationalNeeds);
    public bool FormationYearSelected => IsSelected(userDefinedCharacteristics?.FormationYear);

    public bool? HasAdditionalCharacteristics => TotalPupilsSelected
                                                 || SchoolsInTrustSelected
                                                 || TotalIncomeSelected
                                                 || InternalFloorAreaSelected
                                                 || OverallPhaseSelected
                                                 || FreeSchoolMealsSelected
                                                 || SpecialEducationalNeedsSelected
                                                 || FormationYearSelected;
    private static bool IsSelected(string? value) => bool.TrueString.Equals(value, StringComparison.OrdinalIgnoreCase);
}