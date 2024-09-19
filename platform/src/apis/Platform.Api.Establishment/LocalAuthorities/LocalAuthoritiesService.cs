using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Platform.Search;
using Platform.Sql;

namespace Platform.Api.Establishment.LocalAuthorities;

public interface ILocalAuthoritiesService
{
    Task<SuggestResponse<LocalAuthority>> SuggestAsync(SuggestRequest request, string[]? excludeLas = null);
    Task<LocalAuthority?> GetAsync(string code);
}


[ExcludeFromCodeCoverage]
public class LocalAuthoritiesService(ISearchConnection<LocalAuthority> searchConnection, IDatabaseFactory dbFactory) : ILocalAuthoritiesService
{

    public async Task<LocalAuthority?> GetAsync(string code)
    {
        const string sql = "SELECT * from LocalAuthority where Code = @Code";
        var parameters = new { Code = code };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<LocalAuthority>(sql, parameters);
    }

    public Task<SuggestResponse<LocalAuthority>> SuggestAsync(SuggestRequest request, string[]? excludeLas = null)
    {
        var fields = new[]
        {
            nameof(LocalAuthority.Code),
            nameof(LocalAuthority.Name)
        };

        return searchConnection.SuggestAsync(request, CreateFilterExpression, selectFields: fields);

        string? CreateFilterExpression()
        {
            if (excludeLas is not { Length: > 0 })
            {
                return null;
            }

            var filterExpressions = new List<string>();
            if (excludeLas.Length > 0)
            {
                filterExpressions.Add($"({string.Join(") and ( ", excludeLas.Select(a => $"{nameof(LocalAuthority.Name)} ne '{a}'"))})");
            }

            return string.Join(" and ", filterExpressions);
        }
    }
}