using Microsoft.Extensions.Options;
using Web.App.Builders;
using Xunit;
using UriBuilder = Web.App.Builders.UriBuilder;

namespace Web.Tests.Builders;

public class WhenUriBuilder
{
    private const string? GiasBaseUrl = "https://gias/";
    private const string? CompareSchoolPerformanceBaseUrl = "https://csp/";
    private readonly UriBuilder _builder;

    public WhenUriBuilder()
    {
        var options = Options.Create(new UriOptions
        {
            GiasBaseUrl = GiasBaseUrl,
            CompareSchoolPerformanceBaseUrl = CompareSchoolPerformanceBaseUrl
        });
        _builder = new UriBuilder(options);
    }

    [Theory]
    [InlineData("123456", GiasBaseUrl + "establishments/establishment/details/123456")]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void ShouldReturnExpectedUrlWhenGettingGiasSchoolUrl(string? urn, string? expected)
    {
        var actual = _builder.GiasSchoolUrl(urn);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("12345678", GiasBaseUrl + "groups/group/details/12345678")]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void ShouldReturnExpectedUrlWhenGettingGiasTrustUrl(string? uid, string? expected)
    {
        var actual = _builder.GiasTrustUrl(uid);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("123456", CompareSchoolPerformanceBaseUrl + "school/123456")]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void ShouldReturnExpectedUrlWhenGettingCompareSchoolPerformanceUrl(string? urn, string? expected)
    {
        var actual = _builder.CompareSchoolPerformanceUrl(urn);
        Assert.Equal(expected, actual);
    }
}