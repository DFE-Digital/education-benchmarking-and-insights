using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.TrustComparators;

public class TrustComparatorsIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.TrustComparators;

    public override async Task Build(SearchIndexerClient client)
    {
        var indexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.TrustComparators,
            targetIndexName: SearchResourceNames.Indexes.TrustComparators);

        await client.CreateOrUpdateIndexerAsync(indexer);
    }

    public override async Task Reset(SearchIndexerClient client)
    {
        await client.GetIndexerAsync(Name);
        await client.ResetIndexerAsync(Name);
    }
}