using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace EducationBenchmarking.Platform.Search.Builders.Indexers;

public class EdubaseEstablishmentSchoolBuilder : IndexerBuilder
{
    public override string Name => Names.Indexers.CosmosEstablishmentSchoolEdubase;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: Names.DataSources.CosmosEbisEdubaseSchool,
            targetIndexName: Names.Indexes.Establishment)
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