using Azure.Search.Documents.Indexes;

namespace Platform.Search.Trust;

public class TrustIndex
{
    [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? CompanyNumber { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string? TrustName { get; set; }
}