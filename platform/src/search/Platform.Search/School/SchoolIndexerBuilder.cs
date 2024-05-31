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
                new FieldMapping("SchoolName") { TargetFieldName = nameof(SchoolIndex.SchoolName) }
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