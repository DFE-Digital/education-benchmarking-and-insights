using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Services;

public interface IStatisticalNeighboursService
{
    Task<IEnumerable<StatisticalNeighbourDto>> GetAsync(string identifier, CancellationToken cancellationToken = default);
}

public class StatisticalNeighboursService(IDatabaseFactory dbFactory) : IStatisticalNeighboursService
{
    public async Task<IEnumerable<StatisticalNeighbourDto>> GetAsync(string code, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var laBuilder = new LocalAuthorityStatisticalNeighbourQuery()
            .WhereLaCodeEqual(code);

        return await conn.QueryAsync<StatisticalNeighbourDto>(laBuilder, cancellationToken);
    }
}