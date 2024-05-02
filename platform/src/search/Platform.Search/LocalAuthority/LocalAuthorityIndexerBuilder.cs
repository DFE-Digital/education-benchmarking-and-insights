using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.LocalAuthority;

public class LocalAuthorityIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.LocalAuthority;

    public override async Task Build(SearchIndexerClient client)
    {
        var indexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.LocalAuthority,
            targetIndexName: SearchResourceNames.Indexes.LocalAuthority)
        {
            FieldMappings =
            {
                new FieldMapping("Code") { TargetFieldName = nameof(LocalAuthorityIndex.Code) },
                new FieldMapping("Name") { TargetFieldName = nameof(LocalAuthorityIndex.Name) }
            }
        };

        await client.CreateOrUpdateIndexerAsync(indexer);
    }

    public override async Task Reset(SearchIndexerClient client)
    {
        await client.GetIndexerAsync(Name);
        await client.ResetIndexerAsync(Name);
    }
}