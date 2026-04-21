namespace Platform.Domain;

//TODO: Consider converting these to enums
public static class RagRating
{
    /// <summary>Indicates high priority or critical concern (bottom 10%).</summary>
    public const string Red = "red";
    /// <summary>Indicates medium priority or moderate concern (middle 60%).</summary>
    public const string Amber = "amber";
    /// <summary>Indicates low priority or healthy performance (top 30%).</summary>
    public const string Green = "green";

    public static readonly string[] All =
    [
        Red,
        Amber,
        Green
    ];


    //TODO: Add unit test coverage
    public static bool IsValid(string? ragRating) => All.Any(a => a.Equals(ragRating, StringComparison.OrdinalIgnoreCase));
}