using Azure.Search.Documents.Indexes.Models;

namespace Platform.Search.Resources.CustomTextProcessing;

public static class CustomTokenFilters
{
    public static PatternReplaceTokenFilter RemoveWhitespace => new(
        name: "removeWhitespace",
        pattern: @"\s+",
        replacement: ""
    );
}