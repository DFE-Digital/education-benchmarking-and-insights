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
        };

        await UpsertItemAsync(_options.FinancialPlanCollectionName, plan, new PartitionKey(urn));

        return new CreatedResult<FinancialPlanDataObject>(plan, $"financial-plan/{urn}/{year}");
    }
}