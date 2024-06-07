using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace Platform.Search.TrustComparators;

public class TrustComparatorsIndex
{
    [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? CompanyNumber { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? TotalPupils { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? SchoolsInTrust { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? TotalIncome { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string[]? PhasesCovered { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public DateTime? OpenDate { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentFreeSchoolMeals { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? PercentSpecialEducationNeeds { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? TotalInternalFloorArea { get; set; }
}
