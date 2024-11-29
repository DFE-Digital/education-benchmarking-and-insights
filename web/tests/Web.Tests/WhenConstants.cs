using Web.App;
using Xunit;
namespace Web.Tests;

public class WhenConstants
{
    [Theory]
    [InlineData("123456", "https://www.get-information-schools.service.gov.uk/establishments/establishment/details/123456")]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void ShouldReturnExpectedUrlWhenGettingGiasSchoolUrl(string? urn, string? expected)
    {
        var actual = Constants.GiasSchoolUrl(urn);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("123456", "https://www.get-information-schools.service.gov.uk/groups/group/details/123456")]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void ShouldReturnExpectedUrlWhenGettingGiasTrustUrl(string? uid, string? expected)
    {
        var actual = Constants.GiasTrustUrl(uid);
        Assert.Equal(expected, actual);
    }

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