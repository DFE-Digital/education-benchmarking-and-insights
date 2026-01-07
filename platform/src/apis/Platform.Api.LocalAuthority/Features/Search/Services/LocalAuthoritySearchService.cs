using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthority.Features.Search.Models;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.LocalAuthority.Features.Search.Services;

public interface ILocalAuthoritySearchService
{
    Task<SuggestResponse<LocalAuthoritySummaryResponse>> SuggestAsync(LocalAuthoritySuggestRequest request, CancellationToken cancellationToken = default);
    Task<SearchResponse<LocalAuthoritySummaryResponse>> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
}

public class LocalAuthoritySearchService(
    [FromKeyedServices(ResourceNames.Search.Indexes.LocalAuthority)] IIndexClient client) : SearchService<LocalAuthoritySummaryResponse>(client), ILocalAuthoritySearchService
{
    public Task<SuggestResponse<LocalAuthoritySummaryResponse>> SuggestAsync(LocalAuthoritySuggestRequest request, CancellationToken cancellationToken = default)
    {
        var fields = new[]
        {
            nameof(LocalAuthoritySummaryResponse.Code),
            nameof(LocalAuthoritySummaryResponse.Name)
        };

        return SuggestAsync(request, CreateFilterExpression, fields, cancellationToken);

        string? CreateFilterExpression()
        {
            return request.Exclude is not { Length: > 0 }
                ? null
                : $"({string.Join(") and ( ", request.Exclude.Select(a => $"{nameof(LocalAuthoritySummaryResponse.Name)} ne '{a}'"))})";
        }
    }

    public Task<SearchResponse<LocalAuthoritySummaryResponse>> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default) => SearchAsync(request, request.FilterExpression, cancellationToken: cancellationToken);
}