using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparatorsChosenSchoolsViewModel(string? urn, SchoolCharacteristicUserDefined[]? schoolCharacteristics)
{
    public string? Urn => urn;
    public IOrderedEnumerable<SchoolCharacteristicUserDefined>? Schools => schoolCharacteristics?.OrderBy(c => c.SchoolName);
}