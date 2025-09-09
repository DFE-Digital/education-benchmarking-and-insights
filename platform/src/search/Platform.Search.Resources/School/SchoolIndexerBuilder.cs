using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.School;

public class SchoolIndexerBuilder : IndexerBuilder
{
    public override string Name => ResourceNames.Search.Indexers.School;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            Name,
            ResourceNames.Search.DataSources.School,
            ResourceNames.Search.Indexes.School)
        {
            FieldMappings =
            {
                new FieldMapping("URN")
                {
                    TargetFieldName = nameof(SchoolIndex.URN)
                },
                new FieldMapping("SchoolName")
                {
                    TargetFieldName = nameof(SchoolIndex.SchoolName)
                },
                new FieldMapping("AddressStreet")
                {
                    TargetFieldName = nameof(SchoolIndex.AddressStreet)
                },
                new FieldMapping("AddressLocality")
                {
                    TargetFieldName = nameof(SchoolIndex.AddressLocality)
                },
                new FieldMapping("AddressLine3")
                {
                    TargetFieldName = nameof(SchoolIndex.AddressLine3)
                },
                new FieldMapping("AddressTown")
                {
                    TargetFieldName = nameof(SchoolIndex.AddressTown)
                },
                new FieldMapping("AddressCounty")
                {
                    TargetFieldName = nameof(SchoolIndex.AddressCounty)
                },
                new FieldMapping("AddressPostcode")
                {
                    TargetFieldName = nameof(SchoolIndex.AddressPostcode)
                },
                new FieldMapping("SchoolNameSortable")
                {
                    TargetFieldName = nameof(SchoolIndex.SchoolNameSortable)
                }
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