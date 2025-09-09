using Azure.Search.Documents.Indexes;

// ReSharper disable UnusedMember.Global

namespace Platform.Search.Resources.Trust;

public class TrustIndex
{
    [SearchableField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? CompanyNumber { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? TrustName { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? TotalPupils { get; set; }

    [SimpleField(IsFilterable = true, IsFacetable = false, IsSortable = false)]
    public double? SchoolsInTrust { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = true, IsFacetable = false, IsHidden = true)]
    public string? TrustNameSortable { get; set; }
}