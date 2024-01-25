using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanningViewModel(School school)
{
    public string Name => school.Name;
    public string Urn => school.Urn;

}