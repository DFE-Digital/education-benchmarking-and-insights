using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.NonFinancial.Shared;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    public static async Task<YearsModel?> QueryYearsLocalAuthorityAsync(
        this IDatabaseConnection conn,
        string code,
        CancellationToken cancellationToken = default)
    {
        var builder = new YearsLocalAuthorityQuery(code);
        return await conn.QueryFirstOrDefaultAsync<YearsModel>(builder, cancellationToken);
    }
}