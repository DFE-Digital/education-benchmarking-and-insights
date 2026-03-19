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
}