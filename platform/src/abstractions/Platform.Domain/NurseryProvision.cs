namespace Platform.Domain;

public static class NurseryProvision
{
    public const string HasNurseryClasses = "Has Nursery Classes";
    public const string NoNurseryClasses = "No Nursery Classes";
    public const string NotApplicable = "Not applicable";
    public const string NotRecorded = "Not recorded";

    public static readonly string[] All =
    [
        HasNurseryClasses,
        NoNurseryClasses,
        NotApplicable,
        NotRecorded
    ];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}