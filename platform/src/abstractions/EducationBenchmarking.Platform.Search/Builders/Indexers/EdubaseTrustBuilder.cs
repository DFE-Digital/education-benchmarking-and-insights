using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Indexes;

namespace EducationBenchmarking.Platform.Search.Builders.Indexers;

public class EdubaseTrustBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.CosmosTrustEdubase;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.CosmosEbisEdubaseTrust,
            targetIndexName: SearchResourceNames.Indexes.Trust)
        {
            Schedule = new IndexingSchedule(TimeSpan.FromDays(1)),
            FieldMappings =
            {
                new FieldMapping("CompanyNumber") { TargetFieldName = nameof(Trust.CompanyNumber) },
                new FieldMapping("TrustOrCompanyName") { TargetFieldName = nameof(Trust.Name)  }
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