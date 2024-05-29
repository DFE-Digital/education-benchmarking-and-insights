using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.SchoolComparators;

public class SchoolComparatorsMaintainedIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.SchoolComparatorsMaintained;

    public override async Task Build(SearchIndexerClient client)
    {
        var cosmosDbIndexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.SchoolComparatorsAcademy,
            targetIndexName: SearchResourceNames.Indexes.SchoolComparators)
        {
            FieldMappings =
            {
                new FieldMapping("URN") { TargetFieldName = nameof(SchoolComparatorsIndex.URN) },
                new FieldMapping("FinanceType") { TargetFieldName = nameof(SchoolComparatorsIndex.Sector) },
                new FieldMapping("Overall Phase") { TargetFieldName = nameof(SchoolComparatorsIndex.Phase) },
                new FieldMapping("LA") { TargetFieldName = nameof(SchoolComparatorsIndex.LocalAuthority) },
                new FieldMapping("No Pupils") { TargetFieldName = nameof(SchoolComparatorsIndex.NumberOfPupils) },
                new FieldMapping("% of pupils eligible for FSM") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentFreeSchoolMeals) },
                new FieldMapping("% of pupils with SEN Statement") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentSenWithoutPlan) },
                new FieldMapping("% of pupils without SEN Statement") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentSenWithPlan) },
                new FieldMapping("Type") { TargetFieldName = nameof(SchoolComparatorsIndex.SchoolType) },
                new FieldMapping("GOR") { TargetFieldName = nameof(SchoolComparatorsIndex.Region) },
                new FieldMapping("London Weighting") { TargetFieldName = nameof(SchoolComparatorsIndex.LondonWeighting) },
                new FieldMapping("OfstedRating") { TargetFieldName = nameof(SchoolComparatorsIndex.OfstedRating) },
                new FieldMapping("KS2 Progress") { TargetFieldName = nameof(SchoolComparatorsIndex.KeyStage2Progress) }
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