using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.SchoolComparators;

public class SchoolComparatorsIndexBuilder : IndexBuilder
{
    public override string Name => SearchResourceNames.Indexes.SchoolComparators;

    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(SchoolComparatorsIndex));
        var definition = new SearchIndex(Name, searchFields);
        await client.CreateOrUpdateIndexAsync(definition);
    }
}