using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;


public class SchoolPlanSchoolViewModel(School school)
{
    public string? Name => school.Name;
    public string? Urn => school.Urn;
    public bool IsPrimary => school.IsPrimary;
}

public class SchoolPlanSelectYearViewModel(School school, int? year = null) : SchoolPlanSchoolViewModel(school)
{
    public int? SelectedYear => year;
}


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
}

public class SchoolPlanTimetableViewModel(School school, FinancialPlan plan, string? timetablePeriods = null)
    : SchoolPlanViewModel(school, plan)
{
    public new string? TimetablePeriods => timetablePeriods ?? base.TimetablePeriods;
}

public class SchoolPlanMixedAgeClassesViewModel
{
    public bool MixedAgeReceptionYear1 { get; set; }
    public bool MixedAgeYear1Year2 { get; set; }
    public bool MixedAgeYear2Year3 { get; set; }
    public bool MixedAgeYear3Year4 { get; set; }
    public bool MixedAgeYear4Year5 { get; set; }
    public bool MixedAgeYear5Year6 { get; set; }

    public bool HasSelection => MixedAgeReceptionYear1 || MixedAgeYear1Year2 || MixedAgeYear2Year3 ||
                                MixedAgeYear3Year4 || MixedAgeYear4Year5 || MixedAgeYear5Year6;
}


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