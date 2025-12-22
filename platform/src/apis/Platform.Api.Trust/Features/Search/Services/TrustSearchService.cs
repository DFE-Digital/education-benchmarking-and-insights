using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Trust.Features.Search.Models;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.Trust.Features.Search.Services;

public interface ITrustSearchService
{
    Task<SuggestResponse<TrustSummaryResponse>> SuggestAsync(TrustSuggestRequest request, CancellationToken cancellationToken = default);
    Task<SearchResponse<TrustSummaryResponse>> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class TrustSearchService(
    [FromKeyedServices(ResourceNames.Search.Indexes.Trust)] IIndexClient client) : SearchService<TrustSummaryResponse>(client), ITrustSearchService
{
    public Task<SuggestResponse<TrustSummaryResponse>> SuggestAsync(TrustSuggestRequest request, CancellationToken cancellationToken = default)
    {
        var fields = new[]
        {
            nameof(TrustSummaryResponse.CompanyNumber),
            nameof(TrustSummaryResponse.TrustName)
        };

        return SuggestAsync(request, request.FilterExpression, fields, cancellationToken);
    }

    public Task<SearchResponse<TrustSummaryResponse>> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default) => SearchAsync(request, request.FilterExpression, cancellationToken: cancellationToken);
}