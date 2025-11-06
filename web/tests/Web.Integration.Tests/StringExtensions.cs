using System.Text.RegularExpressions;

namespace Web.Integration.Tests;

public static partial class StringExtensions
{
    public static string Replace(this string source, Regex regex, string replacement)
        => string.IsNullOrWhiteSpace(source) ? source : regex.Replace(source, replacement);

    [GeneratedRegex("\\s+")]
    public static partial Regex WhitespaceRegex();
}