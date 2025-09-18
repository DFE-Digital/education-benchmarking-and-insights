using System.Text.RegularExpressions;

namespace Web.E2ETests;

public static partial class Regexes
{
    [GeneratedRegex("[^a-z0-9\\-]", RegexOptions.IgnoreCase, "en-GB")]
    public static partial Regex NonAlphanumericOrHyphenRegex();

    [GeneratedRegex("\\s+")]
    public static partial Regex WhitespaceRegex();

    [GeneratedRegex(@"[^\w\s]")]
    public static partial Regex ReservedCharactersRegex();

    [GeneratedRegex(@"[^\/?#]+?\.[^\/?#]+(?=$|\?|#)")]
    public static partial Regex FileNameFromUrlRegex();

    [GeneratedRegex("Save.+as image")]
    public static partial Regex SaveAsImageRegex();

    [GeneratedRegex("Copy.+ image")]
    public static partial Regex CopyImageRegex();
}