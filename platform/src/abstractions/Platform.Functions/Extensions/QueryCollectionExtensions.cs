using Microsoft.AspNetCore.Http;

namespace Platform.Functions.Extensions;

public static class QueryCollectionExtensions
{
    public static string[] ToStringArray(this IQueryCollection query, string parameterName)
    {
        return query[parameterName].ToString()
            .Split(",")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray();
    }

    public static bool ToBool(this IQueryCollection query, string parameterName)
    {
        return bool.TryParse(query[parameterName].ToString(), out var val) && val;
    }
}