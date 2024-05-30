using Azure.Search.Documents.Indexes;

namespace Platform.Search.School;

public class SchoolIndex
{
    [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? URN { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? SchoolName { get; set; }

    //Need to review address fields
    /*[SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
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
    public string? Postcode { get; set; }*/
}