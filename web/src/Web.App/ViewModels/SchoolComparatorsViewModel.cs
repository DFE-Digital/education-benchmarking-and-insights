using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparatorsViewModel(School school, string referrer, IEnumerable<Finances>? finances = null)
{
    public IEnumerable<Finances> Schools => finances ?? [];
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public string Referrer => referrer;
}