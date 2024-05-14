using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparatorViewModel(School school, Finances source, Finances target)
{
    public string? Urn => school.Urn;
    public string? Name => school.Name;

    public Finances Source => source;
    public Finances Target => target;
}