using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Platform.Api.Insight.Features.MetricRagRatings.Models;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.MetricRagRatings.Services;

public interface IMetricRagRatingsService
{
    Task<IEnumerable<MetricRagRating>> QueryAsync(
        string[] urns,
        string[] categories,
        string[] statuses,
        string? companyNumber,
        string runType = Pipeline.RunType.Default,
        bool includeSubCategories = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<MetricRagRatingSummary>> QuerySummaryAsync(
        string[] urns,
        string? companyNumber,
        string? laCode,
        string? overallPhases,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<MetricRagRating>> UserDefinedAsync(
        string identifier,
        string runType = Pipeline.RunType.Default,
        bool includeSubCategories = false,
        CancellationToken cancellationToken = default);
}

public class MetricRagRatingsService(IDatabaseFactory dbFactory) : IMetricRagRatingsService
{
    public async Task<IEnumerable<MetricRagRating>> QueryAsync(
        string[] urns,
        string[] categories,
        string[] statuses,
        string? companyNumber,
        string runType = Pipeline.RunType.Default,
        bool includeSubCategories = false,
        CancellationToken cancellationToken = default)
    {
        const string paramSql = "SELECT Value from Parameters where Name = 'CurrentYear'";

        using var conn = await dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(paramSql, cancellationToken: cancellationToken);

        var builder = new SchoolMetricRagQuery()
            .WhereRunTypeEqual(runType)
            .WhereRunIdEqual(year);

        if (urns.Length != 0)
        {
            builder = builder.WhereUrnIn(urns);
        }
        else if (!string.IsNullOrWhiteSpace(companyNumber))
        {
            builder = builder.WhereTrustCompanyNumberEqual(companyNumber);
        }
        else
        {
            throw new ArgumentNullException(nameof(urns), $"{nameof(urns)} or {nameof(companyNumber)} must be supplied");
        }

        if (!includeSubCategories)
        {
            builder = builder.WhereSubCategoryEqual("Total");
        }

        if (categories.Length != 0)
        {
            builder = builder.WhereCategoryIn(categories);
        }

        if (statuses.Length != 0)
        {
            builder = builder.WhereRagIn(statuses);
        }

        return await conn.QueryAsync<MetricRagRating>(builder, cancellationToken);
    }

    public async Task<IEnumerable<MetricRagRatingSummary>> QuerySummaryAsync(
        string[] urns,
        string? companyNumber,
        string? laCode,
        string? overallPhase,
        CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();

        var builder = new MetricRagSummaryQuery();

        if (urns.Length != 0)
        {
            builder.WhereUrnIn(urns);
        }
        else if (!string.IsNullOrWhiteSpace(companyNumber))
        {
            builder.WhereTrustCompanyNumberEqual(companyNumber);
        }
        else if (!string.IsNullOrWhiteSpace(laCode))
        {
            builder.WhereLaCodeEqual(laCode)
                .WhereFinanceTypeEqual(FinanceType.Maintained);
        }
        else
        {
            throw new ArgumentNullException($"{nameof(urns)}, {nameof(laCode)} or {nameof(companyNumber)} must be supplied");
        }

        if (!string.IsNullOrWhiteSpace(overallPhase))
        {
            builder.WhereOverallPhaseEqual(overallPhase);
        }

        return await conn.QueryAsync<MetricRagRatingSummary>(builder, cancellationToken);
    }

    public async Task<IEnumerable<MetricRagRating>> UserDefinedAsync(
        string identifier,
        string runType = Pipeline.RunType.Default,
        bool includeSubCategories = false,
        CancellationToken cancellationToken = default)
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

        return await conn.QueryAsync<MetricRagRating>(template.RawSql, template.Parameters, cancellationToken);
    }
}