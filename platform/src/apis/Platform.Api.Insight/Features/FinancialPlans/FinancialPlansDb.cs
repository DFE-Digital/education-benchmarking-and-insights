using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Features.FinancialPlans;

public interface IFinancialPlansDb
{
    Task<IEnumerable<FinancialPlanResponseModel>> QueryFinancialPlan(string urn);
    Task<FinancialPlanResponseModel?> SingleFinancialPlan(string urn, int year);
    Task UpsertFinancialPlan(string urn, int year, FinancialPlanRequestModel request);
    Task DeleteFinancialPlan(FinancialPlanResponseModel plan);
}

[ExcludeFromCodeCoverage]
public class FinancialPlansDb : CosmosDatabase, IFinancialPlansDb
{
    private readonly string _collectionName;

    public FinancialPlansDb(IOptions<DbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.FinancialPlanCollectionName);
        _collectionName = options.Value.FinancialPlanCollectionName;
    }

    public async Task<IEnumerable<FinancialPlanResponseModel>> QueryFinancialPlan(string urn)
    {
        var plans = await ItemEnumerableAsync<FinancialPlanDataObject>(_collectionName, q => q.Where(x => x.PartitionKey == urn)).ToArrayAsync();
        return plans.Select(FinancialPlanResponseModel.Create);
    }


    public async Task<FinancialPlanResponseModel?> SingleFinancialPlan(string urn, int year)
    {
        var response = await ReadItemStreamAsync(_collectionName, year.ToString(), urn);
        return response.IsSuccessStatusCode
            ? FinancialPlanResponseModel.Create(response.Content.FromJson<FinancialPlanDataObject>())
            : null;
    }

    public async Task UpsertFinancialPlan(string urn, int year, FinancialPlanRequestModel request)
    {
        var response = await ReadItemStreamAsync(_collectionName, year.ToString(), urn);
        if (!response.IsSuccessStatusCode)
        {
            await Create(urn, year, request);
            return;
        }

        var existing = response.Content.FromJson<FinancialPlanDataObject>();
        await Update(request, existing);
    }

    public async Task DeleteFinancialPlan(FinancialPlanResponseModel plan)
    {
        await DeleteItemAsync<FinancialPlanDataObject>(_collectionName, plan.Year.ToString(),
            new PartitionKey(plan.Urn));
    }

    private async Task Update(FinancialPlanRequestModel request, FinancialPlanDataObject existing)
    {
        existing.UpdatedAt = DateTimeOffset.UtcNow;
        existing.UpdatedBy = request.User;
        existing.Version += 1;
        existing.IsComplete = request.IsComplete;
        existing.TargetContactRatio = request.TargetContactRatio;
        existing.UseFigures = request.UseFigures;
        existing.TotalIncome = request.TotalIncome;
        existing.TotalExpenditure = request.TotalExpenditure;
        existing.TotalTeacherCosts = request.TotalTeacherCosts;
        existing.TotalNumberOfTeachersFte = request.TotalNumberOfTeachersFte;
        existing.EducationSupportStaffCosts = request.EducationSupportStaffCosts;
        existing.TimetablePeriods = request.TimetablePeriods;
        existing.HasMixedAgeClasses = request.HasMixedAgeClasses;
        existing.MixedAgeReceptionYear1 = request.MixedAgeReceptionYear1;
        existing.MixedAgeYear1Year2 = request.MixedAgeYear1Year2;
        existing.MixedAgeYear2Year3 = request.MixedAgeYear2Year3;
        existing.MixedAgeYear3Year4 = request.MixedAgeYear3Year4;
        existing.MixedAgeYear4Year5 = request.MixedAgeYear4Year5;
        existing.MixedAgeYear5Year6 = request.MixedAgeYear5Year6;
        existing.PupilsNursery = request.PupilsNursery;
        existing.PupilsMixedReceptionYear1 = request.PupilsMixedReceptionYear1;
        existing.PupilsMixedYear1Year2 = request.PupilsMixedYear1Year2;
        existing.PupilsMixedYear2Year3 = request.PupilsMixedYear2Year3;
        existing.PupilsMixedYear3Year4 = request.PupilsMixedYear3Year4;
        existing.PupilsMixedYear4Year5 = request.PupilsMixedYear4Year5;
        existing.PupilsMixedYear5Year6 = request.PupilsMixedYear5Year6;
        existing.PupilsReception = request.PupilsReception;
        existing.PupilsYear1 = request.PupilsYear1;
        existing.PupilsYear2 = request.PupilsYear2;
        existing.PupilsYear3 = request.PupilsYear3;
        existing.PupilsYear4 = request.PupilsYear4;
        existing.PupilsYear5 = request.PupilsYear5;
        existing.PupilsYear6 = request.PupilsYear6;
        existing.PupilsYear7 = request.PupilsYear7;
        existing.PupilsYear8 = request.PupilsYear8;
        existing.PupilsYear9 = request.PupilsYear9;
        existing.PupilsYear10 = request.PupilsYear10;
        existing.PupilsYear11 = request.PupilsYear11;
        existing.PupilsYear12 = request.PupilsYear12;
        existing.PupilsYear13 = request.PupilsYear13;
        existing.TeachersNursery = request.TeachersNursery;
        existing.TeachersMixedReceptionYear1 = request.TeachersMixedReceptionYear1;
        existing.TeachersMixedYear1Year2 = request.TeachersMixedYear1Year2;
        existing.TeachersMixedYear2Year3 = request.TeachersMixedYear2Year3;
        existing.TeachersMixedYear3Year4 = request.TeachersMixedYear3Year4;
        existing.TeachersMixedYear4Year5 = request.TeachersMixedYear4Year5;
        existing.TeachersMixedYear5Year6 = request.TeachersMixedYear5Year6;
        existing.TeachersReception = request.TeachersReception;
        existing.TeachersYear1 = request.TeachersYear1;
        existing.TeachersYear2 = request.TeachersYear2;
        existing.TeachersYear3 = request.TeachersYear3;
        existing.TeachersYear4 = request.TeachersYear4;
        existing.TeachersYear5 = request.TeachersYear5;
        existing.TeachersYear6 = request.TeachersYear6;
        existing.TeachersYear7 = request.TeachersYear7;
        existing.TeachersYear8 = request.TeachersYear8;
        existing.TeachersYear9 = request.TeachersYear9;
        existing.TeachersYear10 = request.TeachersYear10;
        existing.TeachersYear11 = request.TeachersYear11;
        existing.TeachersYear12 = request.TeachersYear12;
        existing.TeachersYear13 = request.TeachersYear13;
        existing.AssistantsMixedReceptionYear1 = request.AssistantsMixedReceptionYear1;
        existing.AssistantsMixedYear1Year2 = request.AssistantsMixedYear1Year2;
        existing.AssistantsMixedYear2Year3 = request.AssistantsMixedYear2Year3;
        existing.AssistantsMixedYear3Year4 = request.AssistantsMixedYear3Year4;
        existing.AssistantsMixedYear4Year5 = request.AssistantsMixedYear4Year5;
        existing.AssistantsMixedYear5Year6 = request.AssistantsMixedYear5Year6;
        existing.AssistantsNursery = request.AssistantsNursery;
        existing.AssistantsReception = request.AssistantsReception;
        existing.AssistantsYear1 = request.AssistantsYear1;
        existing.AssistantsYear2 = request.AssistantsYear2;
        existing.AssistantsYear3 = request.AssistantsYear3;
        existing.AssistantsYear4 = request.AssistantsYear4;
        existing.AssistantsYear5 = request.AssistantsYear5;
        existing.AssistantsYear6 = request.AssistantsYear6;
        existing.OtherTeachingPeriods = request.OtherTeachingPeriods?
            .Select(x => new TeachingPeriodDataObject
            {
                PeriodName = x.PeriodName,
                PeriodsPerTimetable = x.PeriodsPerTimetable
            }).ToArray();
        existing.ManagementRoleHeadteacher = request.ManagementRoleHeadteacher;
        existing.ManagementRoleDeputyHeadteacher = request.ManagementRoleDeputyHeadteacher;
        existing.ManagementRoleNumeracyLead = request.ManagementRoleNumeracyLead;
        existing.ManagementRoleLiteracyLead = request.ManagementRoleLiteracyLead;
        existing.ManagementRoleHeadSmallCurriculum = request.ManagementRoleHeadSmallCurriculum;
        existing.ManagementRoleHeadKs1 = request.ManagementRoleHeadKs1;
        existing.ManagementRoleHeadKs2 = request.ManagementRoleHeadKs2;
        existing.ManagementRoleSenco = request.ManagementRoleSenco;
        existing.ManagementRoleAssistantHeadteacher = request.ManagementRoleAssistantHeadteacher;
        existing.ManagementRoleHeadLargeCurriculum = request.ManagementRoleHeadLargeCurriculum;
        existing.ManagementRolePastoralLeader = request.ManagementRolePastoralLeader;
        existing.ManagementRoleOtherMembers = request.ManagementRoleOtherMembers;
        existing.NumberHeadteacher = request.NumberHeadteacher;
        existing.NumberDeputyHeadteacher = request.NumberDeputyHeadteacher;
        existing.NumberNumeracyLead = request.NumberNumeracyLead;
        existing.NumberLiteracyLead = request.NumberLiteracyLead;
        existing.NumberHeadSmallCurriculum = request.NumberHeadSmallCurriculum;
        existing.NumberHeadKs1 = request.NumberHeadKs1;
        existing.NumberHeadKs2 = request.NumberHeadKs2;
        existing.NumberSenco = request.NumberSenco;
        existing.NumberAssistantHeadteacher = request.NumberAssistantHeadteacher;
        existing.NumberHeadLargeCurriculum = request.NumberHeadLargeCurriculum;
        existing.NumberPastoralLeader = request.NumberPastoralLeader;
        existing.NumberOtherMembers = request.NumberOtherMembers;
        existing.TeachingPeriodsHeadteacher = request.TeachingPeriodsHeadteacher;
        existing.TeachingPeriodsDeputyHeadteacher = request.TeachingPeriodsDeputyHeadteacher;
        existing.TeachingPeriodsNumeracyLead = request.TeachingPeriodsNumeracyLead;
        existing.TeachingPeriodsLiteracyLead = request.TeachingPeriodsLiteracyLead;
        existing.TeachingPeriodsHeadSmallCurriculum = request.TeachingPeriodsHeadSmallCurriculum;
        existing.TeachingPeriodsHeadKs1 = request.TeachingPeriodsHeadKs1;
        existing.TeachingPeriodsHeadKs2 = request.TeachingPeriodsHeadKs2;
        existing.TeachingPeriodsSenco = request.TeachingPeriodsSenco;
        existing.TeachingPeriodsAssistantHeadteacher = request.TeachingPeriodsAssistantHeadteacher;
        existing.TeachingPeriodsHeadLargeCurriculum = request.TeachingPeriodsHeadLargeCurriculum;
        existing.TeachingPeriodsPastoralLeader = request.TeachingPeriodsPastoralLeader;
        existing.TeachingPeriodsOtherMembers = request.TeachingPeriodsOtherMembers;

        await UpsertItemAsync(_collectionName, existing, new PartitionKey(existing.PartitionKey));
    }

    private async Task Create(string urn, int year, FinancialPlanRequestModel request)
    {
        var plan = new FinancialPlanDataObject
        {
            Id = year.ToString(),
            PartitionKey = urn,
            Created = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            UpdatedBy = request.User,
            CreatedBy = request.User,
            Version = 1,
            IsComplete = request.IsComplete,
            TargetContactRatio = request.TargetContactRatio,
            UseFigures = request.UseFigures,
            TotalIncome = request.TotalIncome,
            TotalExpenditure = request.TotalExpenditure,
            TotalTeacherCosts = request.TotalTeacherCosts,
            TotalNumberOfTeachersFte = request.TotalNumberOfTeachersFte,
            EducationSupportStaffCosts = request.EducationSupportStaffCosts,
            TimetablePeriods = request.TimetablePeriods,
            HasMixedAgeClasses = request.HasMixedAgeClasses,
            MixedAgeReceptionYear1 = request.MixedAgeReceptionYear1,
            MixedAgeYear1Year2 = request.MixedAgeYear1Year2,
            MixedAgeYear2Year3 = request.MixedAgeYear2Year3,
            MixedAgeYear3Year4 = request.MixedAgeYear3Year4,
            MixedAgeYear4Year5 = request.MixedAgeYear4Year5,
            MixedAgeYear5Year6 = request.MixedAgeYear5Year6,
            PupilsMixedReceptionYear1 = request.PupilsMixedReceptionYear1,
            PupilsMixedYear1Year2 = request.PupilsMixedYear1Year2,
            PupilsMixedYear2Year3 = request.PupilsMixedYear2Year3,
            PupilsMixedYear3Year4 = request.PupilsMixedYear3Year4,
            PupilsMixedYear4Year5 = request.PupilsMixedYear4Year5,
            PupilsMixedYear5Year6 = request.PupilsMixedYear5Year6,
            PupilsNursery = request.PupilsNursery,
            PupilsReception = request.PupilsReception,
            PupilsYear1 = request.PupilsYear1,
            PupilsYear2 = request.PupilsYear2,
            PupilsYear3 = request.PupilsYear3,
            PupilsYear4 = request.PupilsYear4,
            PupilsYear5 = request.PupilsYear5,
            PupilsYear6 = request.PupilsYear6,
            PupilsYear7 = request.PupilsYear7,
            PupilsYear8 = request.PupilsYear8,
            PupilsYear9 = request.PupilsYear9,
            PupilsYear10 = request.PupilsYear10,
            PupilsYear11 = request.PupilsYear11,
            PupilsYear12 = request.PupilsYear12,
            PupilsYear13 = request.PupilsYear13,
            TeachersNursery = request.TeachersNursery,
            TeachersMixedReceptionYear1 = request.TeachersMixedReceptionYear1,
            TeachersMixedYear1Year2 = request.TeachersMixedYear1Year2,
            TeachersMixedYear2Year3 = request.TeachersMixedYear2Year3,
            TeachersMixedYear3Year4 = request.TeachersMixedYear3Year4,
            TeachersMixedYear4Year5 = request.TeachersMixedYear4Year5,
            TeachersMixedYear5Year6 = request.TeachersMixedYear5Year6,
            TeachersReception = request.TeachersReception,
            TeachersYear1 = request.TeachersYear1,
            TeachersYear2 = request.TeachersYear2,
            TeachersYear3 = request.TeachersYear3,
            TeachersYear4 = request.TeachersYear4,
            TeachersYear5 = request.TeachersYear5,
            TeachersYear6 = request.TeachersYear6,
            TeachersYear7 = request.TeachersYear7,
            TeachersYear8 = request.TeachersYear8,
            TeachersYear9 = request.TeachersYear9,
            TeachersYear10 = request.TeachersYear10,
            TeachersYear11 = request.TeachersYear11,
            TeachersYear12 = request.TeachersYear12,
            TeachersYear13 = request.TeachersYear13,
            AssistantsMixedReceptionYear1 = request.AssistantsMixedReceptionYear1,
            AssistantsMixedYear1Year2 = request.AssistantsMixedYear1Year2,
            AssistantsMixedYear2Year3 = request.AssistantsMixedYear2Year3,
            AssistantsMixedYear3Year4 = request.AssistantsMixedYear3Year4,
            AssistantsMixedYear4Year5 = request.AssistantsMixedYear4Year5,
            AssistantsMixedYear5Year6 = request.AssistantsMixedYear5Year6,
            AssistantsNursery = request.AssistantsNursery,
            AssistantsReception = request.AssistantsReception,
            AssistantsYear1 = request.AssistantsYear1,
            AssistantsYear2 = request.AssistantsYear2,
            AssistantsYear3 = request.AssistantsYear3,
            AssistantsYear4 = request.AssistantsYear4,
            AssistantsYear5 = request.AssistantsYear5,
            AssistantsYear6 = request.AssistantsYear6,
            OtherTeachingPeriods = request.OtherTeachingPeriods?
                .Select(x => new TeachingPeriodDataObject
                {
                    PeriodName = x.PeriodName,
                    PeriodsPerTimetable = x.PeriodsPerTimetable
                }).ToArray(),
            ManagementRoleHeadteacher = request.ManagementRoleHeadteacher,
            ManagementRoleDeputyHeadteacher = request.ManagementRoleDeputyHeadteacher,
            ManagementRoleNumeracyLead = request.ManagementRoleNumeracyLead,
            ManagementRoleLiteracyLead = request.ManagementRoleLiteracyLead,
            ManagementRoleHeadSmallCurriculum = request.ManagementRoleHeadSmallCurriculum,
            ManagementRoleHeadKs1 = request.ManagementRoleHeadKs1,
            ManagementRoleHeadKs2 = request.ManagementRoleHeadKs2,
            ManagementRoleSenco = request.ManagementRoleSenco,
            ManagementRoleAssistantHeadteacher = request.ManagementRoleAssistantHeadteacher,
            ManagementRoleHeadLargeCurriculum = request.ManagementRoleHeadLargeCurriculum,
            ManagementRolePastoralLeader = request.ManagementRolePastoralLeader,
            ManagementRoleOtherMembers = request.ManagementRoleOtherMembers,
            NumberHeadteacher = request.NumberHeadteacher,
            NumberDeputyHeadteacher = request.NumberDeputyHeadteacher,
            NumberNumeracyLead = request.NumberNumeracyLead,
            NumberLiteracyLead = request.NumberLiteracyLead,
            NumberHeadSmallCurriculum = request.NumberHeadSmallCurriculum,
            NumberHeadKs1 = request.NumberHeadKs1,
            NumberHeadKs2 = request.NumberHeadKs2,
            NumberSenco = request.NumberSenco,
            NumberAssistantHeadteacher = request.NumberAssistantHeadteacher,
            NumberHeadLargeCurriculum = request.NumberHeadLargeCurriculum,
            NumberPastoralLeader = request.NumberPastoralLeader,
            NumberOtherMembers = request.NumberOtherMembers,
            TeachingPeriodsHeadteacher = request.TeachingPeriodsHeadteacher,
            TeachingPeriodsDeputyHeadteacher = request.TeachingPeriodsDeputyHeadteacher,
            TeachingPeriodsNumeracyLead = request.TeachingPeriodsNumeracyLead,
            TeachingPeriodsLiteracyLead = request.TeachingPeriodsLiteracyLead,
            TeachingPeriodsHeadSmallCurriculum = request.TeachingPeriodsHeadSmallCurriculum,
            TeachingPeriodsHeadKs1 = request.TeachingPeriodsHeadKs1,
            TeachingPeriodsHeadKs2 = request.TeachingPeriodsHeadKs2,
            TeachingPeriodsSenco = request.TeachingPeriodsSenco,
            TeachingPeriodsAssistantHeadteacher = request.TeachingPeriodsAssistantHeadteacher,
            TeachingPeriodsHeadLargeCurriculum = request.TeachingPeriodsHeadLargeCurriculum,
            TeachingPeriodsPastoralLeader = request.TeachingPeriodsPastoralLeader,
            TeachingPeriodsOtherMembers = request.TeachingPeriodsOtherMembers
        };

        await UpsertItemAsync(_collectionName, plan, new PartitionKey(urn));
    }
}