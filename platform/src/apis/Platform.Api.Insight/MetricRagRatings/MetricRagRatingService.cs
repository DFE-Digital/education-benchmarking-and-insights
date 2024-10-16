using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Platform.Sql;
namespace Platform.Api.Insight.MetricRagRatings;

public interface IMetricRagRatingsService
{
    Task<IEnumerable<MetricRagRating>> QueryAsync(string[] urns, string[] categories, string[] statuses,
        string runType = "default", bool includeSubCategories = false);

    Task<IEnumerable<MetricRagRating>> UserDefinedAsync(string identifier, string runType = "default", bool includeSubCategories = false);
}

public class MetricRagRatingsService(IDatabaseFactory dbFactory) : IMetricRagRatingsService
{
    public async Task<IEnumerable<MetricRagRating>> QueryAsync(string[] urns, string[] categories, string[] statuses,
        string runType = "default", bool includeSubCategories = false)
    {
        const string paramSql = "SELECT Value from Parameters where Name = 'CurrentYear'";

        using var conn = await dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(paramSql);

        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from MetricRAG /**where**/");
        builder.Where("RunType = @RunType AND RunId = @RunId AND URN IN @URNS",
            new
            {
                RunType = runType,
                RunId = year,
                URNS = urns
            });

        if (!includeSubCategories)
        {
            builder.Where("SubCategory = 'Total'");
        }

        if (categories.Any())
        {
            builder.Where("Category IN @categories", new
            {
                categories
            });
        }

        if (statuses.Any())
        {
            builder.Where("RAG IN @statuses", new
            {
                statuses
            });
        }

        return await conn.QueryAsync<MetricRagRating>(template.RawSql, template.Parameters);
    }

    public async Task<IEnumerable<MetricRagRating>> UserDefinedAsync(string identifier, string runType = "default",
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