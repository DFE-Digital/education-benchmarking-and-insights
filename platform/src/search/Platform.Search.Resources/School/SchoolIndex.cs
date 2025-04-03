using Azure.Search.Documents.Indexes;

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

    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = true)]
    public string? OverallPhase { get; set; }
}