using System.Diagnostics.CodeAnalysis;
using System.Threading;
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
    Task<SuggestResponse<TrustSummary>> TrustsSuggestAsync(TrustSuggestRequest request, CancellationToken cancellationToken = default);
    Task<Trust?> GetAsync(string companyNumber, CancellationToken cancellationToken = default);
    Task<SearchResponse<TrustSummary>> TrustsSearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class TrustsService(
    [FromKeyedServices(ResourceNames.Search.Indexes.Trust)] IIndexClient client,
    IDatabaseFactory dbFactory)
    : SearchService<TrustSummary>(client), ITrustsService
{
    public async Task<Trust?> GetAsync(string companyNumber, CancellationToken cancellationToken = default)
    {
        var trustBuilder = new TrustQuery()
            .WhereCompanyNumberEqual(companyNumber);

        using var conn = await dbFactory.GetConnection();
        var trust = await conn.QueryFirstOrDefaultAsync<Trust>(trustBuilder, cancellationToken);
        if (trust is null)
        {
            return null;
        }

        var schoolsBuilder = new SchoolQuery(TrustSchool.Fields)
            .WhereTrustCompanyNumberEqual(companyNumber)
            .WhereFinanceTypeEqual(FinanceType.Academy)
            .WhereUrnInCurrentFinances();

        trust.Schools = await conn.QueryAsync<TrustSchool>(schoolsBuilder, cancellationToken);
        return trust;
    }

    public Task<SuggestResponse<TrustSummary>> TrustsSuggestAsync(TrustSuggestRequest request, CancellationToken cancellationToken = default)
    {
        var fields = new[] { nameof(Trust.CompanyNumber), nameof(Trust.TrustName) };

        return SuggestAsync(request, request.FilterExpression, fields, cancellationToken);
    }

    public Task<SearchResponse<TrustSummary>> TrustsSearchAsync(SearchRequest request, CancellationToken cancellationToken = default) => SearchAsync(request, request.FilterExpression, cancellationToken: cancellationToken);
}