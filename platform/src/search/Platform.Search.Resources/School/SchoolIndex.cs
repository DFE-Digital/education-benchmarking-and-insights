using Azure.Search.Documents.Indexes;
// ReSharper disable UnusedMember.Global

namespace Platform.Search.Resources.School;

public class SchoolIndex
{
    [SearchableField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? URN { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = false)]
    public string? SchoolName { get; set; }

    [SearchableField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressStreet { get; set; }

    [SearchableField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressLocality { get; set; }

    [SearchableField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressLine3 { get; set; }

    [SearchableField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressTown { get; set; }

    [SearchableField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressCounty { get; set; }

    [SearchableField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressPostcode { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? OverallPhase { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public int? PeriodCoveredByReturn { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? TotalPupils { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = true, IsFacetable = false, IsHidden = true)]
    public string? SchoolNameSortable { get; set; }
}