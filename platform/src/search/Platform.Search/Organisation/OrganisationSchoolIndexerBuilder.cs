using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.Organisation;

public class OrganisationSchoolIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.OrganisationSchool;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.OrganisationSchool,
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