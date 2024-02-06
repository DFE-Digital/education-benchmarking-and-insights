using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanViewModel(School school)
{
    private readonly Finances? _finances;
    public SchoolPlanViewModel(School school, int? year) : this(school)
    {
        SelectedYear = year;
    }

    public SchoolPlanViewModel(School school, Finances finances, int year) : this(school)
    {
        SelectedYear = year;
        _finances = finances;
    }

    public int? SelectedYear { get; set; }
    public string Name => school.Name;
    public string Urn => school.Urn;
    public string CurrentTotalIncome => $"{_finances.TotalIncome:C}";
    public string CurrentTotalExpenditure => $"{_finances.TotalExpenditure:C}";
    public string CurrentTotalTeacherCosts => $"{_finances.TeachingStaffCosts:C}";
    public string CurrentTotalNumberOfTeachersFte => $"{_finances.TotalNumberOfTeachersFte}";
    public string CurrentEducationSupportStaffCosts => $"{_finances.EducationSupportStaffCosts:C}";
    public bool IsPrimary => _finances.OverallPhase == "Primary";
}