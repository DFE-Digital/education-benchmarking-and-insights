namespace Platform.Domain.Extensions;

public static class ObjectExtensions
{
    public static decimal? DecimalValueByName<T>(this object obj, string? name)
    {
        return name != null ? typeof(T).GetProperty(name)?.GetValue(obj) as decimal? : null;
    }
}