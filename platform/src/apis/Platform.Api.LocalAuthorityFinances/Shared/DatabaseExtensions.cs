using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Insight.Shared;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.LocalAuthorityFinances.Shared;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    public static async Task<YearsModel?> QueryYearsLocalAuthorityAsync(
        this IDatabaseConnection conn,
        string code,
        CancellationToken token = default)
    {
        var builder = new YearsLocalAuthorityQuery(code);
        return await conn.QueryFirstOrDefaultAsync<YearsModel>(builder, token);
    }
}