using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using Platform.Api.Establishment.Search;
using Platform.Domain;

namespace Platform.Api.Establishment.Features.Trusts;

[ExcludeFromCodeCoverage]
public class TrustsSearchService(IOptions<SearchServiceOptions> options)
    : SearchService(options.Value, IndexName), ISearchService<TrustResponseModel>
{
    private static readonly string[] Facets = { "" };
    private const string IndexName = SearchResourceNames.Indexes.Trust;

    public Task<SearchResponseModel<TrustResponseModel>> SearchAsync(PostSearchRequestModel request)
    {
        return SearchAsync<TrustResponseModel>(request, CreateFilterExpression, Facets);
    }

    public Task<SuggestResponseModel<TrustResponseModel>> SuggestAsync(PostSuggestRequestModel request)
    {
        var fields = new[]
        {
            nameof(TrustResponseModel.CompanyNumber),
            nameof(TrustResponseModel.Name)
        };

        return SuggestAsync<TrustResponseModel>(request, selectFields: fields);
    }

    private static string? CreateFilterExpression(FilterCriteriaRequestModel[] requestFilters)
    {
        return null;
    }
}