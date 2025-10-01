using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Platform.Api.Insight.Features.ItSpend.Models;
using Platform.Api.Insight.Features.ItSpend.Responses;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.ItSpend.Services;

public interface IItSpendService
{
    Task<IEnumerable<ItSpendSchoolResponse>> GetSchoolsAsync(string[] urns, string dimension, CancellationToken cancellationToken = default);
    Task<IEnumerable<ItSpendTrustResponse>> GetTrustsAsync(string[] companyNumbers, CancellationToken cancellationToken = default);
    Task<ItSpendTrustForecastResponse> GetTrustForecastAsync(string? companyNumber, string? year, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class ItSpendService(IDatabaseFactory dbFactory) : IItSpendService
{
    public async Task<IEnumerable<ItSpendSchoolResponse>> GetSchoolsAsync(string[] urns, string dimension,
        CancellationToken cancellationToken = default)
    {
        var builder = new ItSpendSchoolDefaultCurrentQuery(dimension);

        if (urns.Length != 0)
        {
            builder.WhereUrnIn(urns);
        }
        else
        {
            throw new ArgumentNullException(nameof(urns), $"{nameof(urns)} must be supplied");
        }

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<ItSpendSchoolResponse>(builder, cancellationToken);
    }

    public async Task<IEnumerable<ItSpendTrustResponse>> GetTrustsAsync(string[] companyNumbers, CancellationToken cancellationToken = default)
    {
        if (companyNumbers.Length == 0)
        {
            throw new ArgumentNullException(nameof(companyNumbers), $"{nameof(companyNumbers)} must be supplied");
        }

        var builder = new ItSpendTrustCurrentPreviousYearQuery();

        builder.WhereCompanyNumberIn(companyNumbers);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<ItSpendTrustResponse>(builder, cancellationToken);
    }

    public async Task<ItSpendTrustForecastResponse> GetTrustForecastAsync(string? companyNumber, string? year, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(companyNumber))
        {
            throw new ArgumentNullException(nameof(companyNumber), $"{nameof(companyNumber)} must be supplied");
        }

        var builder = new ItSpendTrustCurrentAllYearsQuery();

        builder.WhereCompanyNumberEqual(companyNumber);

        using var conn = await dbFactory.GetConnection();
        var result = await conn.QueryAsync<ItSpendTrustForecastModel>(builder, cancellationToken);

        return BuildTrustForecastResponse(result);
    }

    private static ItSpendTrustForecastResponse BuildTrustForecastResponse(IEnumerable<ItSpendTrustForecastModel> result)
    {
        var items = result.ToList();
        var first = items.FirstOrDefault();

        return new ItSpendTrustForecastResponse
        {
            CompanyNumber = first?.CompanyNumber,
            TrustName = first?.TrustName,
            Years = items.Select(x => new ItSpendTrustForecastYear
            {
                Year = x.Year,
                Connectivity = x.Connectivity,
                OnsiteServers = x.OnsiteServers,
                ItLearningResources = x.ItLearningResources,
                AdministrationSoftwareAndSystems = x.AdministrationSoftwareAndSystems,
                LaptopsDesktopsAndTablets = x.LaptopsDesktopsAndTablets,
                OtherHardware = x.OtherHardware,
                ItSupport = x.ItSupport
            }).ToArray()
        };
    }
}