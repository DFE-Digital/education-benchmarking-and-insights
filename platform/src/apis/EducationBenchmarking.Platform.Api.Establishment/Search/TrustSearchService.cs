using System.Collections.Generic;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Establishment.Search;

public class TrustSearchServiceOptions : SearchServiceOptions
{
    public string TrustIndexName { get; set; }
}

public class TrustSearchService : SearchService, ISearchService<Trust>
{
    private static readonly string[] Facets = { "" };
    
    public TrustSearchService(IOptions<TrustSearchServiceOptions> options) : base(options.Value.Endpoint, options.Value.TrustIndexName, options.Value.Credential)
    {
    }

    public async Task<SearchOutput<Trust>> SearchAsync(PostSearchRequest request)
    {
        return await SearchAsync<Trust>(request, CreateFilterExpression, Facets);
    }

    private string CreateFilterExpression(List<SearchFilters> requestFilters)
    {
        return "";
    }
}