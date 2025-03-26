using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.School;

public class SchoolFacetedIndexBuilder : IndexBuilder
{
    public override string Name => ResourceNames.Search.Indexes.SchoolFaceted;

    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(SchoolFacetedIndex));
        var definition = new SearchIndex(Name, searchFields);

        await client.CreateOrUpdateIndexAsync(definition);
    }
}