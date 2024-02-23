using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels.SchoolPlanning;





public class SchoolPlanViewModel(School school, FinancialPlan plan) : SchoolPlanSchoolViewModel(school)
{
    public int Year => plan.Year;
    public bool? UseFigures => plan.UseFigures;
    public decimal? TotalIncome => plan.TotalIncome;
    public decimal? TotalExpenditure => plan.TotalExpenditure;
    public decimal? TotalTeacherCosts => plan.TotalTeacherCosts;
    public decimal? TotalNumberOfTeachersFte => plan.TotalNumberOfTeachersFte;
    public decimal? EducationSupportStaffCosts => plan.EducationSupportStaffCosts;
    public string? TimetablePeriods => plan.TimetablePeriods.ToString();
    public bool? HasMixedAgeClasses => plan.HasMixedAgeClasses;
    public bool MixedAgeReceptionYear1 => plan.MixedAgeReceptionYear1;
    public bool MixedAgeYear1Year2 => plan.MixedAgeYear1Year2;
    public bool MixedAgeYear2Year3 => plan.MixedAgeYear2Year3;
    public bool MixedAgeYear3Year4 => plan.MixedAgeYear3Year4;
    public bool MixedAgeYear4Year5 => plan.MixedAgeYear4Year5;
    public bool MixedAgeYear5Year6 => plan.MixedAgeYear5Year6;
    public string? PupilsYear7 => plan.PupilsYear7.ToString();
    public string? PupilsYear8 => plan.PupilsYear8.ToString();
    public string? PupilsYear9 => plan.PupilsYear9.ToString();
    public string? PupilsYear10 => plan.PupilsYear10.ToString();
    public string? PupilsYear11 => plan.PupilsYear11.ToString();
    public decimal? PupilsYear12 => plan.PupilsYear12;
    public decimal? PupilsYear13 => plan.PupilsYear13;
}




