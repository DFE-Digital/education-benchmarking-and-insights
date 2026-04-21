// ReSharper disable MemberCanBePrivate.Global

namespace Platform.Domain;

public static class SortDirection
{
    /// <summary>Sort results in ascending order.</summary>
    public const string Asc = "ASC";
    /// <summary>Sort results in descending order.</summary>
    public const string Desc = "DESC";

    public static readonly string[] All = [Asc, Desc];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}