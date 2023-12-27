using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Builders;

namespace EducationBenchmarking.Platform.Search.Trust;

public class TrustIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.Trust;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.Trust,
            targetIndexName: SearchResourceNames.Indexes.Trust)
        {
            Schedule = new IndexingSchedule(TimeSpan.FromDays(1)),
            FieldMappings =
            {
                new FieldMapping("CompanyNumber") { TargetFieldName = nameof(TrustIndex.CompanyNumber) },
                new FieldMapping("TrustOrCompanyName") { TargetFieldName = nameof(TrustIndex.Name)  }
            }
        };

        await client.CreateOrUpdateIndexerAsync(cosmosDbIndexer);
    }

    public override async Task Reset(SearchIndexerClient client)
    {
        await client.GetIndexerAsync(Name);
        await client.ResetIndexerAsync(Name);
    }
}