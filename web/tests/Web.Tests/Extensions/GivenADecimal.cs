using Web.App.Attributes;
using Web.App.Extensions;
using Xunit;

namespace Web.Tests.Extensions;

public class GivenADecimal
{
    [Theory]
    [InlineData(0, "£0")]
    [InlineData(123, "£123")]
    [InlineData(1234, "£1,234")]
    [InlineData(999999, "£999,999")]
    [InlineData(1000000, "£1 million")]
    [InlineData(1000000.99, "£1 million")]
    [InlineData(10000000, "£10 million")]
    [InlineData(100000000, "£100 million")]
    [InlineData(12000000, "£12 million")]
    [InlineData(12355678, "£12.4 million")]
    [InlineData(12340000, "£12.3 million")]
    [InlineData(12300000, "£12.3 million")]
    [InlineData(1235567, "£1.24 million")]
    [InlineData(1234000, "£1.23 million")]
    [InlineData(1200000, "£1.2 million")]
    [InlineData(999999.99, "£1 million")]
    [InlineData(-123, "-£123")]
    [InlineData(-1234, "-£1,234")]
    [InlineData(-999999, "-£999,999")]
    [InlineData(-1000000, "-£1 million")]
    [InlineData(-1000000.99, "-£1 million")]
    [InlineData(-10000000, "-£10 million")]
    [InlineData(-100000000, "-£100 million")]
    [InlineData(-12000000, "-£12 million")]
    [InlineData(-12355678, "-£12.4 million")]
    [InlineData(-12340000, "-£12.3 million")]
    [InlineData(-12300000, "-£12.3 million")]
    [InlineData(-1235567, "-£1.24 million")]
    [InlineData(-1234000, "-£1.23 million")]
    [InlineData(-1200000, "-£1.2 million")]
    [InlineData(-999999.99, "-£1 million")]
    public void WhenToHeadlineStatisticCurrencyIsCalled(decimal actual, string expected)
    {
        var result = actual.ToHeadlineStatisticCurrency();
        Assert.Equal(expected, result);
    }

    public static TheoryData<decimal?, decimal?, decimal?> SafeDivideData => new()
    {
        { 10, 2, 5 },
        { 10, 5, 2 },
        { 123, 1, 123 },
        { 0m, 2, 0 },
        { 123, 0, null },
        { null, 10, null },
        { null, null, null }
    };

    [Theory]
    [MemberData(nameof(SafeDivideData))]
    public void WhenSafeDivideIsCalled(decimal? numerator, decimal? denominator, decimal? expected)
    {
        var result = numerator.SafeDivide(denominator);
        Assert.Equal(expected, result);
    }

    public static TheoryData<decimal?, decimal?, decimal?> SafePercentageData => new()
    {
        { 2, 10, 20 },
        { 5, 10, 50 },
        { 123, 123, 100 },
        { 1, 100, 1 },
        { 0, 2, 0 },
        { 123, 0, null },
        { null, 10, null },
        { null, null, null }
    };

    [Theory]
    [MemberData(nameof(SafePercentageData))]
    public void WhenSafePercentageIsCalled(decimal? value, decimal? total, decimal? expected)
    {
        var result = value.SafePercentageOf(total);
        Assert.Equal(expected, result);
    }
}