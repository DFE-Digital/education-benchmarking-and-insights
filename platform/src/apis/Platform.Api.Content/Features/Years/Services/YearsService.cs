using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Platform.Api.Content.Features.Years.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Content.Features.Years.Services;

public interface IYearsService
{
    Task<FinanceYears> GetCurrentReturnYears(CancellationToken cancellationToken = default);
}

public class YearsService(IDatabaseFactory dbFactory) : IYearsService
{
    public async Task<FinanceYears> GetCurrentReturnYears(CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var aarBuilder = new ParametersQuery("LatestAARYear");
        var cfrBuilder = new ParametersQuery("LatestCFRYear");
        var s251Builder = new ParametersQuery("LatestS251Year");

        var aar = await conn.QueryFirstOrDefaultAsync<string>(aarBuilder, cancellationToken);
        var cfr = await conn.QueryFirstOrDefaultAsync<string>(cfrBuilder, cancellationToken);
        var s251 = await conn.QueryFirstOrDefaultAsync<string>(s251Builder, cancellationToken);
        return new FinanceYears
        {
            Aar = aar,
            Cfr = cfr,
            S251 = s251
        };
    }
}