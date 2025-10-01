using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
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

        var response = companyNumbers.Select(companyNumber => new ItSpendTrustResponse
        {
            CompanyNumber = companyNumber,
            TrustName = "Stub Trust",
            Connectivity = 2000,
            ItLearningResources = 21000,
            ItSupport = 12000,
            AdministrationSoftwareAndSystems = 36000,
            LaptopsDesktopsAndTablets = 11000,
            OnsiteServers = 36000,
            OtherHardware = 33000
        });

        return response;
    }

    public async Task<ItSpendTrustForecastResponse> GetTrustForecastAsync(string? companyNumber, string? year, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(companyNumber))
        {
            throw new ArgumentNullException(nameof(companyNumber), $"{nameof(companyNumber)} must be supplied");
        }

        if (string.IsNullOrWhiteSpace(year))
        {
            throw new ArgumentNullException(nameof(year), $"{nameof(year)} must be supplied");
        }

        var parsedYear = int.Parse(year);
        int[] years = [parsedYear - 1, parsedYear, parsedYear + 1];

        var response = new ItSpendTrustForecastResponse
        {
            CompanyNumber = companyNumber,
            TrustName = "Stub Trust",
            Years = years.Select(y => new ItSpendTrustForecastYear
            {
                Year = y,
                Connectivity = 2000,
                ItLearningResources = 21000,
                ItSupport = 12000,
                AdministrationSoftwareAndSystems = 36000,
                LaptopsDesktopsAndTablets = 11000,
                OnsiteServers = 36000,
                OtherHardware = 33000
            }).ToArray(),
        };

        return response;
    }
}