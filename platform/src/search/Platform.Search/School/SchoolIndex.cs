using Azure.Search.Documents.Indexes;

namespace Platform.Search.School;

public class SchoolIndex
{
    [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? URN { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? SchoolName { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressStreet { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressLocality { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressLine3 { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressTown { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressCounty { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? AddressPostcode { get; set; }
}