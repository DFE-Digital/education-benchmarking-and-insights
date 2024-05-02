using Azure.Search.Documents.Indexes;

namespace Platform.Search.LocalAuthority;

public class LocalAuthorityIndex
{
    [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? Code { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? Name { get; set; }
}