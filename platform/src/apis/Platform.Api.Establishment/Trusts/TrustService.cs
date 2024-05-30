using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Platform.Infrastructure.Search;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Establishment.Trusts;

public interface ITrustService
{
    Task<SuggestResponse<Trust>> SuggestAsync(PostSuggestRequest request);
    Task<Trust?> GetAsync(string companyNumber);
}


[ExcludeFromCodeCoverage]
public class TrustService : SearchService, ITrustService
{
    private const string IndexName = SearchResourceNames.Indexes.Trust;
    private readonly IDatabaseFactory _dbFactory;

    public TrustService(IDatabaseFactory dbFactory, IOptions<SearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
        _dbFactory = dbFactory;
    }

    public async Task<Trust?> GetAsync(string companyNumber)
    {
        const string sql = "SELECT * from Trust where CompanyNumber = @CompanyNumber";
        var parameters = new { CompanyNumber = companyNumber };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<Trust>(sql, parameters);
    }

    public Task<SuggestResponse<Trust>> SuggestAsync(PostSuggestRequest request)
    {
        var fields = new[]
        {
            nameof(Trust.CompanyNumber),
            nameof(Trust.TrustName)
        };

        return SuggestAsync<Trust>(request, selectFields: fields);
    }
}