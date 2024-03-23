using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Search;

namespace Platform.Api.Establishment.Search;

[ExcludeFromCodeCoverage]
public record OrganisationSearchServiceOptions : SearchServiceOptions;

[ExcludeFromCodeCoverage]
public class OrganisationSearchService : SearchService, ISearchService<OrganisationResponseModel>
{
    private static readonly string[] Facets = Array.Empty<string>();
    private const string IndexName = "organisation-index";

    public OrganisationSearchService(IOptions<SchoolSearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
    }

    public Task<SearchResponseModel<OrganisationResponseModel>> SearchAsync(PostSearchRequestModel request)
    {
        return SearchAsync<OrganisationResponseModel>(request, facets: Facets);
    }

    public Task<SuggestResponseModel<OrganisationResponseModel>> SuggestAsync(PostSuggestRequestModel request)
    {
        var fields = new[]
        {
            nameof(OrganisationResponseModel.Identifier),
            nameof(OrganisationResponseModel.Name),
            nameof(OrganisationResponseModel.Kind),
            nameof(OrganisationResponseModel.Town),
            nameof(OrganisationResponseModel.Postcode)
        };

        return SuggestAsync<OrganisationResponseModel>(request, selectFields: fields);
    }
}