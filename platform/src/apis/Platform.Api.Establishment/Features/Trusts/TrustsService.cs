using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Platform.Domain;
using Platform.Search;
using Platform.Search.Requests;
using Platform.Search.Responses;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Establishment.Features.Trusts;

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
        var trustBuilder = new TrustQuery()
            .WhereCompanyNumberEqual(companyNumber);

        using var conn = await dbFactory.GetConnection();
        var trust = await conn.QueryFirstOrDefaultAsync<Trust>(trustBuilder);
        if (trust is null)
        {
            return null;
        }

        var schoolsBuilder = new SchoolQuery(TrustSchool.Fields)
            .WhereTrustCompanyNumberEqual(companyNumber)
            .WhereFinanceTypeEqual(FinanceType.Academy)
            .WhereUrnInCurrentFinances();

        trust.Schools = await conn.QueryAsync<TrustSchool>(schoolsBuilder);
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