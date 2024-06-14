namespace Web.App.ViewModels;

public record RatingViewModel(TagColour? Colour, string? DisplayText)
{
    public static readonly Dictionary<string, RatingViewModel> Map = new()
    {
        { "red", Red },
        { "amber", Amber },
        { "green", Green }
    };

    public static RatingViewModel Red => new(TagColour.Red, "Red");
    public static RatingViewModel Amber => new(TagColour.Yellow, "Amber");
    public static RatingViewModel Green => new(TagColour.Green, "Green");

    public static RatingViewModel Create(string? rating)
    {
        return Map[rating ?? ""];
    }
}