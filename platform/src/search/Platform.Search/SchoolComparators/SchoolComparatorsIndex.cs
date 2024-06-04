using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace Platform.Search.SchoolComparators;

public class SchoolComparatorsIndex
{
    [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? URN { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? FinanceType { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? OverallPhase { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? LAName { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double TotalPupils { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double PercentFreeSchoolMeals { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double PercentSpecialEducationNeeds { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? SchoolType { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? Region { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? LondonWeighting { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public int AverageBuildingAge { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public int TotalInternalFloorArea { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? OfstedDescription { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public int? SchoolsInTrust { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string? SchoolPosition { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public bool IsPFISchool { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public int? TotalPupilsSixthForm { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? KS2Progress { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? KS4Progress { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithVI { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithSPLD { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithSLD { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithSLCN { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithSEMH { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithPMLD { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithPD { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithOTH { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithMSI { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithMLD { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithHI { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentWithASD { get; set; }
}
