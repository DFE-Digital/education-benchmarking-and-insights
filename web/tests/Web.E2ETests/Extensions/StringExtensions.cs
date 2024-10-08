using System.Text.RegularExpressions;
namespace Web.E2ETests;

public static partial class StringExtensions
{
    public static string ToSlug(this string source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return source;
        }

        return source
            .Trim()
            .Replace(WhitespaceRegex(), "-")
            .Replace(NonAlphanumericOrHyphenRegex(), string.Empty)
            .ToLower();
    }

    private static string Replace(this string source, Regex regex, string replacement)
        => string.IsNullOrWhiteSpace(source) ? source : regex.Replace(source, replacement);

    [GeneratedRegex("[^a-z0-9\\-]", RegexOptions.IgnoreCase, "en-GB")]
    private static partial Regex NonAlphanumericOrHyphenRegex();

    [GeneratedRegex("\\s+")]
    private static partial Regex WhitespaceRegex();
}