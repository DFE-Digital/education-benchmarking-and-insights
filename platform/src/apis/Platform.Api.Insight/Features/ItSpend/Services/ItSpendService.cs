using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.ItSpend.Responses;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.ItSpend.Services;

public interface IItSpendService
{
    Task<IEnumerable<ItSpendSchoolResponse>> GetSchoolsAsync(string[] urns, string dimension, CancellationToken cancellationToken = default);
    Task<IEnumerable<ItSpendTrustResponse>> GetTrustsAsync(string[] companyNumbers, CancellationToken cancellationToken = default);
    Task<IEnumerable<ItSpendTrustForecastResponse>> GetTrustForecastAsync(string companyNumber, CancellationToken cancellationToken = default);
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

    public async Task<IEnumerable<ItSpendTrustForecastResponse>> GetTrustForecastAsync(string companyNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(companyNumber))
        {
            throw new ArgumentNullException(nameof(companyNumber), $"{nameof(companyNumber)} must be supplied");
        }

        var builder = new ItSpendTrustCurrentAllYearsQuery();

        builder.WhereCompanyNumberEqual(companyNumber);

        using var conn = await dbFactory.GetConnection();

        return await conn.QueryAsync<ItSpendTrustForecastResponse>(builder, cancellationToken);
    }
}