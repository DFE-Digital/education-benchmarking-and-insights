namespace Web.App.Domain;

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

    public static Rating TeachingStaffRating(decimal value, decimal[] values, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        IDecileFinder decileCalculator = new FindFromLowest(value, values);
        var decile = decileCalculator.Find();

        int[] redDeciles = isGoodOrOutstandingOfsted
            ? iscloseRangeComparators
                ? [9, 10]
                : [10]
            : iscloseRangeComparators
                ? [1, 2, 9, 10]
                : [1, 10];

        int[] orangeDeciles = isGoodOrOutstandingOfsted
            ? iscloseRangeComparators
                ? [1, 2, 3, 7, 8]
                : [1, 2, 3, 7, 8, 9]
            : iscloseRangeComparators
                ? [3, 7, 8]
                : [2, 3, 7, 8, 9];

        int[] greenDeciles = [4, 5, 6];

        return GetRating(decile, redDeciles, orangeDeciles, greenDeciles);
    }

    public static Rating SupportStaffRating(decimal value, decimal[] values)
    {
        IDecileFinder decileCalculator = new FindFromLowest(value, values);
        var decile = decileCalculator.Find();

        int[] redDeciles = [9, 10];
        int[] orangeDeciles = [4, 5, 6, 7, 8];
        int[] greenDeciles = [1, 2, 3];

        return GetRating(decile, redDeciles, orangeDeciles, greenDeciles);
    }

    public static Rating EducationalSuppliesRating(decimal value, decimal[] values, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        IDecileFinder decileCalculator = new FindFromLowest(value, values);
        var decile = decileCalculator.Find();

        int[] redDeciles = isGoodOrOutstandingOfsted
            ? iscloseRangeComparators
                ? [9, 10]
                : [10]
            : iscloseRangeComparators
                ? [1, 2, 9, 10]
                : [1, 10];

        int[] orangeDeciles = isGoodOrOutstandingOfsted
            ? iscloseRangeComparators
                ? [1, 2, 3, 7, 8]
                : [1, 2, 3, 7, 8, 9]
            : iscloseRangeComparators
                ? [3, 7, 8]
                : [2, 3, 7, 8, 9];

        int[] greenDeciles = [4, 5, 6];

        return GetRating(decile, redDeciles, orangeDeciles, greenDeciles);
    }

    public static Rating EducationalIctRating(decimal value, decimal[] values, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        IDecileFinder decileCalculator = new FindFromLowest(value, values);
        var decile = decileCalculator.Find();

        int[] redDeciles = isGoodOrOutstandingOfsted
            ? iscloseRangeComparators
                ? [9, 10]
                : [10]
            : iscloseRangeComparators
                ? [1, 2, 9, 10]
                : [1, 10];

        int[] orangeDeciles = isGoodOrOutstandingOfsted
            ? iscloseRangeComparators
                ? [1, 2, 3, 7, 8]
                : [1, 2, 3, 7, 8, 9]
            : iscloseRangeComparators
                ? [3, 7, 8]
                : [2, 3, 7, 8, 9];

        int[] greenDeciles = [4, 5, 6];

        return GetRating(decile, redDeciles, orangeDeciles, greenDeciles);
    }

    public static Rating PremisesRating(decimal value, decimal[] values)
    {
        IDecileFinder decileCalculator = new FindFromLowest(value, values);
        var decile = decileCalculator.Find();

        int[] redDeciles = [9, 10];
        int[] orangeDeciles = [4, 5, 6, 7, 8];
        int[] greenDeciles = [1, 2, 3];

        return GetRating(decile, redDeciles, orangeDeciles, greenDeciles);
    }

    public static Rating UtilitiesRating(decimal value, decimal[] values)
    {
        IDecileFinder decileCalculator = new FindFromLowest(value, values);
        var decile = decileCalculator.Find();

        int[] redDeciles = [9, 10];
        int[] orangeDeciles = [4, 5, 6, 7, 8];
        int[] greenDeciles = [1, 2, 3];



        return GetRating(decile, redDeciles, orangeDeciles, greenDeciles);
    }

    public static Rating AdminSuppliesRating(decimal value, decimal[] values)
    {
        IDecileFinder decileCalculator = new FindFromLowest(value, values);
        var decile = decileCalculator.Find();

        int[] redDeciles = [9, 10];
        int[] orangeDeciles = [4, 5, 6, 7, 8];
        int[] greenDeciles = [1, 2, 3];

        return GetRating(decile, redDeciles, orangeDeciles, greenDeciles);
    }

    public static Rating CateringRating(decimal value, decimal[] values, bool iscloseRangeComparators)
    {
        IDecileFinder decileCalculator = new FindFromLowest(value, values);
        var decile = decileCalculator.Find();

        int[] redDeciles = iscloseRangeComparators
            ? [9, 10]
            : [10];

        int[] orangeDeciles = iscloseRangeComparators
            ? [4, 5, 6, 7, 8]
            : [4, 5, 6, 7, 8, 9];

        int[] greenDeciles = [1, 2, 3];

        return GetRating(decile, redDeciles, orangeDeciles, greenDeciles);
    }

    public static Rating OtherCostsRating(decimal value, decimal[] values)
    {
        IDecileFinder decileCalculator = new FindFromLowest(value, values);
        var decile = decileCalculator.Find();

        int[] redDeciles = [10];
        int[] orangeDeciles = [4, 5, 6, 7, 8, 9];
        int[] greenDeciles = [1, 2, 3];

        return GetRating(decile, redDeciles, orangeDeciles, greenDeciles);
    }

    public static Rating GetRating(int decile, int[] redDeciles, int[] orangeDeciles, int[] greenDeciles)
    {
        switch (decile)
        {
            case var val when redDeciles.Contains(val):
                return HighRisk;
            case var val when orangeDeciles.Contains(val):
                return MediumRisk;
            case var val when greenDeciles.Contains(val):
                return LowRisk;
            default:
                throw new ArgumentException($"decile: {decile} is not valid");
        }
    }

    public static Rating Red => new(TagColour.Red, "Red");
    public static Rating Amber => new(TagColour.Orange, "Amber");
    public static Rating Green => new(TagColour.Green, "Green");

    public static Rating HighRisk => new(TagColour.Red, "High risk");
    public static Rating MediumRisk => new(TagColour.Orange, "Medium risk");
    public static Rating LowRisk => new(TagColour.Green, "Low risk");
}