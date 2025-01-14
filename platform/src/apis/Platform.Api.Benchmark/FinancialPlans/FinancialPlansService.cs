using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Json;
using Platform.Sql;

namespace Platform.Api.Benchmark.FinancialPlans;

public interface IFinancialPlansService
{
    Task<IEnumerable<FinancialPlanSummary>> QueryAsync(string[] urns);
    Task<FinancialPlanDetails?> DetailsAsync(string urn, int year);
    Task<FinancialPlanDeployment?> DeploymentPlanAsync(string urn, int year);
    Task<Result> UpsertAsync(string urn, int year, FinancialPlanDetails plan);
    Task DeleteAsync(string urn, int year);
}

[ExcludeFromCodeCoverage]
public class FinancialPlansService : IFinancialPlansService
{
    private readonly IDatabaseFactory _dbFactory;

    public FinancialPlansService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<FinancialPlanSummary>> QueryAsync(string[] urns)
    {
        const string sql = "SELECT * from FinancialPlan where URN IN @URNS";
        var parameters = new { URNS = urns };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<FinancialPlanSummary>(sql, parameters);
    }

    public async Task<FinancialPlanDetails?> DetailsAsync(string urn, int year)
    {
        var result = await GetFinancialPlan(urn, year);
        return result?.Input?.FromJson<FinancialPlanDetails>();
    }

    public async Task<FinancialPlanDeployment?> DeploymentPlanAsync(string urn, int year)
    {
        var result = await GetFinancialPlan(urn, year);
        return result?.DeploymentPlan?.FromJson<FinancialPlanDeployment>();
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
        var parameters = new { URN = urn, Year = year };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<FinancialPlan>(sql, parameters);
    }

    private async Task<Result> Update(FinancialPlanDetails plan, FinancialPlan existing)
    {
        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        var deployment = plan.IsComplete ? DeploymentPlanFactory.Create(plan) : null;

        existing.UpdatedAt = DateTimeOffset.UtcNow;
        existing.UpdatedBy = plan.UpdatedBy;
        existing.Version += 1;
        existing.IsComplete = plan.IsComplete;
        existing.Input = plan.ToJson();
        existing.DeploymentPlan = deployment?.ToJson();
        existing.AverageClassSize = deployment?.AverageClassSize;
        existing.AverageClassSizeRating = deployment?.AverageClassSizeRating;
        existing.TeacherContactRatio = deployment?.TeacherContactRatio;
        existing.ContactRatioRating = deployment?.ContactRatioRating;
        existing.InYearBalancePercentIncomeRating = deployment?.InYearBalancePercentIncomeRating;
        existing.InYearBalance = deployment?.InYearBalance;

        await connection.UpdateAsync(existing, transaction);

        transaction.Commit();

        return new UpdatedResult();
    }

    private async Task<Result> Create(string urn, int year, FinancialPlanDetails plan)
    {
        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        var deployment = plan.IsComplete ? DeploymentPlanFactory.Create(plan) : null;

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
            Input = plan.ToJson(),
            DeploymentPlan = deployment?.ToJson(),
            AverageClassSize = deployment?.AverageClassSize,
            AverageClassSizeRating = deployment?.AverageClassSizeRating,
            TeacherContactRatio = deployment?.TeacherContactRatio,
            ContactRatioRating = deployment?.ContactRatioRating,
            InYearBalancePercentIncomeRating = deployment?.InYearBalancePercentIncomeRating,
            InYearBalance = deployment?.InYearBalance
        };

        await connection.InsertAsync(newPlan, transaction);

        transaction.Commit();

        return new CreatedResult<FinancialPlan>(newPlan, $"financial-plan/{urn}/{year}");
    }
}