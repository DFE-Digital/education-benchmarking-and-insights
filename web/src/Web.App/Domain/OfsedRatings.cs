namespace Web.App.Domain;

public static class OfstedRatings
{
    public const string NoDataAvailable = "No data available";
    public const string Outstanding = "Outstanding";
    public const string Good = "Good";
    public const string RequiresImprovement = "Requires Improvement";
    public const string Inadequate = "Inadequate";
    public const string Unknown = "Unknown";
    public static string GetDescription(string? value)
    {
        return value switch
        {
            "0" => NoDataAvailable,
            "1" => Outstanding,
            "2" => Good,
            "3" => RequiresImprovement,
            "4" => Inadequate,
            _ => Unknown
        };
    }
}