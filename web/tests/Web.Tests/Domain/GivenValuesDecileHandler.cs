using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenValuesDecileHandler
{
    [Theory]
    [InlineData(-100, 1)]
    [InlineData(-1, 1)]
    [InlineData(2, 1)]
    [InlineData(4, 2)]
    [InlineData(9, 3)]
    [InlineData(11, 4)]
    [InlineData(12.01, 5)]
    [InlineData(17.99, 6)]
    [InlineData(20, 7)]
    [InlineData(23, 8)]
    [InlineData(27, 9)]
    [InlineData(27.01, 10)]
    [InlineData(31, 10)]
    [InlineData(100, 10)]
    public void FindsDecileCorrectly(decimal value, int expected)
    {
        var values = Enumerable.Range(1, 30).Select(x => (decimal)x).ToArray();
        var decileCalculator = new DecileHandler(value, values);

        var result = decileCalculator.FindDecile();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-100, 1)]
    [InlineData(-10, 1)]
    [InlineData(-9, 1)]
    [InlineData(-7, 2)]
    [InlineData(-6.99, 3)]
    [InlineData(-3.01, 4)]
    [InlineData(-1.5, 5)]
    [InlineData(0, 6)]
    [InlineData(1.01, 7)]
    [InlineData(3.1, 8)]
    [InlineData(7, 9)]
    [InlineData(7.01, 10)]
    [InlineData(100, 10)]
    public void FindsDecileCorrectlyWithNegativeValues(decimal value, int expected)
    {
        var values = Enumerable.Range(-10, 20).Select(x => (decimal)x).ToArray();
        var decileCalculator = new DecileHandler(value, values);

        var result = decileCalculator.FindDecile();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1500.0, 1)]
    [InlineData(1976.35, 1)]
    [InlineData(1976.36, 2)]
    [InlineData(2003.76, 2)]
    [InlineData(2003.77, 3)]
    [InlineData(2067.92, 3)]
    [InlineData(2067.93, 4)]
    [InlineData(2088.66, 4)]
    [InlineData(2088.67, 5)]
    [InlineData(2112.78, 5)]
    [InlineData(2112.79, 6)]
    [InlineData(2143.09, 6)]
    [InlineData(2143.10, 7)]
    [InlineData(2166.73, 7)]
    [InlineData(2166.74, 8)]
    [InlineData(2195.20, 8)]
    [InlineData(2195.21, 9)]
    [InlineData(2218.46, 9)]
    [InlineData(2218.47, 10)]
    [InlineData(2225.12, 10)]
    [InlineData(2500, 10)]
    public void FindsDecileCorrectlyWithValues(decimal value, int expected)
    {
        var values = new decimal[]
        {
            2195.20m,
            2121.21m,
            2001.67m,
            2067.92m,
            2199.34m,
            2218.46m,
            2143.09m,
            2090.88m,
            2166.73m,
            1976.35m,
            2054.23m,
            2088.66m,
            2112.78m,
            2187.91m,
            2225.12m,
            2154.56m,
            1975.43m,
            2022.89m,
            2003.76m,
            2220.37m,
            2190.45m,
            2078.34m,
        };
        var decileCalculator = new DecileHandler(value, values);

        var result = decileCalculator.FindDecile();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2000, 1)]
    [InlineData(3131.02, 1)]
    [InlineData(3132.03, 2)]
    [InlineData(3169.48, 2)]
    [InlineData(3169.49, 3)]
    [InlineData(3299.83, 3)]
    [InlineData(3299.84, 4)]
    [InlineData(3367.19, 4)]
    [InlineData(3367.20, 5)]
    [InlineData(3488.97, 5)]
    [InlineData(3488.98, 6)]
    [InlineData(3571.52, 6)]
    [InlineData(3571.53, 7)]
    [InlineData(3701.28, 7)]
    [InlineData(3701.29, 8)]
    [InlineData(3812.05, 8)]
    [InlineData(3812.06, 9)]
    [InlineData(3951.14, 9)]
    [InlineData(3951.15, 10)]
    [InlineData(3974.75, 10)]
    [InlineData(5000, 10)]
    public void FindsDecileCorrectlyWithDifferentValues(decimal value, int expected)
    {
        var values = new decimal[]
        {
            3169.48m,
            3276.75m,
            3343.99m,
            3478.12m,
            3571.52m,
            3619.73m,
            3701.28m,
            3812.05m,
            3951.14m,
            3974.75m,
            3076.64m,
            3131.02m,
            3299.83m,
            3367.19m,
            3488.97m,
            3556.67m,
            3650.88m,
            3715.69m,
            3872.34m,
            3960.00m,
            3025.44m,
            3142.75m,
            3258.13m,
            3390.85m,
            3942.16m,
        };
        var decileCalculator = new DecileHandler(value, values);

        var result = decileCalculator.FindDecile();

        Assert.Equal(expected, result);
    }


    [Theory]
    [InlineData(0, true, 1)]
    [InlineData(0, false, 1)]
    [InlineData(1, true, 3)]
    [InlineData(1, false, 1)]
    [InlineData(2, true, 4)]
    [InlineData(2, false, 4)]
    [InlineData(3, true, 5)]
    [InlineData(3, false, 5)]
    [InlineData(4, true, 6)]
    [InlineData(4, false, 6)]
    [InlineData(5, true, 7)]
    [InlineData(5, false, 7)]
    [InlineData(6, true, 8)]
    [InlineData(6, false, 8)]
    [InlineData(7, true, 9)]
    [InlineData(7, false, 9)]
    [InlineData(8, true, 10)]
    [InlineData(8, false, 10)]
    [InlineData(9, true, 10)]
    [InlineData(9, false, 10)]
    public void FindsDecileCorrectlyWithSearchParam(int value, bool searchTopDown, int expected)
    {
        var decileCalculator = new DecileHandler(value, [1, 1, 1, 2, 3, 4, 5, 6, 7, 8], searchTopDown);

        var result = decileCalculator.FindDecile();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, true, 1)]
    [InlineData(0, false, 1)]
    [InlineData(1, true, 1)]
    [InlineData(1, false, 1)]
    [InlineData(2, true, 2)]
    [InlineData(2, false, 2)]
    [InlineData(3, true, 3)]
    [InlineData(3, false, 3)]
    [InlineData(4, true, 6)]
    [InlineData(4, false, 4)]
    [InlineData(5, true, 7)]
    [InlineData(5, false, 7)]
    [InlineData(6, true, 8)]
    [InlineData(6, false, 8)]
    [InlineData(7, true, 9)]
    [InlineData(7, false, 9)]
    [InlineData(8, true, 10)]
    [InlineData(8, false, 10)]
    [InlineData(9, true, 10)]
    [InlineData(9, false, 10)]
    public void FindsDecileCorrectlyWithSearchParamDifferentValues(int value, bool searchTopDown, int expected)
    {
        var decileCalculator = new DecileHandler(value, [1, 2, 3, 4, 4, 4, 5, 6, 7, 8], searchTopDown);

        var result = decileCalculator.FindDecile();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void DecileHandlerThrowsWithLessThanTenValues()
    {
        Assert.Throws<ArgumentException>(() => new DecileHandler(1, [1, 1, 1, 1, 1, 1, 1, 1, 1]));
    }
}