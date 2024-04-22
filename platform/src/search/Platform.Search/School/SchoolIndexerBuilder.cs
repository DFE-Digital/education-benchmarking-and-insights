using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Domain;
using Platform.Search.Builders;

namespace Platform.Search.School;

public class SchoolIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.School;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.School,
            targetIndexName: SearchResourceNames.Indexes.School)
        {
            Schedule = new IndexingSchedule(TimeSpan.FromDays(1)),
            FieldMappings =
            {
                new FieldMapping("URN") { TargetFieldName = nameof(SchoolIndex.Urn) },
                new FieldMapping("EstablishmentName") { TargetFieldName = nameof(SchoolIndex.Name) },
                new FieldMapping("LAEstab") { TargetFieldName = nameof(SchoolIndex.LaEstab) },
                new FieldMapping("TypeOfEstablishment") { TargetFieldName = nameof(SchoolIndex.Kind) },
                new FieldMapping("FinanceType") { TargetFieldName = nameof(SchoolIndex.FinanceType) },
                new FieldMapping("Street") { TargetFieldName = nameof(SchoolIndex.Street) },
                new FieldMapping("Locality") { TargetFieldName = nameof(SchoolIndex.Locality) },
                new FieldMapping("Address3") { TargetFieldName = nameof(SchoolIndex.Address3) },
                new FieldMapping("Town") { TargetFieldName = nameof(SchoolIndex.Town) },
                new FieldMapping("County") { TargetFieldName = nameof(SchoolIndex.County) },
                new FieldMapping("Postcode") { TargetFieldName = nameof(SchoolIndex.Postcode) }
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