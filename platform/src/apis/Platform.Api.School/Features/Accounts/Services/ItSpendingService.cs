using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.School.Features.Accounts.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.School.Features.Accounts.Services;

public interface IItSpendingService
{
    Task<IEnumerable<ItSpendingResponse>> QueryAsync(string[] urns, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class ItSpendingService(IDatabaseFactory dbFactory) : IItSpendingService
{
    public async Task<IEnumerable<ItSpendingResponse>> QueryAsync(string[] urns, string dimension,
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
        return await conn.QueryAsync<ItSpendingResponse>(builder, cancellationToken);
    }
}