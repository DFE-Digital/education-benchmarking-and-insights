using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.SchoolComparators;

public class SchoolComparatorsIndexBuilder : IndexBuilder
{
    public override string Name => ResourceNames.Search.Indexes.SchoolComparators;

    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(SchoolComparatorsIndex));
        var definition = new SearchIndex(Name, searchFields);

        await client.CreateOrUpdateIndexAsync(definition);
    }
}