using System.Collections.Generic;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Establishment.Search;

public class LocalAuthoritySearchServiceOptions : SearchServiceOptions
{
    public string LocalAuthorityIndexName { get; set; }
}

public class LocalAuthoritySearchService : SearchService, ISearchService<LocalAuthority>
{
    private static readonly string[] Facets = { ""};
    
    public LocalAuthoritySearchService(IOptions<LocalAuthoritySearchServiceOptions> options) : base(options.Value.Endpoint, options.Value.LocalAuthorityIndexName, options.Value.Credential)
    {
    }

    public async Task<SearchOutput<LocalAuthority>> SearchAsync(PostSearchRequest request)
    {
        return await SearchAsync<LocalAuthority>(request, CreateFilterExpression, Facets);
    }

    private string CreateFilterExpression(List<SearchFilters> requestFilters)
    {
        return "";
    }
}