using System.Diagnostics.CodeAnalysis;
using Web.App.Domain;

namespace Web.App.ViewModels;

[ExcludeFromCodeCoverage]
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
    public string? OverallPhase => school.OverallPhase;
    public IEnumerable<SchoolCharacteristicPupil> PupilSchools => pupil ?? [];
    public IEnumerable<SchoolCharacteristicBuilding> BuildingSchools => building ?? [];
    public IEnumerable<SchoolCharacteristicUserDefined> UserDefinedSchools => userDefined ?? [];
}