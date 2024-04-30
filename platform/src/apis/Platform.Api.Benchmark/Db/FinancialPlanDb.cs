using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.Db;

public interface IFinancialPlanDb
{
    Task<IEnumerable<FinancialPlanResponseModel>> QueryFinancialPlan(string urn);
    Task<FinancialPlanInputResponseModel?> SingleFinancialPlanInput(string urn, int year);
    Task<Result> UpsertFinancialPlan(string urn, int year, FinancialPlanInputRequestModel inputRequest);
    Task DeleteFinancialPlan(string urn, int year);
}

public class FinancialPlanDb : IFinancialPlanDb
{
    private readonly IDatabaseFactory _dbFactory;

    public FinancialPlanDb(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<FinancialPlanResponseModel>> QueryFinancialPlan(string urn)
    {
        const string sql = "SELECT * from FinancialPlan where URN = @URN";
        var parameters = new { URN = int.Parse(urn) };

        using var conn = await _dbFactory.GetConnection();
        var results = conn.Query<FinancialPlanDataObject>(sql, parameters);

        return results.Select(FinancialPlanFactory.CreateResponse);
    }

    public async Task<FinancialPlanInputResponseModel?> SingleFinancialPlanInput(string urn, int year)
    {
        var result = await GetFinancialPlan(urn, year);

        return result?.Input?.FromJson<FinancialPlanInputResponseModel>();
    }

    public async Task<Result> UpsertFinancialPlan(string urn, int year, FinancialPlanInputRequestModel inputRequest)
    {
        var existing = await GetFinancialPlan(urn, year);
        if (existing == null)
        {
            return await Create(urn, year, inputRequest);
        }

        return await Update(inputRequest, existing);
    }

    public async Task DeleteFinancialPlan(string urn, int year)
    {
        var existing = await GetFinancialPlan(urn, year);

        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(existing, transaction);

        transaction.Commit();
    }

    private async Task<FinancialPlanDataObject?> GetFinancialPlan(string urn, int year)
    {
        const string sql = "SELECT * from FinancialPlan where URN = @URN AND Year = @Year";
        var parameters = new { URN = int.Parse(urn), Year = year };

        using var conn = await _dbFactory.GetConnection();
        var results = conn.Query<FinancialPlanDataObject>(sql, parameters);
        return results.FirstOrDefault();
    }

    private async Task<Result> Update(FinancialPlanInputRequestModel inputRequest, FinancialPlanDataObject existing)
    {
        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        existing.UpdatedAt = DateTimeOffset.UtcNow;
        existing.UpdatedBy = inputRequest.User;
        existing.Version += 1;
        existing.IsComplete = inputRequest.IsComplete;
        existing.Input = inputRequest.ToJson(Formatting.None);

        await connection.UpdateAsync(existing, transaction);

        transaction.Commit();

        return new UpdatedResult();
    }

    private async Task<Result> Create(string urn, int year, FinancialPlanInputRequestModel inputRequest)
    {
        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        var plan = new FinancialPlanDataObject
        {
            Year = year,
            Urn = urn,
            Created = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            UpdatedBy = inputRequest.User,
            CreatedBy = inputRequest.User,
            Version = 1,
            IsComplete = inputRequest.IsComplete,
            Input = inputRequest.ToJson(Formatting.None)
        };

        await connection.InsertAsync(plan, transaction);

        transaction.Commit();

        return new CreatedResult<FinancialPlanDataObject>(plan, $"financial-plan/{urn}/{year}");
    }
}