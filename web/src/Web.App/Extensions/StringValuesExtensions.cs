using Microsoft.Extensions.Primitives;

namespace Web.App.Extensions;

public static class StringValuesExtensions
{
    // todo: unit test
    public static IEnumerable<T> CastQueryToEnum<T>(this StringValues values)
    {
        var validValues = values.ToArray().Where(v => !string.IsNullOrWhiteSpace(v));
        foreach (var value in validValues)
        {
            if (int.TryParse(value!, out var parsed))
            {
                yield return (T)Enum.ToObject(typeof(T), parsed);
            }
        }
    }
}