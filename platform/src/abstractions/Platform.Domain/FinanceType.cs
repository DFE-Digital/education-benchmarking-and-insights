namespace Platform.Domain;

//TODO: Consider converting these to enums
public static class FinanceType
{
    /// <summary>Academy school, directly funded by the DfE and independent of local authority control.</summary>
    public const string Academy = "Academy";
    /// <summary>Maintained school, funded and controlled by the local authority.</summary>
    public const string Maintained = "Maintained";

    public static readonly string[] All =
    [
        Academy,
        Maintained
    ];


    //TODO: Add unit test coverage
    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}