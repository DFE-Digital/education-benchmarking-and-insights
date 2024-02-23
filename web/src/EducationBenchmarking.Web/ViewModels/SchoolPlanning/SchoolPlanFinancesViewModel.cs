using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels.SchoolPlanning;

public class SchoolPlanFinancesViewModel(School school, Finances finances, FinancialPlan plan)
    : SchoolPlanViewModel(school, plan)
{
    public string CurrentTotalIncome => $"{finances.TotalIncome:C}";
    public string CurrentTotalExpenditure => $"{finances.TotalExpenditure:C}";
    public string CurrentTotalTeacherCosts => $"{finances.TeachingStaffCosts:C}";
    public string CurrentTotalNumberOfTeachersFte => $"{finances.TotalNumberOfTeachersFte}";
    public string CurrentEducationSupportStaffCosts => $"{finances.EducationSupportStaffCosts:C}";
    public string FinancePeriod => $"{finances.YearEnd - 1} - {finances.YearEnd}";
}