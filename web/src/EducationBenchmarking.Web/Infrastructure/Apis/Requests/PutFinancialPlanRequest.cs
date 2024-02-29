using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class PutFinancialPlanRequest
{
    public int? Year { get; set; }
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
    public int? TeachersNursery { get; set; }
    public int? TeachersMixedReceptionYear1 { get; set; }
    public int? TeachersMixedYear1Year2 { get; set; }
    public int? TeachersMixedYear2Year3 { get; set; }
    public int? TeachersMixedYear3Year4 { get; set; }
    public int? TeachersMixedYear4Year5 { get; set; }
    public int? TeachersMixedYear5Year6 { get; set; }
    public int? TeachersReception { get; set; }
    public int? TeachersYear1 { get; set; }
    public int? TeachersYear2 { get; set; }
    public int? TeachersYear3 { get; set; }
    public int? TeachersYear4 { get; set; }
    public int? TeachersYear5 { get; set; }
    public int? TeachersYear6 { get; set; }
    public int? TeachersYear7 { get; set; }
    public int? TeachersYear8 { get; set; }
    public int? TeachersYear9 { get; set; }
    public int? TeachersYear10 { get; set; }
    public int? TeachersYear11 { get; set; }
    public int? TeachersYear12 { get; set; }
    public int? TeachersYear13 { get; set; }
    public decimal? AssistantsMixedReceptionYear1 { get; set; }
    public decimal? AssistantsMixedYear1Year2 { get; set; }
    public decimal? AssistantsMixedYear2Year3 { get; set; }
    public decimal? AssistantsMixedYear3Year4 { get; set; }
    public decimal? AssistantsMixedYear4Year5 { get; set; }
    public decimal? AssistantsMixedYear5Year6 { get; set; }
    public decimal? AssistantsNursery { get; set; }
    public decimal? AssistantsReception { get; set; }
    public decimal? AssistantsYear1 { get; set; }
    public decimal? AssistantsYear2 { get; set; }
    public decimal? AssistantsYear3 { get; set; }
    public decimal? AssistantsYear4 { get; set; }
    public decimal? AssistantsYear5 { get; set; }
    public decimal? AssistantsYear6 { get; set; }
    public IEnumerable<OtherTeachingPeriod> OtherTeachingPeriods { get; set; } = Array.Empty<OtherTeachingPeriod>();
    
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
            PupilsMixedYear5Year6 = plan.PupilsMixedYear5Year6,
            PupilsYear7 = plan.PupilsYear7,
            PupilsYear8 = plan.PupilsYear8,
            PupilsYear9 = plan.PupilsYear9,
            PupilsYear10 = plan.PupilsYear10,
            PupilsYear11 = plan.PupilsYear11,
            PupilsYear12 = plan.PupilsYear12,
            PupilsYear13 = plan.PupilsYear13,
            TeachersNursery = plan.TeachersNursery,
            TeachersMixedReceptionYear1 = plan.TeachersMixedReceptionYear1,
            TeachersMixedYear1Year2 = plan.TeachersMixedYear1Year2,
            TeachersMixedYear2Year3 = plan.TeachersMixedYear2Year3,
            TeachersMixedYear3Year4 = plan.TeachersMixedYear3Year4,
            TeachersMixedYear4Year5 = plan.TeachersMixedYear4Year5,
            TeachersMixedYear5Year6 = plan.TeachersMixedYear5Year6,
            TeachersReception = plan.TeachersReception,
            TeachersYear1 = plan.TeachersYear1,
            TeachersYear2 = plan.TeachersYear2,
            TeachersYear3 = plan.TeachersYear3,
            TeachersYear4 = plan.TeachersYear4,
            TeachersYear5 = plan.TeachersYear5,
            TeachersYear6 = plan.TeachersYear6,
            TeachersYear7 = plan.TeachersYear7,
            TeachersYear8 = plan.TeachersYear8,
            TeachersYear9 = plan.TeachersYear9,
            TeachersYear10 = plan.TeachersYear10,
            TeachersYear11 = plan.TeachersYear11,
            TeachersYear12 = plan.TeachersYear12,
            TeachersYear13 = plan.TeachersYear13,
            AssistantsMixedReceptionYear1 = plan.AssistantsMixedReceptionYear1,
            AssistantsMixedYear1Year2 = plan.AssistantsMixedYear1Year2,
            AssistantsMixedYear2Year3 = plan.AssistantsMixedYear2Year3,
            AssistantsMixedYear3Year4 = plan.AssistantsMixedYear3Year4,
            AssistantsMixedYear4Year5 = plan.AssistantsMixedYear4Year5,
            AssistantsMixedYear5Year6 = plan.AssistantsMixedYear5Year6,
            AssistantsNursery = plan.AssistantsNursery,
            AssistantsReception = plan.AssistantsReception,
            AssistantsYear1 = plan.AssistantsYear1,
            AssistantsYear2 = plan.AssistantsYear2,
            AssistantsYear3 = plan.AssistantsYear3,
            AssistantsYear4 = plan.AssistantsYear4,
            AssistantsYear5 = plan.AssistantsYear5,
            AssistantsYear6 = plan.AssistantsYear6,
            OtherTeachingPeriods = plan.OtherTeachingPeriods
                .Select(x => new OtherTeachingPeriod
                {
                    PeriodName = x.PeriodName,
                    PeriodsPerTimetable = x.PeriodsPerTimetable
                })
        };
    }

    public class OtherTeachingPeriod
    {
        public string? PeriodName { get; set; }
        public string? PeriodsPerTimetable { get; set; }
    }
}