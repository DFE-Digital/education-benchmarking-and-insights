using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolComparatorSetViewModel(School school, ComparatorSet<School> comparatorSet, string referrer)
{
    public IEnumerable<School> Schools => comparatorSet.Results;
    public string Urn => school.Urn;
    public string Name => school.Name;
    public string Referrer => referrer;
}