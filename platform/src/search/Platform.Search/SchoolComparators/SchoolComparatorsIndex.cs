using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace Platform.Search.SchoolComparators;

public class SchoolComparatorsIndex
{
    [SimpleField(IsKey = true, IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? UKPRN { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? Sector { get; set; } // Academy / Maintained

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? Phase { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? LocalAuthority { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public int NumberOfPupils { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public decimal PercentFreeSchoolMeals { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public decimal PercentSenWithoutPlan { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public decimal PercentSenWithPlan { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? SchoolType { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? Region { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? LondonWeighting { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public int AverageBuildingAge { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public int GrossInternalFloorArea { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? OfstedRating { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public int? NumberSchoolsInTrust { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? SchoolPosition { get; set; } // Deficit / Surplus

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public bool PrivateFinanceInitiative { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public int? NumberOfPupilsSixthForm { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public decimal? KeyStage2Progress { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public decimal? KeyStage4Progress { get; set; }
}