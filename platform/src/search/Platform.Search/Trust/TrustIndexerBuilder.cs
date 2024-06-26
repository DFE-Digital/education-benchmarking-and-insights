using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.Trust;

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
            FieldMappings =
            {
                new FieldMapping("CompanyNumber") { TargetFieldName = nameof(TrustIndex.CompanyNumber) },
                new FieldMapping("TrustName") { TargetFieldName = nameof(TrustIndex.TrustName)  }
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