using Azure.Search.Documents.Indexes;

namespace Platform.Search.Resources.LocalAuthority;

public class LocalAuthorityIndex
{
    [SearchableField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? Code { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? Name { get; set; }

    [SimpleField(IsFilterable = false, IsSortable = true, IsFacetable = false, IsHidden = true)]
    public string? LocalAuthorityNameSortable { get; set; }
}