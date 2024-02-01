using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanViewModel(School school)
{
    public SchoolPlanViewModel(School school, int year) : this(school)
    {
        SelectedYear = year;
    }
    public int? SelectedYear { get; set; }
    public string Name => school.Name;
    public string Urn => school.Urn;
    public int CurrentYear => DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    public IEnumerable<int> AvailableYears => Enumerable.Range(CurrentYear, 4).ToArray();
}