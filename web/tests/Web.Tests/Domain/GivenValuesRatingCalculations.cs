using Web.App.Domain;
using Xunit;
using Web.App;

namespace Web.Tests.Domain;

public class GivenValuesRatingCalculation
{
    [Theory]
    [MemberData(nameof(RatingTestData))]
    public void GetsRatingCorrectly(int decile, Rating expected)
    {
        int[] redDeciles = [1, 2, 3];
        int[] orangeDeciles = [7, 8, 9, 10];
        int[] greenDeciles = [4, 5, 6];

        var result = RatingCalculations.GetRating(decile, redDeciles, orangeDeciles, greenDeciles);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetsRatingsThrowsWithInvalidValue()
    {
        int[] redDeciles = [1, 2, 3];
        int[] orangeDeciles = [4, 5, 6];
        int[] greenDeciles = [7, 8, 9, 10];

        Assert.Throws<ArgumentException>(() => RatingCalculations.GetRating(11, redDeciles, orangeDeciles, greenDeciles));
    }

    [Theory]
    [InlineData(9, true, true)]
    [InlineData(10, true, true)]
    [InlineData(10, false, true)]
    [InlineData(1, true, false)]
    [InlineData(2, true, false)]
    [InlineData(9, true, false)]
    [InlineData(10, true, false)]
    [InlineData(1, false, false)]
    [InlineData(10, false, false)]
    public void GetsTeachingStaffRatingReturnsHigh(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Red, "High risk");

        var result = RatingCalculations.TeachingStaffRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, true, true)]
    [InlineData(2, true, true)]
    [InlineData(3, true, true)]
    [InlineData(7, true, true)]
    [InlineData(8, true, true)]
    [InlineData(1, false, true)]
    [InlineData(2, false, true)]
    [InlineData(3, false, true)]
    [InlineData(7, false, true)]
    [InlineData(8, false, true)]
    [InlineData(9, false, true)]
    [InlineData(3, true, false)]
    [InlineData(7, true, false)]
    [InlineData(8, true, false)]
    [InlineData(2, false, false)]
    [InlineData(3, false, false)]
    [InlineData(7, false, false)]
    [InlineData(8, false, false)]
    [InlineData(9, false, false)]

    public void GetsTeachingStaffRatingReturnsMedium(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Orange, "Medium risk");

        var result = RatingCalculations.TeachingStaffRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4, true, true)]
    [InlineData(5, true, true)]
    [InlineData(6, true, true)]
    [InlineData(4, false, true)]
    [InlineData(5, false, true)]
    [InlineData(6, false, true)]
    [InlineData(4, true, false)]
    [InlineData(5, true, false)]
    [InlineData(6, true, false)]
    [InlineData(4, false, false)]
    [InlineData(5, false, false)]
    [InlineData(6, false, false)]

    public void GetsTeachingStaffRatingReturnsLow(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Green, "Low risk");

        var result = RatingCalculations.TeachingStaffRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(9)]
    [InlineData(10)]
    public void GetsSupportStaffRatingReturnsHigh(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Red, "High risk");

        var result = RatingCalculations.SupportStaffRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]

    public void GetsSupportStaffRatingReturnsMedium(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Orange, "Medium risk");

        var result = RatingCalculations.SupportStaffRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetsSupportStaffRatingReturnsLow(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Green, "Low risk");

        var result = RatingCalculations.SupportStaffRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(9, true, true)]
    [InlineData(10, true, true)]
    [InlineData(10, false, true)]
    [InlineData(1, true, false)]
    [InlineData(2, true, false)]
    [InlineData(9, true, false)]
    [InlineData(10, true, false)]
    [InlineData(1, false, false)]
    [InlineData(10, false, false)]
    public void GetsEducationalSuppliesRatingReturnsHigh(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Red, "High risk");

        var result = RatingCalculations.EducationalSuppliesRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, true, true)]
    [InlineData(2, true, true)]
    [InlineData(3, true, true)]
    [InlineData(7, true, true)]
    [InlineData(8, true, true)]
    [InlineData(1, false, true)]
    [InlineData(2, false, true)]
    [InlineData(3, false, true)]
    [InlineData(7, false, true)]
    [InlineData(8, false, true)]
    [InlineData(9, false, true)]
    [InlineData(3, true, false)]
    [InlineData(7, true, false)]
    [InlineData(8, true, false)]
    [InlineData(2, false, false)]
    [InlineData(3, false, false)]
    [InlineData(7, false, false)]
    [InlineData(8, false, false)]
    [InlineData(9, false, false)]

    public void GetsEducationalSuppliesRatingReturnsMedium(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Orange, "Medium risk");

        var result = RatingCalculations.EducationalSuppliesRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4, true, true)]
    [InlineData(5, true, true)]
    [InlineData(6, true, true)]
    [InlineData(4, false, true)]
    [InlineData(5, false, true)]
    [InlineData(6, false, true)]
    [InlineData(4, true, false)]
    [InlineData(5, true, false)]
    [InlineData(6, true, false)]
    [InlineData(4, false, false)]
    [InlineData(5, false, false)]
    [InlineData(6, false, false)]

    public void GetsEducationalSuppliesRatingReturnsLow(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Green, "Low risk");

        var result = RatingCalculations.EducationalSuppliesRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(9, true, true)]
    [InlineData(10, true, true)]
    [InlineData(10, false, true)]
    [InlineData(1, true, false)]
    [InlineData(2, true, false)]
    [InlineData(9, true, false)]
    [InlineData(10, true, false)]
    [InlineData(1, false, false)]
    [InlineData(10, false, false)]
    public void GetsEducationalIctRatingReturnsHigh(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Red, "High risk");

        var result = RatingCalculations.EducationalSuppliesRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, true, true)]
    [InlineData(2, true, true)]
    [InlineData(3, true, true)]
    [InlineData(7, true, true)]
    [InlineData(8, true, true)]
    [InlineData(1, false, true)]
    [InlineData(2, false, true)]
    [InlineData(3, false, true)]
    [InlineData(7, false, true)]
    [InlineData(8, false, true)]
    [InlineData(9, false, true)]
    [InlineData(3, true, false)]
    [InlineData(7, true, false)]
    [InlineData(8, true, false)]
    [InlineData(2, false, false)]
    [InlineData(3, false, false)]
    [InlineData(7, false, false)]
    [InlineData(8, false, false)]
    [InlineData(9, false, false)]

    public void GetsEducationalIctRatingReturnsMedium(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Orange, "Medium risk");

        var result = RatingCalculations.EducationalSuppliesRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4, true, true)]
    [InlineData(5, true, true)]
    [InlineData(6, true, true)]
    [InlineData(4, false, true)]
    [InlineData(5, false, true)]
    [InlineData(6, false, true)]
    [InlineData(4, true, false)]
    [InlineData(5, true, false)]
    [InlineData(6, true, false)]
    [InlineData(4, false, false)]
    [InlineData(5, false, false)]
    [InlineData(6, false, false)]

    public void GetsEducationalIctRatingReturnsLow(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Green, "Low risk");

        var result = RatingCalculations.EducationalIctRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(9)]
    [InlineData(10)]
    public void GetsPremisesRatingReturnsHigh(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Red, "High risk");

        var result = RatingCalculations.PremisesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]

    public void GetsPremisesRatingReturnsMedium(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Orange, "Medium risk");

        var result = RatingCalculations.PremisesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetsPremisesRatingReturnsLow(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Green, "Low risk");

        var result = RatingCalculations.PremisesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(9)]
    [InlineData(10)]
    public void GetsUtilitiesRatingReturnsHigh(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Red, "High risk");

        var result = RatingCalculations.UtilitiesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]

    public void GetsUtilitiesRatingReturnsMedium(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Orange, "Medium risk");

        var result = RatingCalculations.UtilitiesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetsUtilitiesRatingReturnsLow(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Green, "Low risk");

        var result = RatingCalculations.UtilitiesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(9)]
    [InlineData(10)]
    public void GetsAdminSuppliesRatingReturnsHigh(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Red, "High risk");

        var result = RatingCalculations.AdminSuppliesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]

    public void GetsAdminSuppliesRatingReturnsMedium(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Orange, "Medium risk");

        var result = RatingCalculations.AdminSuppliesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetsAdminSuppliesRatingReturnsLow(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Green, "Low risk");

        var result = RatingCalculations.AdminSuppliesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(9, true)]
    [InlineData(10, true)]
    [InlineData(10, false)]
    public void GetsCateringRatingReturnsHigh(decimal value, bool iscloseRangeComparators)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Red, "High risk");

        var result = RatingCalculations.CateringRating(value, values, iscloseRangeComparators);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4, true)]
    [InlineData(5, true)]
    [InlineData(6, true)]
    [InlineData(7, true)]
    [InlineData(8, true)]
    [InlineData(4, false)]
    [InlineData(5, false)]
    [InlineData(6, false)]
    [InlineData(7, false)]
    [InlineData(8, false)]
    [InlineData(9, false)]

    public void GetsCateringRatingReturnsMedium(decimal value, bool iscloseRangeComparators)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Orange, "Medium risk");

        var result = RatingCalculations.CateringRating(value, values, iscloseRangeComparators);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(1, false)]
    [InlineData(2, false)]
    [InlineData(3, false)]
    public void GetsCateringRatingReturnsLow(decimal value, bool iscloseRangeComparators)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Green, "Low risk");

        var result = RatingCalculations.CateringRating(value, values, iscloseRangeComparators);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10)]
    public void GetsOtherCostsRatingReturnsHigh(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Red, "High risk");

        var result = RatingCalculations.OtherCostsRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]

    public void GetsOtherCostsRatingReturnsMedium(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Orange, "Medium risk");

        var result = RatingCalculations.OtherCostsRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetsOtherCostsRatingReturnsLow(decimal value)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var expected = new Rating(TagColour.Green, "Low risk");

        var result = RatingCalculations.OtherCostsRating(value, values);

        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> RatingTestData()
    {
        yield return new object[]
        {
            1,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            2,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            3,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
           7,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
           8,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
           9,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
           10,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
            4,
            new Rating(TagColour.Green, "Low risk")
        };

        yield return new object[]
        {
            5,
            new Rating(TagColour.Green, "Low risk")
        };

        yield return new object[]
        {
            6,
            new Rating(TagColour.Green, "Low risk")
        };
    }
}