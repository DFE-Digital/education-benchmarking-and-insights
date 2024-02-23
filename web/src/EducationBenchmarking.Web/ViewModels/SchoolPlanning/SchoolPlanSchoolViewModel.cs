using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels.SchoolPlanning;

public class SchoolPlanSchoolViewModel(School school)
{
    public string? Name => school.Name;
    public string? Urn => school.Urn;
    public bool IsPrimary => school.IsPrimary;
    public bool HasSixthForm => school.HasSixthForm;
}