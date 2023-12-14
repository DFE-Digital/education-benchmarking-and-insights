namespace EducationBenchmarking.Platform.Shared;

public static class ObjectExtensions
{
    public static decimal? GetDecimalValueByName<T>(this object obj, string? name)
    {
        return name != null ? typeof(T).GetProperty(name)?.GetValue(obj) as decimal? : null;
    }
}