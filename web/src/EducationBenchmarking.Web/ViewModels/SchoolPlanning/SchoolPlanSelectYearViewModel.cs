using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels.SchoolPlanning;

public class SchoolPlanSelectYearViewModel(School school, int? year = null) : SchoolPlanSchoolViewModel(school)
{
    public int? SelectedYear => year;
}