using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.Organisation;

public class OrganisationTrustIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.OrganisationTrust;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.OrganisationTrust,
            targetIndexName: SearchResourceNames.Indexes.Organisation)
        {
            Schedule = new IndexingSchedule(TimeSpan.FromDays(1))
        };

        await client.CreateOrUpdateIndexerAsync(cosmosDbIndexer);
    }

    public override async Task Reset(SearchIndexerClient client)
    {
        await client.GetIndexerAsync(Name);

        // Reset the indexer if it exists.
        await client.ResetIndexerAsync(Name);
    }
}