using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanViewModel(School school)
{
    public SchoolPlanViewModel(School school, int? year) : this(school)
    {
        SelectedYear = year;
    }
    public int? SelectedYear { get; set; }
    public string Name => school.Name;
    public string Urn => school.Urn;
}