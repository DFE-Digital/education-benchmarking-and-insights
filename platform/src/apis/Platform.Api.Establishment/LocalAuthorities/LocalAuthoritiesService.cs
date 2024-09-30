using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Platform.Api.Establishment.Schools;
using Platform.Search;
using Platform.Sql;
namespace Platform.Api.Establishment.LocalAuthorities;

public interface ILocalAuthoritiesService
{
    Task<SuggestResponse<LocalAuthority>> SuggestAsync(SuggestRequest request, string[]? excludeLas = null);
    Task<LocalAuthorityResponse?> GetAsync(string code);
}

[ExcludeFromCodeCoverage]
public class LocalAuthoritiesService(ISearchConnection<LocalAuthority> searchConnection, IDatabaseFactory dbFactory) : ILocalAuthoritiesService
{
    public async Task<LocalAuthorityResponse?> GetAsync(string code)
    {
        const string trustSql = "SELECT * from LocalAuthority where Code = @Code";
        var parameters = new
        {
            Code = code
        };

        using var conn = await dbFactory.GetConnection();
        var localAuthority = await conn.QueryFirstOrDefaultAsync<LocalAuthority>(trustSql, parameters);
        if (localAuthority is null)
        {
            return null;
        }

        const string schoolsSql = "SELECT URN, SchoolName, OverallPhase from School where LaCode = @Code AND FinanceType = 'Maintained'";
        var schools = await conn.QueryAsync<School>(schoolsSql, parameters);
        return LocalAuthorityResponseFactory.Create(localAuthority, schools);
    }

    public Task<SuggestResponse<LocalAuthority>> SuggestAsync(SuggestRequest request, string[]? excludeLas = null)
    {
        var fields = new[]
        {
            nameof(LocalAuthority.Code),
            nameof(LocalAuthority.Name)
        };

        return searchConnection.SuggestAsync(request, CreateFilterExpression, fields);

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