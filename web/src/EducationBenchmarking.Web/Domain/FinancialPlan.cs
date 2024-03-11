namespace EducationBenchmarking.Web.Domain;

public class FinancialPlan
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }
    public bool? UseFigures { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal TargetContactRatio { get; set; } = 0.78M;
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeacherCosts { get; set; }
    public decimal? TotalNumberOfTeachersFte { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public string? TimetablePeriods { get; set; }
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
    public decimal? AssistantsNursery { get; set; }
    public decimal? AssistantsMixedReceptionYear1 { get; set; }
    public decimal? AssistantsMixedYear1Year2 { get; set; }
    public decimal? AssistantsMixedYear2Year3 { get; set; }
    public decimal? AssistantsMixedYear3Year4 { get; set; }
    public decimal? AssistantsMixedYear4Year5 { get; set; }
    public decimal? AssistantsMixedYear5Year6 { get; set; }
    public decimal? AssistantsReception { get; set; }
    public decimal? AssistantsYear1 { get; set; }
    public decimal? AssistantsYear2 { get; set; }
    public decimal? AssistantsYear3 { get; set; }
    public decimal? AssistantsYear4 { get; set; }
    public decimal? AssistantsYear5 { get; set; }
    public decimal? AssistantsYear6 { get; set; }
    public List<OtherTeachingPeriod> OtherTeachingPeriods { get; set; } = new();
    public bool ManagementRoleHeadteacher { get; set; }
    public bool ManagementRoleDeputyHeadteacher { get; set; }
    public bool ManagementRoleNumeracyLead { get; set; }
    public bool ManagementRoleLiteracyLead { get; set; }
    public bool ManagementRoleHeadSmallCurriculum { get; set; }
    public bool ManagementRoleHeadKs1 { get; set; }
    public bool ManagementRoleHeadKs2 { get; set; }
    public bool ManagementRoleSenco { get; set; }
    public bool ManagementRoleAssistantHeadteacher { get; set; }
    public bool ManagementRoleHeadLargeCurriculum { get; set; }
    public bool ManagementRolePastoralLeader { get; set; }
    public bool ManagementRoleOtherMembers { get; set; }
    public string? NumberHeadteacher { get; set; }
    public string? NumberDeputyHeadteacher { get; set; }
    public string? NumberNumeracyLead { get; set; }
    public string? NumberLiteracyLead { get; set; }
    public string? NumberHeadSmallCurriculum { get; set; }
    public string? NumberHeadKs1 { get; set; }
    public string? NumberHeadKs2 { get; set; }
    public string? NumberSenco { get; set; }
    public string? NumberAssistantHeadteacher { get; set; }
    public string? NumberHeadLargeCurriculum { get; set; }
    public string? NumberPastoralLeader { get; set; }
    public string? NumberOtherMembers { get; set; }
    public string[] TeachingPeriodsHeadteacher { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsDeputyHeadteacher { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsNumeracyLead { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsLiteracyLead { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsHeadSmallCurriculum { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsHeadKs1 { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsHeadKs2 { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsSenco { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsAssistantHeadteacher { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsHeadLargeCurriculum { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsPastoralLeader { get; set; } = Array.Empty<string>();
    public string[] TeachingPeriodsOtherMembers { get; set; } = Array.Empty<string>();

    public class OtherTeachingPeriod
    {
        public string? PeriodName { get; set; }
        public string? PeriodsPerTimetable { get; set; }
    }
}