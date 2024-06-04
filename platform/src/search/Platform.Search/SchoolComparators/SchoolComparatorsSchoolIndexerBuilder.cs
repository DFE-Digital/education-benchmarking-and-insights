using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;
using Platform.Search.School;

namespace Platform.Search.SchoolComparators;

public class SchoolComparatorsSchoolIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.SchoolComparatorsSchool;

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
                new FieldMapping("FinanceType") { TargetFieldName = nameof(SchoolComparatorsIndex.Sector) },
                new FieldMapping("OverallPhase") { TargetFieldName = nameof(SchoolComparatorsIndex.Phase) },
                new FieldMapping("LAName") { TargetFieldName = nameof(SchoolComparatorsIndex.LocalAuthority) },
                new FieldMapping("LondonWeighting") { TargetFieldName = nameof(SchoolComparatorsIndex.LondonWeighting) },
                new FieldMapping("OfstedDescription") { TargetFieldName = nameof(SchoolComparatorsIndex.OfstedRating) },
                new FieldMapping("IsPFISchool") { TargetFieldName = nameof(SchoolComparatorsIndex.PrivateFinanceInitiative) }
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