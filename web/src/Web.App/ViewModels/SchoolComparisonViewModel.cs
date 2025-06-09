using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparisonViewModel(
    School school,
    string? userDefinedSetId = null,
    string? customDataId = null,
    SchoolExpenditure? expenditure = null,
    SchoolComparatorSet? defaultComparatorSet = null)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? UserDefinedSetId => userDefinedSetId;
    public string? CustomDataId => customDataId;
    public int? PeriodCoveredByReturn => expenditure?.PeriodCoveredByReturn;
    public bool HasDefaultComparatorSet => defaultComparatorSet != null
                                           && (defaultComparatorSet.Building.Any(b => !string.IsNullOrWhiteSpace(b))
                                               || defaultComparatorSet.Pupil.Any(p => !string.IsNullOrWhiteSpace(p)));
    public Dictionary<string, string> CostCodeMap => new CostCodes(IsPartOfTrust).SubCategoryToCostCodeMap;
}