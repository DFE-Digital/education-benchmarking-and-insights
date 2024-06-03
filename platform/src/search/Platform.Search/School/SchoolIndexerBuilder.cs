using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
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
            FieldMappings =
            {
                new FieldMapping("URN") { TargetFieldName = nameof(SchoolIndex.URN) },
                new FieldMapping("SchoolName") { TargetFieldName = nameof(SchoolIndex.SchoolName) },
                new FieldMapping("AddressStreet") { TargetFieldName = nameof(SchoolIndex.AddressStreet) },
                new FieldMapping("AddressLocality") { TargetFieldName = nameof(SchoolIndex.AddressLocality) },
                new FieldMapping("AddressLine3") { TargetFieldName = nameof(SchoolIndex.AddressLine3) },
                new FieldMapping("AddressTown") { TargetFieldName = nameof(SchoolIndex.AddressTown) },
                new FieldMapping("AddressCounty") { TargetFieldName = nameof(SchoolIndex.AddressCounty) },
                new FieldMapping("AddressPostcode") { TargetFieldName = nameof(SchoolIndex.AddressPostcode) }
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