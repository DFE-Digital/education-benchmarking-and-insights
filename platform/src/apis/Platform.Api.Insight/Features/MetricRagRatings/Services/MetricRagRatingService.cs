﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Platform.Api.Insight.Features.MetricRagRatings.Models;
using Platform.Domain;
using Platform.Sql;

namespace Platform.Api.Insight.Features.MetricRagRatings.Services;

public interface IMetricRagRatingsService
{
    Task<IEnumerable<MetricRagRating>> QueryAsync(
        string[] urns,
        string[] categories,
        string[] statuses,
        string? companyNumber,
        string? laCode,
        string? phase,
        string runType = Pipeline.RunType.Default,
        bool includeSubCategories = false,
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
        string? laCode,
        string? phase,
        string runType = Pipeline.RunType.Default,
        bool includeSubCategories = false,
        CancellationToken cancellationToken = default)
    {
        const string paramSql = "SELECT Value from Parameters where Name = 'CurrentYear'";

        using var conn = await dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(paramSql, cancellationToken: cancellationToken);

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
            builder.Where("LaCode = @LaCode", new
            {
                LaCode = laCode
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

        if (!string.IsNullOrWhiteSpace(phase))
        {
            builder.Where("OverallPhase = @Phase", new
            {
                Phase = phase
            });
        }

        return await conn.QueryAsync<MetricRagRating>(template.RawSql, template.Parameters, cancellationToken);
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