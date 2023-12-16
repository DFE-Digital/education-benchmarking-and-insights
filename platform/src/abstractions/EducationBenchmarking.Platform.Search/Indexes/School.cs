using Azure.Search.Documents.Indexes;

namespace EducationBenchmarking.Platform.Search.Indexes;

public class School
{
    [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string Urn { get; set; }
    
    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string Name { get; set; }
    
    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string LaEstab { get; set; }
    
    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
    public string? FinanceType { get; set; }
    
    [SimpleField(IsFilterable = false, IsSortable = false, IsFacetable = false)]
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