using System.Collections.Specialized;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Platform.Api.Insight.Features.ItSpend.Validators;
using Xunit;

namespace Platform.Insight.Tests.ItSpend.Validators;

public class WhenItSpendTrustForecastParametersValidatorValidates
{
    private readonly ItSpendTrustForecastParametersValidator _validator = new();

    [Fact]
    public async Task ShouldBeValidWithGoodParameters()
    {
        var parameters = new ItSpendTrustForecastParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "companyNumber", "12345678" },
            { "year", "2025" }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("12345678", "")]
    [InlineData("", "2025")]
    public async Task ShouldBeInvalidWithBadParameters(string companyNumber, string year)
    {
        var parameters = new ItSpendTrustForecastParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "companyNumber", companyNumber },
            { "year", year }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}