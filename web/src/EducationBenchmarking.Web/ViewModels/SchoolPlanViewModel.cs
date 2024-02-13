using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanViewModel(School school)
{
    
    private readonly FinancialPlan? _plan;
    public SchoolPlanViewModel(School school, int? year, FinancialPlan? plan = null) : this(school)
    {
        SelectedYear = year;
        _plan = plan;
    }
    
    public int? SelectedYear { get; }
    public string? Name => school.Name;
    public string? Urn => school.Urn;
    public bool? UseFigures => _plan?.UseFigures;
    public bool IsPrimary => school.IsPrimary;
    public decimal? TotalIncome => _plan?.TotalIncome;
    public decimal? TotalExpenditure => _plan?.TotalExpenditure;
    public decimal? TotalTeacherCosts => _plan?.TotalTeacherCosts;
    public decimal? TotalNumberOfTeachersFte => _plan?.TotalNumberOfTeachersFte;
}

public class SchoolPlanFinancesViewModel(School school, Finances finances, int year, FinancialPlan? plan)
    : SchoolPlanViewModel(school, year, plan)
{
    public string CurrentTotalIncome => $"{finances.TotalIncome:C}";
    public string CurrentTotalExpenditure => $"{finances.TotalExpenditure:C}";
    public string CurrentTotalTeacherCosts => $"{finances.TeachingStaffCosts:C}";
    public string CurrentTotalNumberOfTeachersFte => $"{finances.TotalNumberOfTeachersFte}";
    public string CurrentEducationSupportStaffCosts => $"{finances.EducationSupportStaffCosts:C}";
    public string FinancePeriod => $"{finances.YearEnd - 1} - {finances.YearEnd}";
}