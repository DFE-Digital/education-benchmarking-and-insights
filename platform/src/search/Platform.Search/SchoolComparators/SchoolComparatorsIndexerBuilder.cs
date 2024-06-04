using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.SchoolComparators;

public class SchoolComparatorsIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.SchoolComparators;

    public override async Task Build(SearchIndexerClient client)
    {
        var indexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.School,
            targetIndexName: SearchResourceNames.Indexes.School);

        await client.CreateOrUpdateIndexerAsync(indexer);
    }

    public override async Task Reset(SearchIndexerClient client)
    {
        await client.GetIndexerAsync(Name);
        await client.ResetIndexerAsync(Name);
    }
}