using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public class TrustSearchService<T> : SearchService, ISearchService<T>
{
    private static readonly string[] Facets = { "" };
    
    public TrustSearchService(IOptions<TrustSearchServiceOptions> options) : base(options.Value.Endpoint, options.Value.TrustIndexName, options.Value.Credential)
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