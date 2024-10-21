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
        var template = Queries.GetLocalAuthority(code);
        using var conn = await dbFactory.GetConnection();
        var localAuthority = await conn.QueryFirstOrDefaultAsync<LocalAuthority>(template);
        if (localAuthority is null)
        {
            return null;
        }
        
        var schoolsTemplate = Queries.GetLocalAuthoritySchools(code);
        localAuthority.Schools = await conn.QueryAsync<LocalAuthoritySchool>(schoolsTemplate);
        return localAuthority;
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