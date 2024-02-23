using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels.SchoolPlanning;

public class SchoolPlanTimetableViewModel(School school, FinancialPlan plan, string? timetablePeriods = null)
    : SchoolPlanViewModel(school, plan)
{
    public new string? TimetablePeriods => timetablePeriods ?? base.TimetablePeriods;
}