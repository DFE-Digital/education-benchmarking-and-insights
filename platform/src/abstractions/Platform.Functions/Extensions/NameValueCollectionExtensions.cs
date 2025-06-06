using System.Collections.Specialized;

namespace Platform.Functions.Extensions;

public static class NameValueCollectionExtensions
{
    public static string[] ToStringArray(this NameValueCollection query, string parameterName)
    {
        return query[parameterName]?
            .Split(",", StringSplitOptions.TrimEntries)
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray() ?? [];
    }

    public static bool ToBool(this NameValueCollection query, string parameterName)
    {
        return bool.TryParse(query[parameterName], out var val) && val;
    }

    public static bool TryGetValue(this NameValueCollection query, string parameterName, out string value)
    {
        value = query.Get(parameterName) ?? string.Empty;
        return !string.IsNullOrWhiteSpace(value);
    }
}