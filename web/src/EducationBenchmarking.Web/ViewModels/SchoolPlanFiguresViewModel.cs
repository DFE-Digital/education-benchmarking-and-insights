using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanFiguresViewModel(School school, Finances finances)
{

    public SchoolPlanFiguresViewModel(School school, Finances finances, int year) : this(school, finances)
    {
        SelectedYear = year;
    }
    public int? SelectedYear { get; set; }
    public string Name => school.Name;
    public string Urn => school.Urn;
    public string TotalIncome => string.Format("{0:C}", finances.TotalIncome);
    public string TotalExpenditure => string.Format("{0:C}", finances.TotalExpenditure);
    public string TotalTeacherCosts => string.Format("{0:C}", finances.TeachingStaffCosts);
    public decimal TotalNumberOfTeachersFte => finances.TotalNumberOfTeachersFte;
    public string EducationSupportStaffCosts => string.Format("{0:C}", finances.EducationSupportStaffCosts);
    public bool IsPrimary => finances.OverallPhase == "Primary";
}
