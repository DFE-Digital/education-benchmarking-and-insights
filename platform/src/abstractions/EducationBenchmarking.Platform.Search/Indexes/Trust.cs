using Azure.Search.Documents.Indexes;

namespace EducationBenchmarking.Platform.Search.Indexes;

public class Trust
{
    [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string CompanyNumber { get; set; }
    
    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string Name { get; set; }
}