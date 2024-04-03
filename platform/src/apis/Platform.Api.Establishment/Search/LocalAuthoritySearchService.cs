using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Search;

namespace Platform.Api.Establishment.Search;

[ExcludeFromCodeCoverage]
public record LocalAuthoritySearchServiceOptions : SearchServiceOptions;

[ExcludeFromCodeCoverage]
public class LocalAuthoritySearchService : SearchService, ISearchService<LocalAuthorityResponseModel>
{
    private static readonly string[] Facets = { "" };
    private const string IndexName = "local-authority-index";

    public LocalAuthoritySearchService(IOptions<LocalAuthoritySearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
    }

    public async Task<SearchResponseModel<LocalAuthorityResponseModel>> SearchAsync(PostSearchRequestModel request)
    {
        return await SearchAsync<LocalAuthorityResponseModel>(request, CreateFilterExpression, Facets);
    }

    public Task<SuggestResponseModel<LocalAuthorityResponseModel>> SuggestAsync(PostSuggestRequestModel request)
    {
        throw new System.NotImplementedException();
    }

    private static string? CreateFilterExpression(FilterCriteriaRequestModel[] requestFilters)
    {
        return null;
    }
}