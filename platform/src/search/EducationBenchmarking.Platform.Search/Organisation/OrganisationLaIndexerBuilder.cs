using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Builders;

namespace EducationBenchmarking.Platform.Search.Organisation;

public class OrganisationLaIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.OrganisationLa;

    public override async Task Build(SearchIndexerClient client)
    {
        var indexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.OrganisationLa,
            targetIndexName: SearchResourceNames.Indexes.Organisation)
        {
            Schedule = new IndexingSchedule(TimeSpan.FromDays(1)),
            Parameters = new IndexingParameters { Configuration = { new KeyValuePair<string, object>("parsingMode", "jsonArray") } }
        };

        await client.CreateOrUpdateIndexerAsync(indexer);
    }

    public override async Task Reset(SearchIndexerClient client)
    {
        await client.GetIndexerAsync(Name);

        // Reset the indexer if it exists.
        await client.ResetIndexerAsync(Name);
    }
}