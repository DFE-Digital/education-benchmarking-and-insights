namespace EducationBenchmarking.Web.Domain;

public record Rating(TagColour Colour, string DisplayText);

public static class RatingCalculations
{
    public static Rating AverageClassSize(decimal value)
    {
        return value switch
        {
            < 19.5M => Red,
            >= 19.5M and < 20M => Amber,
            >= 20M and < 22.5M => Green,
            >= 22.5M and < 23.5M => Amber,
            >= 23.5M => Red
        };
    }

    public static Rating ContactRatio(decimal value)
    {
        return value switch
        {
            < 0.7M => Red,
            >= 0.7M and < 0.74M => Amber,
            >= 0.74M and < 0.8M => Green,
            >= 0.8M and < 0.82M => Amber,
            >= 0.82M => Red
        };
    }

    public static Rating InYearBalancePercentIncome(decimal value, bool withRatingText = false)
    {
        return value switch
        {
            < -5.0M => withRatingText ? HighRisk : Red,
            >= -5.0M and < 0M => withRatingText ? MediumRisk : Amber,
            >= 0M => withRatingText ? LowRisk : Green
        };
    }

    public static Rating Red => new(TagColour.Red, "Red");
    public static Rating Amber => new(TagColour.Orange, "Amber");
    public static Rating Green => new(TagColour.Green, "Green");

    public static Rating HighRisk => new(TagColour.Red, "High risk");
    public static Rating MediumRisk => new(TagColour.Orange, "Medium risk");
    public static Rating LowRisk => new(TagColour.Green, "Low risk");
}