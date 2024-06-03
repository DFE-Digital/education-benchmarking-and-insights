using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparatorsViewModel(School school, string? by = null, SchoolCharacteristicPupil[]? pupil = null, SchoolCharacteristicBuilding[]? building = null, SchoolCharacteristicUserDefined[]? userDefined = null)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public string? By => by;

    public IEnumerable<SchoolCharacteristicPupil> PupilSchools => pupil ?? [];
    public IEnumerable<SchoolCharacteristicBuilding> BuildingSchools => building ?? [];
    public IEnumerable<SchoolCharacteristicUserDefined> UserDefinedSchools => userDefined ?? [];
}