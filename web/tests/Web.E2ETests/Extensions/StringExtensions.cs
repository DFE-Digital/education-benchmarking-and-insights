using System.Text.RegularExpressions;

namespace Web.E2ETests;

public static class StringExtensions
{
    public static string ToSlug(this string source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return source;
        }

        return source
            .Trim()
            .Replace(Regexes.WhitespaceRegex(), "-")
            .Replace(Regexes.NonAlphanumericOrHyphenRegex(), string.Empty)
            .ToLower();
    }

    private static string Replace(this string source, Regex regex, string replacement)
        => string.IsNullOrWhiteSpace(source) ? source : regex.Replace(source, replacement);
}