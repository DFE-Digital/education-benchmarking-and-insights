using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain.Requests;

[ExcludeFromCodeCoverage]
public record FinancialPlanRequest
{
    public string? User { get; set; }
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
    public int? PupilsYear7 { get; set; }
    public int? PupilsYear8 { get; set; }
    public int? PupilsYear9 { get; set; }
    public int? PupilsYear10 { get; set; }
    public int? PupilsYear11 { get; set; }
    public decimal? PupilsYear12 { get; set; }
    public decimal? PupilsYear13 { get; set; }
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
    public OtherTeachingPeriod[]? OtherTeachingPeriods { get; set; }
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
    public int? NumberHeadteacher { get; set; }
    public int? NumberDeputyHeadteacher { get; set; }
    public int? NumberNumeracyLead { get; set; }
    public int? NumberLiteracyLead { get; set; }
    public int? NumberHeadSmallCurriculum { get; set; }
    public int? NumberHeadKs1 { get; set; }
    public int? NumberHeadKs2 { get; set; }
    public int? NumberSenco { get; set; }
    public int? NumberAssistantHeadteacher { get; set; }
    public int? NumberHeadLargeCurriculum { get; set; }
    public int? NumberPastoralLeader { get; set; }
    public int? NumberOtherMembers { get; set; }
    public int?[] TeachingPeriodsHeadteacher { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsDeputyHeadteacher { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsNumeracyLead { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsLiteracyLead { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsHeadSmallCurriculum { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsHeadKs1 { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsHeadKs2 { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsSenco { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsAssistantHeadteacher { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsHeadLargeCurriculum { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsPastoralLeader { get; set; } = Array.Empty<int?>();
    public int?[] TeachingPeriodsOtherMembers { get; set; } = Array.Empty<int?>();

    public class OtherTeachingPeriod
    {
        public string? PeriodName { get; set; }
        public string? PeriodsPerTimetable { get; set; }
    }
}