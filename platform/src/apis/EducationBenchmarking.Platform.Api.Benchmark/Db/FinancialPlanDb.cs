using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
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
        return response.IsSuccessStatusCode ? Domain.Responses.FinancialPlan.Create(response.Content.FromJson<FinancialPlanDataObject>()) : null;
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
        await DeleteItemAsync<FinancialPlan>(_options.FinancialPlanCollectionName, plan.Year.ToString(), new PartitionKey(plan.Urn));
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
        existing.PupilsYear7 = request.PupilsYear7;
        existing.PupilsYear8 = request.PupilsYear8;
        existing.PupilsYear9 = request.PupilsYear9;
        existing.PupilsYear10 = request.PupilsYear10;
        existing.PupilsYear11 = request.PupilsYear11;
        existing.PupilsYear12 = request.PupilsYear12;
        existing.PupilsYear13 = request.PupilsYear13;
        existing.PupilsNursery = request.PupilsNursery;
        existing.PupilsReception = request.PupilsReception;
        existing.PupilsYear1 = request.PupilsYear1;
        existing.PupilsYear2 = request.PupilsYear2;
        existing.PupilsYear3 = request.PupilsYear3;
        existing.PupilsYear4 = request.PupilsYear4;
        existing.PupilsYear5 = request.PupilsYear5;
        existing.PupilsYear6 = request.PupilsYear6;
        existing.PupilsMixedReceptionYear1 = request.PupilsMixedReceptionYear1;
        existing.PupilsMixedYear1Year2 = request.PupilsMixedYear1Year2;
        existing.PupilsMixedYear2Year3 = request.PupilsMixedYear2Year3;
        existing.PupilsMixedYear3Year4 = request.PupilsMixedYear3Year4;
        existing.PupilsMixedYear4Year5 = request.PupilsMixedYear4Year5;
        existing.PupilsMixedYear5Year6 = request.PupilsMixedYear5Year6;

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
            PupilsYear7 = request.PupilsYear7,
            PupilsYear8 = request.PupilsYear8,
            PupilsYear9 = request.PupilsYear9,
            PupilsYear10 = request.PupilsYear10,
            PupilsYear11 = request.PupilsYear11,
            PupilsYear12 = request.PupilsYear12,
            PupilsYear13 = request.PupilsYear13,
            PupilsNursery = request.PupilsNursery,
            PupilsReception = request.PupilsReception,
            PupilsYear1 = request.PupilsYear1,
            PupilsYear2 = request.PupilsYear2,
            PupilsYear3 = request.PupilsYear3,
            PupilsYear4 = request.PupilsYear4,
            PupilsYear5 = request.PupilsYear5,
            PupilsYear6 = request.PupilsYear6,
            PupilsMixedReceptionYear1 = request.PupilsMixedReceptionYear1,
            PupilsMixedYear1Year2 = request.PupilsMixedYear1Year2,
            PupilsMixedYear2Year3 = request.PupilsMixedYear2Year3,
            PupilsMixedYear3Year4 = request.PupilsMixedYear3Year4, 
            PupilsMixedYear4Year5 = request.PupilsMixedYear4Year5, 
            PupilsMixedYear5Year6 = request.PupilsMixedYear5Year6
        };

        await UpsertItemAsync(_options.FinancialPlanCollectionName, plan, new PartitionKey(urn));

        return new CreatedResult<FinancialPlanDataObject>(plan, $"financial-plan/{urn}/{year}");
    }
}