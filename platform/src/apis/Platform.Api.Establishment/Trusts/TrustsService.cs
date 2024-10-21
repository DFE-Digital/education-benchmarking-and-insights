using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Platform.Search;
using Platform.Sql;

namespace Platform.Api.Establishment.Trusts;

public interface ITrustsService
{
    Task<SuggestResponse<Trust>> SuggestAsync(SuggestRequest request, string[]? excludeTrusts = null);
    Task<Trust?> GetAsync(string companyNumber);
}

[ExcludeFromCodeCoverage]
public class TrustsService(ISearchConnection<Trust> searchConnection, IDatabaseFactory dbFactory) : ITrustsService
{
    public async Task<Trust?> GetAsync(string companyNumber)
    {
        var template = Queries.GetTrust(companyNumber);
        using var conn = await dbFactory.GetConnection();
        var trust = await conn.QueryFirstOrDefaultAsync<Trust>(template);
        if (trust is null)
        {
            return null;
        }
        
        var schoolsTemplate = Queries.GetTrustSchools(companyNumber);
        trust.Schools = await conn.QueryAsync<TrustSchool>(schoolsTemplate);
        return trust;
    }

    public Task<SuggestResponse<Trust>> SuggestAsync(SuggestRequest request, string[]? excludeTrusts = null)
    {
        var fields = new[]
        {
            nameof(Trust.CompanyNumber),
            nameof(Trust.TrustName)
        };

        return searchConnection.SuggestAsync(request, CreateFilterExpression, fields);

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