using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Domain.DataObjects;
using EducationBenchmarking.Platform.Domain.Requests;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Benchmark.Db;

public interface IFinancialPlanDb
{
    Task<FinancialPlan?> FinancialPlan(string urn, int year);
    Task<Result> UpsertFinancialPlan(string urn, int year, FinancialPlanRequest request);
    Task DeleteFinancialPlan(FinancialPlan plan);
}

[ExcludeFromCodeCoverage]
public record FinancialPlanDbOptions : CosmosDatabaseOptions
{
    [Required] public string? FinancialPlanCollectionName { get; set; }
}

[ExcludeFromCodeCoverage]
public class FinancialPlanDb : CosmosDatabase, IFinancialPlanDb
{
    private readonly FinancialPlanDbOptions _options;

    public FinancialPlanDb(IOptions<FinancialPlanDbOptions> options) : base(options.Value)
    {
        _options = options.Value;
    }

    public async Task<FinancialPlan?> FinancialPlan(string urn, int year)
    {
        ArgumentNullException.ThrowIfNull(_options.FinancialPlanCollectionName);

        var response = await ReadItemStreamAsync(_options.FinancialPlanCollectionName, year.ToString(), urn);
        return response.IsSuccessStatusCode
            ? Domain.Responses.FinancialPlan.Create(response.Content.FromJson<FinancialPlanDataObject>())
            : null;
    }

    public async Task<Result> UpsertFinancialPlan(string urn, int year, FinancialPlanRequest request)
    {
        ArgumentNullException.ThrowIfNull(_options.FinancialPlanCollectionName);

        var response = await ReadItemStreamAsync(_options.FinancialPlanCollectionName, year.ToString(), urn);
        if (!response.IsSuccessStatusCode)
        {
            return await Create(urn, year, request);
        }

        var existing = response.Content.FromJson<FinancialPlanDataObject>();
        if (existing.Created > DateTimeOffset.UtcNow || existing.UpdatedAt > DateTimeOffset.UtcNow)
        {
            return new DataConflictResult
            {
                ConflictReason = DataConflictResult.Reason.Timestamp,
                Id = existing.Id,
                Type = nameof(Domain.Responses.FinancialPlan),
                CreatedBy = existing.CreatedBy,
                CreatedAt = existing.Created,
                UpdatedBy = existing.UpdatedBy,
                UpdatedAt = existing.UpdatedAt
            };
        }

        return await Update(request, existing);
    }

    public async Task DeleteFinancialPlan(FinancialPlan plan)
    {
        ArgumentNullException.ThrowIfNull(_options.FinancialPlanCollectionName);
        await DeleteItemAsync<FinancialPlan>(_options.FinancialPlanCollectionName, plan.Year.ToString(),
            new PartitionKey(plan.Urn));
    }

    private async Task<Result> Update(FinancialPlanRequest request, FinancialPlanDataObject existing)
    {
        ArgumentNullException.ThrowIfNull(_options.FinancialPlanCollectionName);

        existing.UpdatedAt = DateTimeOffset.UtcNow;
        existing.UpdatedBy = request.User;
        existing.Version += 1;
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
        existing.TeachersYear13 = request.TeachersYear13;
        existing.OtherTeachingPeriods = request.OtherTeachingPeriods?
            .Select(x => new FinancialPlanDataObject.OtherTeachingPeriod
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

        await UpsertItemAsync(_options.FinancialPlanCollectionName, existing, new PartitionKey(existing.PartitionKey));

        return new UpdatedResult();
    }

    private async Task<Result> Create(string urn, int year, FinancialPlanRequest request)
    {
        ArgumentNullException.ThrowIfNull(_options.FinancialPlanCollectionName);

        var plan = new FinancialPlanDataObject
        {
            Id = year.ToString(),
            PartitionKey = urn,
            Created = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            UpdatedBy = request.User,
            CreatedBy = request.User,
            Version = 1,
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
            OtherTeachingPeriods = request.OtherTeachingPeriods?
                .Select(x => new FinancialPlanDataObject.OtherTeachingPeriod
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
            ManagementRoleOtherMembers = request.ManagementRoleOtherMembers
        };

        await UpsertItemAsync(_options.FinancialPlanCollectionName, plan, new PartitionKey(urn));

        return new CreatedResult<FinancialPlanDataObject>(plan, $"financial-plan/{urn}/{year}");
    }
}