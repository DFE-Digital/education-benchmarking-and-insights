using System;
using System.Threading;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Establishment.Search;

public class OrganisationSearchServiceOptions : SearchServiceOptions
{
}

public class OrganisationSearchService : SearchService, ISearchService<Organisation>
{
    private static readonly string[] Facets = Array.Empty<string>();
    private const string IndexName = "organisation-index";
    
    public OrganisationSearchService(IOptions<SchoolSearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
    }

    public Task<SearchOutput<Organisation>> SearchAsync(PostSearchRequest request)
    {
        return SearchAsync<Organisation>(request, facets: Facets);
    }
    
    public Task<SuggestOutput<Organisation>> SuggestAsync(PostSuggestRequest request, CancellationToken cancellationToken)
    {
        var fields = new[]
        {
            nameof(Organisation.Identifier), 
            nameof(Organisation.Name),
            nameof(Organisation.Kind),
            nameof(Organisation.Town),
            nameof(Organisation.Postcode)
        };
        
        return SuggestAsync<Organisation>(request, cancellationToken, selectFields: fields);
    }
}