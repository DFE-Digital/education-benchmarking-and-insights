using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Extensions;

[ExcludeFromCodeCoverage]
public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, value.ToJson());
    }

    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : value.FromJson<T>();
    }
}