using Azure.Search.Documents.Indexes;

namespace Platform.Search.Organisation;

public class OrganisationIndex
{
    [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? Id { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? Name { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? Identifier { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? Kind { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? Street { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? Locality { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? Address3 { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? Town { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? County { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? Postcode { get; set; }
}