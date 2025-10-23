namespace Platform.Domain;

public static class SpecialClassProvision
{
    public const string HasSpecialClass = "Has Special Classes";
    public const string NoSpecialClass = "No Special Classes";
    public const string NotApplicable = "Not applicable";
    public const string NotRecorded = "Not recorded";

    public static readonly string[] All =
    [
        HasSpecialClass,
        NoSpecialClass,
        NotApplicable,
        NotRecorded
    ];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}