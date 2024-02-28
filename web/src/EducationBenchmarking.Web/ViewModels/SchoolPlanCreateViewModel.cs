using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanCreateViewModel
{
    public SchoolPlanCreateViewModel() { }

    public SchoolPlanCreateViewModel(School school)
    {
        Name = school.Name;
        Urn = school.Urn;
        IsPrimary = school.IsPrimary;
        HasSixthForm = school.HasSixthForm;
    }

    public SchoolPlanCreateViewModel(School school, int? year) : this(school)
    {
        Year = year;
    }

    public SchoolPlanCreateViewModel(School school, FinancialPlan plan) : this(school)
    {
        Year = plan.Year;
        UseFigures = plan.UseFigures;
        TimetablePeriods = plan.TimetablePeriods.ToString();
        TotalIncome = plan.TotalIncome;
        TotalExpenditure = plan.TotalExpenditure;
        TotalTeacherCosts = plan.TotalTeacherCosts;
        TotalNumberOfTeachersFte = plan.TotalNumberOfTeachersFte;
        EducationSupportStaffCosts = plan.EducationSupportStaffCosts;
        HasMixedAgeClasses = plan.HasMixedAgeClasses;
        MixedAgeReceptionYear1 = plan.MixedAgeReceptionYear1;
        MixedAgeYear1Year2 = plan.MixedAgeYear1Year2;
        MixedAgeYear2Year3 = plan.MixedAgeYear2Year3;
        MixedAgeYear3Year4 = plan.MixedAgeYear3Year4;
        MixedAgeYear4Year5 = plan.MixedAgeYear4Year5;
        MixedAgeYear5Year6 = plan.MixedAgeYear5Year6;
        PupilsNursery = plan.PupilsNursery;
        PupilsMixedReceptionYear1 = plan.PupilsMixedReceptionYear1.ToString();
        PupilsMixedYear1Year2 = plan.PupilsMixedYear1Year2.ToString();
        PupilsMixedYear2Year3 = plan.PupilsMixedYear2Year3.ToString();
        PupilsMixedYear3Year4 = plan.PupilsMixedYear3Year4.ToString();
        PupilsMixedYear4Year5 = plan.PupilsMixedYear4Year5.ToString();
        PupilsMixedYear5Year6 = plan.PupilsMixedYear5Year6.ToString();
        PupilsReception = plan.PupilsReception.ToString();
        PupilsYear1 = plan.PupilsYear1.ToString();
        PupilsYear2 = plan.PupilsYear2.ToString();
        PupilsYear3 = plan.PupilsYear3.ToString();
        PupilsYear4 = plan.PupilsYear4.ToString();
        PupilsYear5 = plan.PupilsYear5.ToString();
        PupilsYear6 = plan.PupilsYear6.ToString();
        PupilsYear7 = plan.PupilsYear7.ToString();
        PupilsYear8 = plan.PupilsYear8.ToString();
        PupilsYear9 = plan.PupilsYear9.ToString();
        PupilsYear10 = plan.PupilsYear10.ToString();
        PupilsYear11 = plan.PupilsYear11.ToString();
        PupilsYear12 = plan.PupilsYear12;
        PupilsYear13 = plan.PupilsYear13;
        TeachersNursery = plan.TeachersNursery.ToString();
        TeachersMixedReceptionYear1 = plan.TeachersMixedReceptionYear1.ToString();
        TeachersMixedYear1Year2 = plan.TeachersMixedYear1Year2.ToString();
        TeachersMixedYear2Year3 = plan.TeachersMixedYear2Year3.ToString();
        TeachersMixedYear3Year4 = plan.TeachersMixedYear3Year4.ToString();
        TeachersMixedYear4Year5 = plan.TeachersMixedYear4Year5.ToString();
        TeachersMixedYear5Year6 = plan.TeachersMixedYear5Year6.ToString();
        TeachersReception = plan.TeachersReception.ToString();
        TeachersYear1 = plan.TeachersYear1.ToString();
        TeachersYear2 = plan.TeachersYear2.ToString();
        TeachersYear3 = plan.TeachersYear3.ToString();
        TeachersYear4 = plan.TeachersYear4.ToString();
        TeachersYear5 = plan.TeachersYear5.ToString();
        TeachersYear6 = plan.TeachersYear6.ToString();
        TeachersYear7 = plan.TeachersYear7.ToString();
        TeachersYear8 = plan.TeachersYear8.ToString();
        TeachersYear9 = plan.TeachersYear9.ToString();
        TeachersYear10 = plan.TeachersYear10.ToString();
        TeachersYear11 = plan.TeachersYear11.ToString();
        TeachersYear12 = plan.TeachersYear12.ToString();
        TeachersYear13 = plan.TeachersYear13.ToString();
    }

    public string? Name { get; set; }
    public string? Urn { get; set; }
    public bool IsPrimary { get; set; }
    public bool HasSixthForm { get; set; }
    public int? Year { get; set; }
    public bool? UseFigures { get; set; }
    public string? TimetablePeriods { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeacherCosts { get; set; }
    public decimal? TotalNumberOfTeachersFte { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public bool? HasMixedAgeClasses { get; set; }
    public bool MixedAgeReceptionYear1 { get; set; }
    public bool MixedAgeYear1Year2 { get; set; }
    public bool MixedAgeYear2Year3 { get; set; }
    public bool MixedAgeYear3Year4 { get; set; }
    public bool MixedAgeYear4Year5 { get; set; }
    public bool MixedAgeYear5Year6 { get; set; }
    public decimal? PupilsNursery { get; set; }
    public string? PupilsMixedReceptionYear1 { get; set; }
    public string? PupilsMixedYear1Year2 { get; set; }
    public string? PupilsMixedYear2Year3 { get; set; }
    public string? PupilsMixedYear3Year4 { get; set; }
    public string? PupilsMixedYear4Year5 { get; set; }
    public string? PupilsMixedYear5Year6 { get; set; }
    public string? PupilsReception { get; set; }
    public string? PupilsYear1 { get; set; }
    public string? PupilsYear2 { get; set; }
    public string? PupilsYear3 { get; set; }
    public string? PupilsYear4 { get; set; }
    public string? PupilsYear5 { get; set; }
    public string? PupilsYear6 { get; set; }
    public string? PupilsYear7 { get; set; }
    public string? PupilsYear8 { get; set; }
    public string? PupilsYear9 { get; set; }
    public string? PupilsYear10 { get; set; }
    public string? PupilsYear11 { get; set; }
    public decimal? PupilsYear12 { get; set; }
    public decimal? PupilsYear13 { get; set; }
    public string? TeachersNursery { get; set; }
    public string? TeachersMixedReceptionYear1 { get; set; }
    public string? TeachersMixedYear1Year2 { get; set; }
    public string? TeachersMixedYear2Year3 { get; set; }
    public string? TeachersMixedYear3Year4 { get; set; }
    public string? TeachersMixedYear4Year5 { get; set; }
    public string? TeachersMixedYear5Year6 { get; set; }
    public string? TeachersReception { get; set; }
    public string? TeachersYear1 { get; set; }
    public string? TeachersYear2 { get; set; }
    public string? TeachersYear3 { get; set; }
    public string? TeachersYear4 { get; set; }
    public string? TeachersYear5 { get; set; }
    public string? TeachersYear6 { get; set; }
    public string? TeachersYear7 { get; set; }
    public string? TeachersYear8 { get; set; }
    public string? TeachersYear9 { get; set; }
    public string? TeachersYear10 { get; set; }
    public string? TeachersYear11 { get; set; }
    public string? TeachersYear12 { get; set; }
    public string? TeachersYear13 { get; set; }
}