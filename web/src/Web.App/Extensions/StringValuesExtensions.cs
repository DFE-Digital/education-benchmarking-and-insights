using Microsoft.Extensions.Primitives;

namespace Web.App.Extensions;

public static class StringValuesExtensions
{
    public static IEnumerable<T> CastQueryToEnum<T>(this StringValues values)
    {
        var validValues = values.ToArray().Where(v => !string.IsNullOrWhiteSpace(v));
        foreach (var value in validValues)
        {
            if (!int.TryParse(value!, out var parsedInt))
            {
                continue;
            }

            var parsedEnum = Enum.ToObject(typeof(T), parsedInt);
            if (Enum.IsDefined(typeof(T), parsedEnum))
            {
                yield return (T)parsedEnum;
            }
        }
    }
}