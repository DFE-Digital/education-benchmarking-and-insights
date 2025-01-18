using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Requests;
using Platform.Domain;
using Platform.Infrastructure;
using Platform.Search;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Establishment.Features.Trusts.Services;

public interface ITrustsService
{
    Task<SuggestResponse<Trust>> SuggestAsync(TrustSuggestRequest request);
    Task<Trust?> GetAsync(string companyNumber);
}


[ExcludeFromCodeCoverage]
public class TrustsService(
    [FromKeyedServices(ResourceNames.Search.Indexes.Trust)] IIndexClient client,
    IDatabaseFactory dbFactory)
    : SearchService<Trust>(client), ITrustsService
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

    public Task<SuggestResponse<Trust>> SuggestAsync(TrustSuggestRequest request)
    {
        var fields = new[]
        {
            nameof(Trust.CompanyNumber),
            nameof(Trust.TrustName)
        };

        return SuggestAsync(request, CreateFilterExpression, fields);

        string? CreateFilterExpression()
        {
            return request.Exclude is not { Length: > 0 }
                ? null
                : $"({string.Join(") and ( ", request.Exclude.Select(a => $"{nameof(Trust.CompanyNumber)} ne '{a}'"))})";
        }
    }
}