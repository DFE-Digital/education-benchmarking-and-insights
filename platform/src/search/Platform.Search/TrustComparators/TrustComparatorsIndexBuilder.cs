using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.TrustComparators;

public class TrustComparatorsIndexBuilder : IndexBuilder
{
    public override string Name => SearchResourceNames.Indexes.TrustComparators;

    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(TrustComparatorsIndex));
        var definition = new SearchIndex(Name, searchFields);

        await client.CreateOrUpdateIndexAsync(definition);
    }
}