using System.Collections.Generic;
using System.Threading.Tasks;
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
        using var conn = await dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(Queries.GetCurrentYear);
        var template = Queries.GetMetricRag(year, runType, includeSubCategories, urns,categories, statuses);
        return await conn.QueryAsync<MetricRagRating>(template);
    }

    public async Task<IEnumerable<MetricRagRating>> UserDefinedAsync(string identifier, string runType = "default",
        bool includeSubCategories = false)
    {
        using var conn = await dbFactory.GetConnection();
        var template = Queries.GetMetricRag(identifier, runType, includeSubCategories);
        return await conn.QueryAsync<MetricRagRating>(template);
    }
}