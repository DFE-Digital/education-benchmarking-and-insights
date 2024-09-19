using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.SchoolComparators;

public class SchoolComparatorsIndexerBuilder : IndexerBuilder
{
    public override string Name => ResourceNames.Search.Indexers.SchoolComparators;

    public override async Task Build(SearchIndexerClient client)
    {
        var indexer = new SearchIndexer(
            name: Name,
            dataSourceName: ResourceNames.Search.DataSources.SchoolComparators,
            targetIndexName: ResourceNames.Search.Indexes.SchoolComparators);

        await client.CreateOrUpdateIndexerAsync(indexer);
    }

    public override async Task Reset(SearchIndexerClient client)
    {
        await client.GetIndexerAsync(Name);
        await client.ResetIndexerAsync(Name);
    }
}