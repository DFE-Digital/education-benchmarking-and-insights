using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Establishment.Features.LocalAuthorities.Services;

public interface ILocalAuthorityRankingService
{
    Task<LocalAuthorityRanking> GetRanking(string ranking, string sort, CancellationToken cancellationToken = default);
}

public class LocalAuthorityRankingService(IDatabaseFactory dbFactory) : ILocalAuthorityRankingService
{
    public async Task<LocalAuthorityRanking> GetRanking(string ranking, string sort, CancellationToken cancellationToken = default)
    {
        var laBuilder = new LocalAuthorityFinancialDefaultCurrentRankingQuery(ranking, sort)
            .WhereValueIsNotNull();

        using var conn = await dbFactory.GetConnection();
        var results = await conn.QueryAsync<LocalAuthorityRank>(laBuilder, cancellationToken);
        return new LocalAuthorityRanking
        {
            Ranking = results.ToArray()
        };
    }
}