using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class PutFinancialPlanRequest
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    public bool? UseFigures { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeacherCosts { get; set; }
    public decimal? TotalNumberOfTeachersFte { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public int? TimetablePeriods { get; set; }
    public bool? HasMixedAgeClasses { get; set; }
    public bool MixedAgeReceptionYear1 { get; set; }
    public bool MixedAgeYear1Year2 { get; set; }
    public bool MixedAgeYear2Year3 { get; set; }
    public bool MixedAgeYear3Year4 { get; set; }
    public bool MixedAgeYear4Year5 { get; set; }
    public bool MixedAgeYear5Year6 { get; set; }
    public int? PupilsYear7 { get; set; }
    public int? PupilsYear8 { get; set; }
    public int? PupilsYear9 { get; set; }
    public int? PupilsYear10 { get; set; }
    public int? PupilsYear11 { get; set; }
    public decimal? PupilsYear12 { get; set; }
    public decimal? PupilsYear13 { get; set; }
    public decimal? PupilsNursery { get; set; }
    public int? PupilsMixedReceptionYear1 { get; set; }
    public int? PupilsMixedYear1Year2 { get; set; }
    public int? PupilsMixedYear2Year3 { get; set; }
    public int? PupilsMixedYear3Year4 { get; set; }
    public int? PupilsMixedYear4Year5 { get; set; }
    public int? PupilsMixedYear5Year6 { get; set; }
    public int? PupilsReception { get; set; }
    public int? PupilsYear1 { get; set; }
    public int? PupilsYear2 { get; set; }
    public int? PupilsYear3 { get; set; }
    public int? PupilsYear4 { get; set; }
    public int? PupilsYear5 { get; set; }
    public int? PupilsYear6 { get; set; }

    public static PutFinancialPlanRequest Create(FinancialPlan plan)
    {
        return new PutFinancialPlanRequest
        {
            Year = plan.Year,
            Urn = plan.Urn,
            UseFigures = plan.UseFigures,
            TotalIncome = plan.TotalIncome,
            TotalExpenditure = plan.TotalExpenditure,
            TotalTeacherCosts = plan.TotalTeacherCosts,
            TotalNumberOfTeachersFte = plan.TotalNumberOfTeachersFte,
            EducationSupportStaffCosts = plan.EducationSupportStaffCosts,
            TimetablePeriods = plan.TimetablePeriods,
            HasMixedAgeClasses = plan.HasMixedAgeClasses,
            MixedAgeReceptionYear1 = plan.HasMixedAgeClasses.GetValueOrDefault() && plan.MixedAgeReceptionYear1,
            MixedAgeYear1Year2 = plan.HasMixedAgeClasses.GetValueOrDefault() && plan.MixedAgeYear1Year2,
            MixedAgeYear2Year3 = plan.HasMixedAgeClasses.GetValueOrDefault() && plan.MixedAgeYear2Year3,
            MixedAgeYear3Year4 = plan.HasMixedAgeClasses.GetValueOrDefault() && plan.MixedAgeYear3Year4,
            MixedAgeYear4Year5 = plan.HasMixedAgeClasses.GetValueOrDefault() && plan.MixedAgeYear4Year5,
            MixedAgeYear5Year6 = plan.HasMixedAgeClasses.GetValueOrDefault() && plan.MixedAgeYear5Year6,
            PupilsYear7 = plan.PupilsYear7,
            PupilsYear8 = plan.PupilsYear8,
            PupilsYear9 = plan.PupilsYear9,
            PupilsYear10 = plan.PupilsYear10,
            PupilsYear11 = plan.PupilsYear11,
            PupilsYear12 = plan.PupilsYear12,
            PupilsYear13 = plan.PupilsYear13,
            PupilsNursery = plan.PupilsNursery,
            PupilsReception = plan.PupilsReception,
            PupilsYear1 = plan.PupilsYear1,
            PupilsYear2 = plan.PupilsYear2,
            PupilsYear3 = plan.PupilsYear3,
            PupilsYear4 = plan.PupilsYear4,
            PupilsYear5 = plan.PupilsYear5,
            PupilsYear6 = plan.PupilsYear6,
            PupilsMixedReceptionYear1 = plan.PupilsMixedReceptionYear1,
            PupilsMixedYear1Year2 = plan.PupilsMixedYear1Year2,
            PupilsMixedYear2Year3 = plan.PupilsMixedYear2Year3,
            PupilsMixedYear3Year4 = plan.PupilsMixedYear3Year4,
            PupilsMixedYear4Year5 = plan.PupilsMixedYear4Year5,
            PupilsMixedYear5Year6 = plan.PupilsMixedYear5Year6
        };
    }
}