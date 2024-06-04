using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.SchoolComparators;

public class SchoolComparatorsIndexerBuilder : IndexerBuilder
{
    public override string Name => SearchResourceNames.Indexers.SchoolComparators;

    public override async Task Build(SearchIndexerClient client)
    {
        var indexer = new SearchIndexer(
            name: Name,
            dataSourceName: SearchResourceNames.DataSources.School,
            targetIndexName: SearchResourceNames.Indexes.School)
        {
            FieldMappings =
            {
                new FieldMapping("URN") { TargetFieldName = nameof(SchoolComparatorsIndex.URN) },
                new FieldMapping("FinanceType") { TargetFieldName = nameof(SchoolComparatorsIndex.FinanceType) },
                new FieldMapping("OverallPhase") { TargetFieldName = nameof(SchoolComparatorsIndex.OverallPhase) },
                new FieldMapping("LAName") { TargetFieldName = nameof(SchoolComparatorsIndex.LAName) },
                new FieldMapping("TotalPupils") { TargetFieldName = nameof(SchoolComparatorsIndex.TotalPupils) },
                new FieldMapping("PercentFreeSchoolMeals") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentFreeSchoolMeals) },
                new FieldMapping("PercentSpecialEducationNeeds") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentSpecialEducationNeeds) },
                new FieldMapping("LondonWeighting") { TargetFieldName = nameof(SchoolComparatorsIndex.LondonWeighting) },
                new FieldMapping("AverageBuildingAge") { TargetFieldName = nameof(SchoolComparatorsIndex.AverageBuildingAge) },
                new FieldMapping("TotalInternalFloorArea") { TargetFieldName = nameof(SchoolComparatorsIndex.TotalInternalFloorArea) },
                new FieldMapping("OfstedDescription") { TargetFieldName = nameof(SchoolComparatorsIndex.OfstedDescription) },
                new FieldMapping("SchoolsInTrust") { TargetFieldName = nameof(SchoolComparatorsIndex.SchoolsInTrust) },
                new FieldMapping("IsPFISchool") { TargetFieldName = nameof(SchoolComparatorsIndex.IsPFISchool) },
                new FieldMapping("TotalPupilsSixthForm") { TargetFieldName = nameof(SchoolComparatorsIndex.TotalPupilsSixthForm) },
                new FieldMapping("KS2Progress") { TargetFieldName = nameof(SchoolComparatorsIndex.KS2Progress) },
                new FieldMapping("KS4Progress") { TargetFieldName = nameof(SchoolComparatorsIndex.KS4Progress) },
                new FieldMapping("PercentWithVI") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithVI) },
                new FieldMapping("PercentWithSPLD") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithSPLD) },
                new FieldMapping("PercentWithSLD") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithSLD) },
                new FieldMapping("PercentWithSLCN") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithSLCN) },
                new FieldMapping("PercentWithSEMH") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithSEMH) },
                new FieldMapping("PercentWithPMLD") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithPMLD) },
                new FieldMapping("PercentWithPD") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithPD) },
                new FieldMapping("PercentWithOTH") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithOTH) },
                new FieldMapping("PercentWithMSI") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithMSI) },
                new FieldMapping("PercentWithMLD") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithMLD) },
                new FieldMapping("PercentWithHI") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithHI) },
                new FieldMapping("PercentWithASD") { TargetFieldName = nameof(SchoolComparatorsIndex.PercentWithASD) },
                new FieldMapping("SchoolPosition") { TargetFieldName = nameof(SchoolComparatorsIndex.SchoolPosition) },
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