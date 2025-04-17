using Azure.Search.Documents.Indexes.Models;

namespace Platform.Search.Resources.CustomTextProcessing;

public static class CustomTokenizers
{
    public static NGramTokenizer NGramTokenizer => new("ngramTokenizer")
    {
        TokenChars = { TokenCharacterKind.Letter, TokenCharacterKind.Digit },
        MinGram = 3,
        MaxGram = 15
    };
}