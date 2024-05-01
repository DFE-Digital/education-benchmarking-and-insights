using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Search;

namespace Platform.Api.Establishment.Search;

[ExcludeFromCodeCoverage]
public class TrustSearchService : SearchService, ISearchService<TrustResponseModel>
{
    private static readonly string[] Facets = { "" };
    private const string IndexName = SearchResourceNames.Indexes.Trust;
    public TrustSearchService(IOptions<SearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
    }

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