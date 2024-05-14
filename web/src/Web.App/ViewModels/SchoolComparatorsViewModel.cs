using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparatorsViewModel(School school, IEnumerable<Finances>? finances = null)
{
    public IEnumerable<Finances> Schools => finances ?? [];
    public string? Urn => school.Urn;
    public string? Name => school.Name;
}