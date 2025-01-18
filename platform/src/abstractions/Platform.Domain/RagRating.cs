namespace Platform.Domain;

//TODO: Consider converting these to enums
public static class RagRating
{
    public const string Red = "red";
    public const string Amber = "amber";
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