using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Builders;

namespace EducationBenchmarking.Platform.Search.Establishment;

public class EstablishmentTrustIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.EstablishmentTrust;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.EstablishmentTrust,
            targetIndexName: SearchResourceNames.Indexes.Establishment)
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