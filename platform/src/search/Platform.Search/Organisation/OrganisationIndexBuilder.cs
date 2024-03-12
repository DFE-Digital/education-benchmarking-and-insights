using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.Organisation;

public class OrganisationIndexBuilder : IndexBuilder
{
    public override string Name => SearchResourceNames.Indexes.Organisation;

    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(OrganisationIndex));

        var definition = new SearchIndex(Name, searchFields);
        var suggestFields = new[]
        {
            nameof(OrganisationIndex.Name),
            nameof(OrganisationIndex.Identifier),
            nameof(OrganisationIndex.Street),
            nameof(OrganisationIndex.Locality),
            nameof(OrganisationIndex.Address3),
            nameof(OrganisationIndex.Town),
            nameof(OrganisationIndex.County),
            nameof(OrganisationIndex.Postcode)
        };
        var suggester = new SearchSuggester(SearchResourceNames.Suggesters.Organisation, suggestFields);

        definition.Suggesters.Add(suggester);

        await client.CreateOrUpdateIndexAsync(definition);
    }
}