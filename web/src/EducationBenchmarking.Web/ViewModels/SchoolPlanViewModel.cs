using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanViewModel(School school)
{
    private readonly Finances? _finances;
    private readonly FinancialPlan? _plan;
    public SchoolPlanViewModel(School school, int? year) : this(school)
    {
        SelectedYear = year;
    }

    public SchoolPlanViewModel(School school, Finances finances, FinancialPlan? plan, int year) : this(school)
    {
        SelectedYear = year;
        _plan = plan;
        _finances = finances;
    }

    public int? SelectedYear { get; set; }
    public string Name => school.Name;
    public string Urn => school.Urn;
    public decimal CurrentTotalIncome => _finances?.TotalIncome ?? throw new ArgumentNullException(nameof(_finances));
    public decimal CurrentTotalExpenditure => _finances?.TotalExpenditure ?? throw new ArgumentNullException(nameof(_finances));
    public decimal CurrentTotalTeacherCosts => _finances?.TeachingStaffCosts ?? throw new ArgumentNullException(nameof(_finances));
    public decimal CurrentTotalNumberOfTeachersFte => _finances?.TotalNumberOfTeachersFte ?? throw new ArgumentNullException(nameof(_finances));
    public decimal CurrentEducationSupportStaffCosts => _finances?.EducationSupportStaffCosts ?? throw new ArgumentNullException(nameof(_finances));
    public int CurrentYearEnd => _finances?.YearEnd ?? throw new ArgumentNullException(nameof(_finances));
    public bool IsPrimary => _finances?.OverallPhase == "Primary";
    public bool? UseFigures => _plan?.UseFigures;
}