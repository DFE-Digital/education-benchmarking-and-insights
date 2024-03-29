using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolComparatorSetViewModel(School school, ComparatorSet comparatorSet)
{
    public IEnumerable<string> Schools => comparatorSet.DefaultPupil;
    public string? Urn => school.Urn;
    public string? Name => school.Name;
}