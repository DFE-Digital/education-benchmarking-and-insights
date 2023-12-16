using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace EducationBenchmarking.Platform.Search.Indexes;

public class Establishment
{
    [SearchableField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false,
        AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string Id { get; set; }
    
    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false,
        AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string Name { get; set; }
    
    [SimpleField(IsFilterable = true, IsSortable = false, IsFacetable = false)]
    public string Kind { get; set; }
}