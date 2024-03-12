using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Domain.DataObjects;

namespace Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public record FinancialPlan
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }
    public int Version { get; set; }
    public bool IsComplete { get; set; }
    public decimal TargetContactRatio { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeacherCosts { get; set; }
    public decimal? TotalNumberOfTeachersFte { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public bool? UseFigures { get; set; }
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
    public static FinancialPlan Create(FinancialPlanDataObject dataObject)
    {
        return new FinancialPlan
        {
            Year = int.Parse(dataObject.Id ?? throw new ArgumentNullException(nameof(dataObject.Id))),
            Urn = dataObject.PartitionKey,
            Created = dataObject.Created,
            UpdatedAt = dataObject.UpdatedAt,
            UpdatedBy = dataObject.UpdatedBy,
            CreatedBy = dataObject.CreatedBy,
            Version = dataObject.Version,
            IsComplete = dataObject.IsComplete,
            TargetContactRatio = dataObject.TargetContactRatio,
            UseFigures = dataObject.UseFigures,
            TotalIncome = dataObject.TotalIncome,
            TotalExpenditure = dataObject.TotalExpenditure,
            TotalTeacherCosts = dataObject.TotalTeacherCosts,
            TotalNumberOfTeachersFte = dataObject.TotalNumberOfTeachersFte,
            EducationSupportStaffCosts = dataObject.EducationSupportStaffCosts,
            TimetablePeriods = dataObject.TimetablePeriods,
            HasMixedAgeClasses = dataObject.HasMixedAgeClasses,
            MixedAgeReceptionYear1 = dataObject.MixedAgeReceptionYear1,
            MixedAgeYear1Year2 = dataObject.MixedAgeYear1Year2,
            MixedAgeYear2Year3 = dataObject.MixedAgeYear2Year3,
            MixedAgeYear3Year4 = dataObject.MixedAgeYear3Year4,
            MixedAgeYear4Year5 = dataObject.MixedAgeYear4Year5,
            MixedAgeYear5Year6 = dataObject.MixedAgeYear5Year6,
            PupilsNursery = dataObject.PupilsNursery,
            PupilsMixedReceptionYear1 = dataObject.PupilsMixedReceptionYear1,
            PupilsMixedYear1Year2 = dataObject.PupilsMixedYear1Year2,
            PupilsMixedYear2Year3 = dataObject.PupilsMixedYear2Year3,
            PupilsMixedYear3Year4 = dataObject.PupilsMixedYear3Year4,
            PupilsMixedYear4Year5 = dataObject.PupilsMixedYear4Year5,
            PupilsMixedYear5Year6 = dataObject.PupilsMixedYear5Year6,
            PupilsReception = dataObject.PupilsReception,
            PupilsYear1 = dataObject.PupilsYear1,
            PupilsYear2 = dataObject.PupilsYear2,
            PupilsYear3 = dataObject.PupilsYear3,
            PupilsYear4 = dataObject.PupilsYear4,
            PupilsYear5 = dataObject.PupilsYear5,
            PupilsYear6 = dataObject.PupilsYear6,
            PupilsYear7 = dataObject.PupilsYear7,
            PupilsYear8 = dataObject.PupilsYear8,
            PupilsYear9 = dataObject.PupilsYear9,
            PupilsYear10 = dataObject.PupilsYear10,
            PupilsYear11 = dataObject.PupilsYear11,
            PupilsYear12 = dataObject.PupilsYear12,
            PupilsYear13 = dataObject.PupilsYear13,
            TeachersNursery = dataObject.TeachersNursery,
            TeachersMixedReceptionYear1 = dataObject.TeachersMixedReceptionYear1,
            TeachersMixedYear1Year2 = dataObject.TeachersMixedYear1Year2,
            TeachersMixedYear2Year3 = dataObject.TeachersMixedYear2Year3,
            TeachersMixedYear3Year4 = dataObject.TeachersMixedYear3Year4,
            TeachersMixedYear4Year5 = dataObject.TeachersMixedYear4Year5,
            TeachersMixedYear5Year6 = dataObject.TeachersMixedYear5Year6,
            TeachersReception = dataObject.TeachersReception,
            TeachersYear1 = dataObject.TeachersYear1,
            TeachersYear2 = dataObject.TeachersYear2,
            TeachersYear3 = dataObject.TeachersYear3,
            TeachersYear4 = dataObject.TeachersYear4,
            TeachersYear5 = dataObject.TeachersYear5,
            TeachersYear6 = dataObject.TeachersYear6,
            TeachersYear7 = dataObject.TeachersYear7,
            TeachersYear8 = dataObject.TeachersYear8,
            TeachersYear9 = dataObject.TeachersYear9,
            TeachersYear10 = dataObject.TeachersYear10,
            TeachersYear11 = dataObject.TeachersYear11,
            TeachersYear12 = dataObject.TeachersYear12,
            TeachersYear13 = dataObject.TeachersYear13,
            AssistantsMixedReceptionYear1 = dataObject.AssistantsMixedReceptionYear1,
            AssistantsMixedYear1Year2 = dataObject.AssistantsMixedYear1Year2,
            AssistantsMixedYear2Year3 = dataObject.AssistantsMixedYear2Year3,
            AssistantsMixedYear3Year4 = dataObject.AssistantsMixedYear3Year4,
            AssistantsMixedYear4Year5 = dataObject.AssistantsMixedYear4Year5,
            AssistantsMixedYear5Year6 = dataObject.AssistantsMixedYear5Year6,
            AssistantsNursery = dataObject.AssistantsNursery,
            AssistantsReception = dataObject.AssistantsReception,
            AssistantsYear1 = dataObject.AssistantsYear1,
            AssistantsYear2 = dataObject.AssistantsYear2,
            AssistantsYear3 = dataObject.AssistantsYear3,
            AssistantsYear4 = dataObject.AssistantsYear4,
            AssistantsYear5 = dataObject.AssistantsYear5,
            AssistantsYear6 = dataObject.AssistantsYear6,
            OtherTeachingPeriods = dataObject.OtherTeachingPeriods?
                .Select(x => new OtherTeachingPeriod
                {
                    PeriodName = x.PeriodName,
                    PeriodsPerTimetable = x.PeriodsPerTimetable
                }).ToArray(),
            ManagementRoleHeadteacher = dataObject.ManagementRoleHeadteacher,
            ManagementRoleDeputyHeadteacher = dataObject.ManagementRoleDeputyHeadteacher,
            ManagementRoleNumeracyLead = dataObject.ManagementRoleNumeracyLead,
            ManagementRoleLiteracyLead = dataObject.ManagementRoleLiteracyLead,
            ManagementRoleHeadSmallCurriculum = dataObject.ManagementRoleHeadSmallCurriculum,
            ManagementRoleHeadKs1 = dataObject.ManagementRoleHeadKs1,
            ManagementRoleHeadKs2 = dataObject.ManagementRoleHeadKs2,
            ManagementRoleSenco = dataObject.ManagementRoleSenco,
            ManagementRoleAssistantHeadteacher = dataObject.ManagementRoleAssistantHeadteacher,
            ManagementRoleHeadLargeCurriculum = dataObject.ManagementRoleHeadLargeCurriculum,
            ManagementRolePastoralLeader = dataObject.ManagementRolePastoralLeader,
            ManagementRoleOtherMembers = dataObject.ManagementRoleOtherMembers,
            NumberHeadteacher = dataObject.NumberHeadteacher,
            NumberDeputyHeadteacher = dataObject.NumberDeputyHeadteacher,
            NumberNumeracyLead = dataObject.NumberNumeracyLead,
            NumberLiteracyLead = dataObject.NumberLiteracyLead,
            NumberHeadSmallCurriculum = dataObject.NumberHeadSmallCurriculum,
            NumberHeadKs1 = dataObject.NumberHeadKs1,
            NumberHeadKs2 = dataObject.NumberHeadKs2,
            NumberSenco = dataObject.NumberSenco,
            NumberAssistantHeadteacher = dataObject.NumberAssistantHeadteacher,
            NumberHeadLargeCurriculum = dataObject.NumberHeadLargeCurriculum,
            NumberPastoralLeader = dataObject.NumberPastoralLeader,
            NumberOtherMembers = dataObject.NumberOtherMembers,
            TeachingPeriodsHeadteacher = dataObject.TeachingPeriodsHeadteacher,
            TeachingPeriodsDeputyHeadteacher = dataObject.TeachingPeriodsDeputyHeadteacher,
            TeachingPeriodsNumeracyLead = dataObject.TeachingPeriodsNumeracyLead,
            TeachingPeriodsLiteracyLead = dataObject.TeachingPeriodsLiteracyLead,
            TeachingPeriodsHeadSmallCurriculum = dataObject.TeachingPeriodsHeadSmallCurriculum,
            TeachingPeriodsHeadKs1 = dataObject.TeachingPeriodsHeadKs1,
            TeachingPeriodsHeadKs2 = dataObject.TeachingPeriodsHeadKs2,
            TeachingPeriodsSenco = dataObject.TeachingPeriodsSenco,
            TeachingPeriodsAssistantHeadteacher = dataObject.TeachingPeriodsAssistantHeadteacher,
            TeachingPeriodsHeadLargeCurriculum = dataObject.TeachingPeriodsHeadLargeCurriculum,
            TeachingPeriodsPastoralLeader = dataObject.TeachingPeriodsPastoralLeader,
            TeachingPeriodsOtherMembers = dataObject.TeachingPeriodsOtherMembers,
        };
    }

    public class OtherTeachingPeriod
    {
        public string? PeriodName { get; set; }
        public string? PeriodsPerTimetable { get; set; }
    }
}