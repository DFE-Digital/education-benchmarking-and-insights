using Web.App;
using Xunit;

namespace Web.Tests;

public class WhenConstants
{
    [Fact]
    public void ShouldReturnExpectedAvailableYears()
    {
        var currentYear = DateTime.UtcNow.Year;
        if (DateTime.UtcNow.Month < 9)
        {
            currentYear -= 1;
        }

        var expected = Enumerable.Range(currentYear, 4).ToArray();
        var actual = Constants.AvailableYears;

        Assert.Equal(expected, actual);
    }
}