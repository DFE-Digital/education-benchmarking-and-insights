using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Web.App.Extensions;

[ExcludeFromCodeCoverage]
public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, value.ToJson(Formatting.None));
    }

    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : value.FromJson<T>();
    }
}