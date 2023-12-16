using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace EducationBenchmarking.Platform.Search.Indexes;

public class Trust
{
    [SearchableField(IsKey = true, IsFilterable = true, IsSortable = false, IsFacetable = false,
        AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string CompanyNumber { get; set; }
    
    [SearchableField(IsFilterable = true, IsSortable = false, IsFacetable = false,
        AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
    public string Name { get; set; }
}