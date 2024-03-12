using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Web.App.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class StringExtensions
    {
        private static readonly TextInfo TextInfo = CultureInfo.CurrentCulture.TextInfo;
        public static string Santise(this string source)
        {
            return TextInfo.ToTitleCase(Regex.Replace(source, @"\s{2,}", " ")).Trim();
        }

        public static int? ToInt(this string? source)
        {
            return int.TryParse(source, out var val) ? val : null;
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string SubstringToLast(this string source, char c)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;

            var index = source.LastIndexOf(c);
            return index > 0 ? source.Substring(0, index) : source;
        }

        public static string SubstringFromLast(this string source, char c)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;

            var index = source.LastIndexOf(c) + 1;
            return index > 0 ? source.Substring(index, source.Length - index) : source;
        }

        public static string SplitPascalCase(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;

            var stringBuilder = new StringBuilder(source.Length + 10);
            for (var index = 0; index < source.Length; ++index)
            {
                var c = source[index];

                if (char.IsUpper(c) && (index > 1 && !char.IsUpper(source[index - 1]) || index + 1 < source.Length && !char.IsUpper(source[index + 1])))
                    stringBuilder.Append(' ');

                if (index > 1
                    && stringBuilder[Math.Min(stringBuilder.Length - 1, index + 1)] == ' '
                    && !char.IsUpper(source[Math.Min(source.Length - 1, index + 1)]))
                {
                    stringBuilder.Append(char.ToLower(c));
                }
                else
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Trim();
        }

        public static string Capitalise(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;

            return char.IsLower(source[0]) ? $"{char.ToUpper(source[0])}{source.Substring(1)}" : source;
        }

        public static string Uncapitalise(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;

            return char.IsUpper(source[0]) ? $"{char.ToLower(source[0])}{source.Substring(1)}" : source;
        }

        public static string GetIndefiniteArticle(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;

            var first = char.ToLower(source[0]);

            if (first == 'a' || first == 'e' || first == 'i' || first == 'o' || first == 'u')
                return "an";

            return "a";
        }
    }
}