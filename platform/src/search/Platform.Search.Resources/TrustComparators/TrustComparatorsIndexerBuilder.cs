using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.TrustComparators;

public class TrustComparatorsIndexerBuilder : IndexerBuilder
{
    public override string Name => ResourceNames.Search.Indexers.TrustComparators;

    public override async Task Build(SearchIndexerClient client)
    {
        var indexer = new SearchIndexer(
            Name,
            ResourceNames.Search.DataSources.TrustComparators,
            ResourceNames.Search.Indexes.TrustComparators);

        await client.CreateOrUpdateIndexerAsync(indexer);
    }

    public override async Task Reset(SearchIndexerClient client)
    {
        await client.GetIndexerAsync(Name);
        await client.ResetIndexerAsync(Name);
    }
}