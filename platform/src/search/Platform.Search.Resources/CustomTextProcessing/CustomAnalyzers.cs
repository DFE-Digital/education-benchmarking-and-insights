using Azure.Search.Documents.Indexes.Models;

namespace Platform.Search.Resources.CustomTextProcessing;

public static class CustomAnalyzers
{
    public static CustomAnalyzer NGramAnalyzer => new("ngramAnalyzer", "ngramTokenizer")
    {
        TokenFilters = { TokenFilterName.Lowercase }
    };

    public static CustomAnalyzer NormalizedKeywordAnalyzer => new("normalizedKeywordAnalyzer", LexicalTokenizerName.Keyword)
    {
        TokenFilters = {
            TokenFilterName.Lowercase,
            new TokenFilterName("removeWhitespace")
        }
    };
}