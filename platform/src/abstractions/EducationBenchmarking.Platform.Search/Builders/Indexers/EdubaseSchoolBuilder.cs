using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Search.Indexes;

namespace EducationBenchmarking.Platform.Search.Builders.Indexers;

public class EdubaseSchoolBuilder : IndexerBuilder
{
    public override string Name => Names.Indexers.CosmosSchoolEdubase;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: Names.DataSources.CosmosEbisEdubase,
            targetIndexName: Names.Indexes.School)
        {
            Schedule = new IndexingSchedule(TimeSpan.FromDays(1)),
            FieldMappings =
            {
                new FieldMapping("URN") { TargetFieldName = nameof(School.Urn) },
                new FieldMapping("EstablishmentName") { TargetFieldName = nameof(School.Name) },
                new FieldMapping("LAEstab") { TargetFieldName = nameof(School.LaEstab) }
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