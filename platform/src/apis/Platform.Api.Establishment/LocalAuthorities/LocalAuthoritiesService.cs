using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Platform.Infrastructure.Search;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Establishment.LocalAuthorities;

public interface ILocalAuthoritiesService
{
    Task<SuggestResponse<LocalAuthority>> SuggestAsync(SuggestRequest request);
    Task<LocalAuthority?> GetAsync(string code);
}


[ExcludeFromCodeCoverage]
public class LocalAuthoritiesService : SearchService, ILocalAuthoritiesService
{
    private const string IndexName = SearchResourceNames.Indexes.LocalAuthority;
    private readonly IDatabaseFactory _dbFactory;

    public LocalAuthoritiesService(IDatabaseFactory dbFactory, IOptions<SearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
        _dbFactory = dbFactory;
    }

    public async Task<LocalAuthority?> GetAsync(string code)
    {
        const string sql = "SELECT * from LocalAuthority where Code = @Code";
        var parameters = new { Code = code };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<LocalAuthority>(sql, parameters);
    }

    public Task<SuggestResponse<LocalAuthority>> SuggestAsync(SuggestRequest request)
    {
        var fields = new[]
        {
            nameof(LocalAuthority.Code),
            nameof(LocalAuthority.Name)
        };

        return SuggestAsync<LocalAuthority>(request, selectFields: fields);
    }
}