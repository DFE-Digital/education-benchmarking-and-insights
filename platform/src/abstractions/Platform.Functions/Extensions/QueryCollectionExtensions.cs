using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Platform.Functions.Extensions;

[ExcludeFromCodeCoverage]
public static class QueryCollectionExtensions
{
    [Obsolete("Switch over to NameValueCollection and use ToStringArray(this NameValueCollection query, string parameterName) instead")]
    public static string[] ToStringArray(this IQueryCollection query, string parameterName)
    {
        return query[parameterName].ToString()
            .Split(",")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray();
    }

    [Obsolete("Switch over to NameValueCollection and use ToBool(this NameValueCollection query, string parameterName) instead")]
    public static bool ToBool(this IQueryCollection query, string parameterName) => bool.TryParse(query[parameterName].ToString(), out var val) && val;
}