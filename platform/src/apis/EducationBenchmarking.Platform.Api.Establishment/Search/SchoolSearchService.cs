using System.Collections.Generic;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Establishment.Search;

public class SchoolSearchServiceOptions : SearchServiceOptions
{
    public string SchoolIndexName { get; set; }
}

public class SchoolSearchService : SearchService, ISearchService<School>
{
    private static readonly string[] Facets = { ""};
    
    public SchoolSearchService(IOptions<SchoolSearchServiceOptions> options) : base(options.Value.Endpoint, options.Value.SchoolIndexName, options.Value.Credential)
    {
    }

    public async Task<SearchOutput<School>> SearchAsync(PostSearchRequest request)
    {
        return await SearchAsync<School>(request, CreateFilterExpression, Facets);
    }

    private string CreateFilterExpression(List<SearchFilters> requestFilters)
    {
        return "";
    }
}