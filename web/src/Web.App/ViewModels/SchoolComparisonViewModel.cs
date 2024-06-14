using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparisonViewModel(School school, string? userDefinedSetId = null, string? customDataId = null)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? UserDefinedSetId => userDefinedSetId;
    public string? CustomDataId => customDataId;
}