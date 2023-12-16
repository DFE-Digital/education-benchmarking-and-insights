using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace EducationBenchmarking.Platform.Search.Indexes;

public class School
{
    [SearchableField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false,
        AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string Urn { get; set; }
    
    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false,
        AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string Name { get; set; }
    
    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false,
        AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string LaEstab { get; set; }
}