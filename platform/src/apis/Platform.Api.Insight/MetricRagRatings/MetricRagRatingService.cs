﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Insight.MetricRagRatings;

public interface IMetricRagRatingService
{
    Task<IEnumerable<MetricRagRating>> QueryAsync(string[] urns, string[] categories, string[] statuses,
        string runType = "default", string setType = "unmixed", bool includeSubCategories = false);
}

public class MetricRagRatingService : IMetricRagRatingService
{
    private readonly IDatabaseFactory _dbFactory;

    public MetricRagRatingService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }


    public async Task<IEnumerable<MetricRagRating>> QueryAsync(string[] urns, string[] categories, string[] statuses,
        string runType = "default", string setType = "unmixed", bool includeSubCategories = false)
    {
        const string paramSql = "SELECT Value from Parameters where Name = 'CurrentYear'";

        using var conn = await _dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(paramSql);

        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from MetricRAG /**where**/");
        builder.Where("RunType = @RunType AND RunId = @RunId AND SetType = @SetType AND URN IN @URNS",
            new { RunType = runType, RunId = year, SetType = setType, URNS = urns });

        if (!includeSubCategories)
        {
            builder.Where("SubCategory = 'Total'");
        }

        if (categories.Any())
        {
            builder.Where("Category IN @categories", new { categories });
        }

        if (statuses.Any())
        {
            builder.Where("RAG IN @statuses", new { statuses });
        }

        return await conn.QueryAsync<MetricRagRating>(template.RawSql, template.Parameters);
    }
}