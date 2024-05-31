using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.School;

public class SchoolIndexBuilder : IndexBuilder
{
    public override string Name => SearchResourceNames.Indexes.School;

    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(SchoolIndex));
        var definition = new SearchIndex(Name, searchFields);
        var suggestFields = new[]
        {
            nameof(SchoolIndex.SchoolName),
            nameof(SchoolIndex.URN),
            nameof(SchoolIndex.AddressStreet),
            nameof(SchoolIndex.AddressLocality),
            nameof(SchoolIndex.AddressLine3),
            nameof(SchoolIndex.AddressTown),
            nameof(SchoolIndex.AddressCounty),
            nameof(SchoolIndex.AddressPostcode)
        };

        var suggester = new SearchSuggester(SearchResourceNames.Suggesters.School, suggestFields);
        definition.Suggesters.Add(suggester);
        await client.CreateOrUpdateIndexAsync(definition);
    }
}