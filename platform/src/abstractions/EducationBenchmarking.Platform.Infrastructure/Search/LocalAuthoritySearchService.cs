using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public class LocalAuthoritySearchService<T> : SearchService, ISearchService<T>
{
    private static readonly string[] Facets = { ""};
    
    public LocalAuthoritySearchService(IOptions<LocalAuthoritySearchServiceOptions> options) : base(options.Value.Endpoint, options.Value.LocalAuthorityIndexName, options.Value.Credential)
    {
    }

    public async Task<SearchOutput<T>> SearchAsync(PostSearchRequest request)
    {
        return await SearchAsync<T>(request, CreateFilterExpression, Facets);
    }

    private string CreateFilterExpression(List<SearchFilters> requestFilters)
    {
        return "";
    }
}