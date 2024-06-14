using Web.App.Domain;
using Web.App.Domain.Insight;

namespace Web.App.ViewModels;

public class SchoolComparatorsViewModel(
    School school,
    string? by = null,
    SchoolCharacteristicPupil[]? pupil = null,
    SchoolCharacteristicBuilding[]? building = null,
    SchoolCharacteristicUserDefined[]? userDefined = null,
    string? userDefinedSetId = null,
    bool hasCustomData = false)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public string? By => by;
    public string? UserDefinedSetId => userDefinedSetId;
    public bool HasCustomData => hasCustomData;
    public IEnumerable<SchoolCharacteristicPupil> PupilSchools => pupil ?? [];
    public IEnumerable<SchoolCharacteristicBuilding> BuildingSchools => building ?? [];
    public IEnumerable<SchoolCharacteristicUserDefined> UserDefinedSchools => userDefined ?? [];
}