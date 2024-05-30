using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.FinancialPlans;

public interface IFinancialPlanService
{
    Task<IEnumerable<FinancialPlanSummary>> QueryAsync(string urn);
    Task<FinancialPlanDetails?> DetailsAsync(string urn, int year);
    Task<Result> UpsertAsync(string urn, int year, FinancialPlanDetails plan);
    Task DeleteAsync(string urn, int year);
}

[ExcludeFromCodeCoverage]
public class FinancialPlanService : IFinancialPlanService
{
    private readonly IDatabaseFactory _dbFactory;

    public FinancialPlanService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<FinancialPlanSummary>> QueryAsync(string urn)
    {
        const string sql = "SELECT * from FinancialPlan where URN = @URN";
        var parameters = new { URN = int.Parse(urn) };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<FinancialPlanSummary>(sql, parameters);
    }

    public async Task<FinancialPlanDetails?> DetailsAsync(string urn, int year)
    {
        var result = await GetFinancialPlan(urn, year);
        return result?.Input?.FromJson<FinancialPlanDetails>();
    }

    public async Task<Result> UpsertAsync(string urn, int year, FinancialPlanDetails plan)
    {
        var existing = await GetFinancialPlan(urn, year);
        if (existing == null)
        {
            return await Create(urn, year, plan);
        }

        return await Update(plan, existing);
    }

    public async Task DeleteAsync(string urn, int year)
    {
        var existing = await GetFinancialPlan(urn, year);

        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(existing, transaction);

        transaction.Commit();
    }

    private async Task<FinancialPlan?> GetFinancialPlan(string urn, int year)
    {
        const string sql = "SELECT * from FinancialPlan where URN = @URN AND Year = @Year";
        var parameters = new { URN = int.Parse(urn), Year = year };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<FinancialPlan>(sql, parameters);
    }

    private async Task<Result> Update(FinancialPlanDetails plan, FinancialPlan existing)
    {
        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        existing.UpdatedAt = DateTimeOffset.UtcNow;
        existing.UpdatedBy = plan.UpdatedBy;
        existing.Version += 1;
        existing.IsComplete = plan.IsComplete;
        existing.Input = plan.ToJson(Formatting.None);

        await connection.UpdateAsync(existing, transaction);

        transaction.Commit();

        return new UpdatedResult();
    }

    private async Task<Result> Create(string urn, int year, FinancialPlanDetails plan)
    {
        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        var newPlan = new FinancialPlan
        {
            Year = year,
            Urn = urn,
            Created = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            UpdatedBy = plan.UpdatedBy,
            CreatedBy = plan.UpdatedBy,
            Version = 1,
            IsComplete = plan.IsComplete,
            Input = plan.ToJson(Formatting.None)
        };

        await connection.InsertAsync(plan, transaction);

        transaction.Commit();

        return new CreatedResult<FinancialPlan>(newPlan, $"financial-plan/{urn}/{year}");
    }
}