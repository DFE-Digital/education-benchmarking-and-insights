using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Platform.Infrastructure.Search;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Establishment.Trusts;

public interface ITrustsService
{
    Task<SuggestResponse<Trust>> SuggestAsync(SuggestRequest request, string[]? excludeTrusts = null);
    Task<Trust?> GetAsync(string companyNumber);
}


[ExcludeFromCodeCoverage]
public class TrustsService : SearchService, ITrustsService
{
    private const string IndexName = SearchResourceNames.Indexes.Trust;
    private readonly IDatabaseFactory _dbFactory;

    public TrustsService(IDatabaseFactory dbFactory, IOptions<SearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
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

    public Task<SuggestResponse<Trust>> SuggestAsync(SuggestRequest request, string[]? excludeTrusts = null)
    {
        var fields = new[]
        {
            nameof(Trust.CompanyNumber),
            nameof(Trust.TrustName)
        };

        return SuggestAsync<Trust>(request, CreateFilterExpression, selectFields: fields);

        string? CreateFilterExpression()
        {
            if (excludeTrusts is not { Length: > 0 })
            {
                return null;
            }

            var filterExpressions = new List<string>();
            if (excludeTrusts.Length > 0)
            {
                filterExpressions.Add($"({string.Join(") and ( ", excludeTrusts.Select(a => $"{nameof(Trust.CompanyNumber)} ne '{a}'"))})");
            }

            return string.Join(" and ", filterExpressions);
        }
    }
}