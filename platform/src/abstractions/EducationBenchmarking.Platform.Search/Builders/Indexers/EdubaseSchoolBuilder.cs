using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Indexes;

namespace EducationBenchmarking.Platform.Search.Builders.Indexers;

public class EdubaseSchoolBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.CosmosSchoolEdubase;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.CosmosEbisEdubase,
            targetIndexName: SearchResourceNames.Indexes.School)
        {
            Schedule = new IndexingSchedule(TimeSpan.FromDays(1)),
            FieldMappings =
            {
                new FieldMapping("URN") { TargetFieldName = nameof(School.Urn) },
                new FieldMapping("EstablishmentName") { TargetFieldName = nameof(School.Name) },
                new FieldMapping("LAEstab") { TargetFieldName = nameof(School.LaEstab) },
                new FieldMapping("TypeOfEstablishment") { TargetFieldName = nameof(School.Kind) },
                new FieldMapping("FinanceType") { TargetFieldName = nameof(School.FinanceType) },
                new FieldMapping("Street") { TargetFieldName = nameof(School.Street) },
                new FieldMapping("Locality") { TargetFieldName = nameof(School.Locality) },
                new FieldMapping("Address3") { TargetFieldName = nameof(School.Address3) },
                new FieldMapping("Town") { TargetFieldName = nameof(School.Town) },
                new FieldMapping("County") { TargetFieldName = nameof(School.County) },
                new FieldMapping("Postcode") { TargetFieldName = nameof(School.Postcode) }
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