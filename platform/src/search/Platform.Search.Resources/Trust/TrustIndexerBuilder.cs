using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.Trust;

public class TrustIndexerBuilder : IndexerBuilder
{
    public override string Name => ResourceNames.Search.Indexers.Trust;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: ResourceNames.Search.DataSources.Trust,
            targetIndexName: ResourceNames.Search.Indexes.Trust)
        {
            FieldMappings =
            {
                new FieldMapping("CompanyNumber") { TargetFieldName = nameof(TrustIndex.CompanyNumber) },
                new FieldMapping("TrustName") { TargetFieldName = nameof(TrustIndex.TrustName) },
                new FieldMapping("TrustNameSortable") { TargetFieldName = nameof(TrustIndex.TrustNameSortable) }
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