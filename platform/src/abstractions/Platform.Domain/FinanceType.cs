namespace Platform.Domain;

public static class FinanceType
{
    public const string Academy = "Academy";
    public const string Maintained = "Maintained";

    public static readonly string[] All =
    [
        Academy,
        Maintained
    ];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}