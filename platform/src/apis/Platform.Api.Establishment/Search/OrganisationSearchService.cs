using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain.Responses;
using Platform.Infrastructure.Search;

namespace Platform.Api.Establishment.Search;

[ExcludeFromCodeCoverage]
public record OrganisationSearchServiceOptions : SearchServiceOptions;

[ExcludeFromCodeCoverage]
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

    public Task<SuggestOutput<Organisation>> SuggestAsync(PostSuggestRequest request)
    {
        var fields = new[]
        {
            nameof(Organisation.Identifier),
            nameof(Organisation.Name),
            nameof(Organisation.Kind),
            nameof(Organisation.Town),
            nameof(Organisation.Postcode)
        };

        return SuggestAsync<Organisation>(request, selectFields: fields);
    }
}