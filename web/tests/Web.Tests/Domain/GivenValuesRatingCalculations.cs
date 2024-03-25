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
    [MemberData(nameof(TeachingStaffRatingData))]
    public void GetsTeachingStaffRatingCorrectly(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted, Rating expected)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var result = RatingCalculations.TeachingStaffRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(SupportStaffRatingData))]
    public void GetsSupportStaffRatingCorrectly(decimal value, Rating expected)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        var result = RatingCalculations.SupportStaffRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(EducationalSuppliesRatingData))]
    public void GetsEducationalSuppliesRatingCorrectly(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted, Rating expected)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        var result = RatingCalculations.EducationalSuppliesRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(EducationalIctRatingData))]
    public void GetsEducationalIctRatingCorrectly(decimal value, bool iscloseRangeComparators, bool isGoodOrOutstandingOfsted, Rating expected)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        var result = RatingCalculations.EducationalIctRating(value, values, iscloseRangeComparators, isGoodOrOutstandingOfsted);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(PremisesRatingData))]
    public void GetsPremisesRatingCorrectly(decimal value, Rating expected)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        var result = RatingCalculations.PremisesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(UtilitiesRatingData))]
    public void GetsUtilitiesCorrectly(decimal value, Rating expected)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        var result = RatingCalculations.UtilitiesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(AdminSuppliesRatingData))]
    public void GetsAdminSuppliesRatingCorrectly(decimal value, Rating expected)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        var result = RatingCalculations.AdminSuppliesRating(value, values);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(CateringRatingData))]
    public void GetsCateringRatingCorrectly(decimal value, bool iscloseRangeComparators, Rating expected)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        var result = RatingCalculations.CateringRating(value, values, iscloseRangeComparators);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(OtherCostsRatingData))]
    public void GetsOtherCostsRatingCorrectly(decimal value, Rating expected)
    {
        decimal[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

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

    public static IEnumerable<object[]> TeachingStaffRatingData()
    {
        yield return new object[]
        {
            1,
            false,
            false,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            9,
            false,
            false,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
            5,
            false,
            false,
            new Rating(TagColour.Green, "Low risk")
        };

        yield return new object[]
        {
            9,
            true,
            false,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            3,
            true,
            false,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
            10,
            false,
            true,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            8,
            false,
            true,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
            9,
            true,
            true,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            1,
            true,
            true,
            new Rating(TagColour.Orange, "Medium risk")
        };
    }

    public static IEnumerable<object[]> SupportStaffRatingData()
    {
        yield return new object[]
        {
          9,
          new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
          6,
          new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
          3,
          new Rating(TagColour.Green, "Low risk")
        };
    }

    public static IEnumerable<object[]> EducationalSuppliesRatingData()
    {
        yield return new object[]
        {
            1,
            false,
            false,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            9,
            false,
            false,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
            5,
            false,
            false,
            new Rating(TagColour.Green, "Low risk")
        };

        yield return new object[]
        {
            9,
            true,
            false,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            3,
            true,
            false,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
            10,
            false,
            true,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            8,
            false,
            true,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
            9,
            true,
            true,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            1,
            true,
            true,
            new Rating(TagColour.Orange, "Medium risk")
        };
    }

    public static IEnumerable<object[]> EducationalIctRatingData()
    {
        yield return new object[]
        {
            1,
            false,
            false,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            9,
            false,
            false,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
            5,
            false,
            false,
            new Rating(TagColour.Green, "Low risk")
        };

        yield return new object[]
        {
            9,
            true,
            false,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            3,
            true,
            false,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
            10,
            false,
            true,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            8,
            false,
            true,
            new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
            9,
            true,
            true,
            new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
            1,
            true,
            true,
            new Rating(TagColour.Orange, "Medium risk")
        };
    }

    public static IEnumerable<object[]> PremisesRatingData()
    {
        yield return new object[]
        {
          9,
          new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
          6,
          new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
          3,
          new Rating(TagColour.Green, "Low risk")
        };
    }

    public static IEnumerable<object[]> UtilitiesRatingData()
    {
        yield return new object[]
        {
          9,
          new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
          6,
          new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
          3,
          new Rating(TagColour.Green, "Low risk")
        };
    }

    public static IEnumerable<object[]> AdminSuppliesRatingData()
    {
        yield return new object[]
        {
          9,
          new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
          6,
          new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
          3,
          new Rating(TagColour.Green, "Low risk")
        };
    }

    public static IEnumerable<object[]> CateringRatingData()
    {
        yield return new object[]
        {
          9,
          true,
          new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
          6,
          true,
          new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
          3,
          true,
          new Rating(TagColour.Green, "Low risk")
        };

        yield return new object[]
        {
          9,
          false,
          new Rating(TagColour.Orange, "Medium risk")
        };
    }

    public static IEnumerable<object[]> OtherCostsRatingData()
    {
        yield return new object[]
        {
          10,
          new Rating(TagColour.Red, "High risk")
        };

        yield return new object[]
        {
          6,
          new Rating(TagColour.Orange, "Medium risk")
        };

        yield return new object[]
        {
          3,
          new Rating(TagColour.Green, "Low risk")
        };
    }
}