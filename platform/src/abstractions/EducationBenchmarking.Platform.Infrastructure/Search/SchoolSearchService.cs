using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public class SchoolSearchService<T> : SearchService, ISearchService<T>
{
    private static readonly string[] Facets = { ""};
    
    public SchoolSearchService(IOptions<SchoolSearchServiceOptions> options) : base(options.Value.Endpoint, options.Value.SchoolIndexName, options.Value.Credential)
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