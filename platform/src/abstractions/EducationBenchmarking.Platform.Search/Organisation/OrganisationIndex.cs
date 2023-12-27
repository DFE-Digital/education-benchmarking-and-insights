using Azure.Search.Documents.Indexes;

namespace EducationBenchmarking.Platform.Search.Organisation;

public class OrganisationIndex
{
    [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string Id { get; set; }
    
    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string Name { get; set; }
    
    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string Identifier { get; set; }
    
    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string Kind { get; set; }
}