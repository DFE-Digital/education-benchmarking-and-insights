namespace Platform.Domain;

public static class SixthFormProvision
{
    public const string HasSixthFormClasses = "Has a sixth form";
    public const string NoSixthFormClasses = "Does not have a sixth form";
    public const string NotApplicable = "Not applicable";
    public const string NotRecorded = "Not recorded";

    public static readonly string[] All =
    [
        HasSixthFormClasses,
        NoSixthFormClasses,
        NotApplicable,
        NotRecorded
    ];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}