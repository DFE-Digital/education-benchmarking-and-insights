using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Functions;
using Platform.Sql;
namespace Platform.Api.Insight.MetricRagRatings;

public interface IMetricRagRatingsService
{
    Task<IEnumerable<MetricRagRating>> QueryAsync(
        string[] urns,
        string[] categories,
        string[] statuses,
        string? companyNumber,
        string? laCode,
        string? phase,
        string runType = PipelineRunType.Default,
        bool includeSubCategories = false);

    Task<IEnumerable<MetricRagRating>> UserDefinedAsync(string identifier, string runType = PipelineRunType.Default, bool includeSubCategories = false);
}

public class MetricRagRatingsService(IDatabaseFactory dbFactory) : IMetricRagRatingsService
{
    public async Task<IEnumerable<MetricRagRating>> QueryAsync(
        string[] urns,
        string[] categories,
        string[] statuses,
        string? companyNumber,
        string? laCode,
        string? phase,
        string runType = PipelineRunType.Default,
        bool includeSubCategories = false)
    {
        const string paramSql = "SELECT Value from Parameters where Name = 'CurrentYear'";

        using var conn = await dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(paramSql);

        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from SchoolMetricRAG /**where**/");
        builder.Where("RunType = @RunType AND RunId = @RunId", new
        {
            RunType = runType,
            RunId = year
        });

        if (urns.Length != 0)
        {
            builder.Where("URN IN @URNS", new
            {
                URNS = urns
            });
        }
        else if (!string.IsNullOrWhiteSpace(companyNumber))
        {
            builder.Where("TrustCompanyNumber = @CompanyNumber", new
            {
                CompanyNumber = companyNumber
            });
        }
        else if (!string.IsNullOrWhiteSpace(laCode))
        {
            builder.Where("LaCode = @LaCode AND OverallPhase = @Phase", new
            {
                LaCode = laCode,
                Phase = phase
            });
        }
        else
        {
            throw new ArgumentNullException(nameof(urns), $"{nameof(urns)} or {nameof(companyNumber)} or {nameof(laCode)} must be supplied");
        }

        if (!includeSubCategories)
        {
            builder.Where("SubCategory = 'Total'");
        }

        if (categories.Length != 0)
        {
            builder.Where("Category IN @categories", new
            {
                categories
            });
        }

        if (statuses.Length != 0)
        {
            builder.Where("RAG IN @statuses", new
            {
                statuses
            });
        }

        return await conn.QueryAsync<MetricRagRating>(template.RawSql, template.Parameters);
    }

    public async Task<IEnumerable<MetricRagRating>> UserDefinedAsync(string identifier, string runType = PipelineRunType.Default,
        bool includeSubCategories = false)
    {
        using var conn = await dbFactory.GetConnection();

        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from MetricRAG /**where**/");
        builder.Where("RunType = @RunType AND RunId = @RunId",
            new
            {
                RunType = runType,
                RunId = identifier
            });

        if (!includeSubCategories)
        {
            builder.Where("SubCategory = 'Total'");
        }

        return await conn.QueryAsync<MetricRagRating>(template.RawSql, template.Parameters);
    }
}