using Web.App.Domain;
using Web.App.Domain.Insight;

namespace Web.App.ViewModels;

public class SchoolComparatorsChosenSchoolsViewModel(string? urn, SchoolCharacteristicUserDefined[]? schoolCharacteristics)
{
    public string? Urn => urn;
    public IOrderedEnumerable<SchoolCharacteristicUserDefined>? Schools => schoolCharacteristics?.OrderBy(c => c.SchoolName);
}