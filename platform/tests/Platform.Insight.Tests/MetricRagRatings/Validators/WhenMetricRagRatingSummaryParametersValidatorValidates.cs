using System.Collections.Specialized;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Platform.Api.Insight.Features.MetricRagRatings.Validators;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.MetricRagRatings.Validators;

public class WhenMetricRagRatingSummaryParametersValidatorValidates
{
    private readonly MetricRagRatingSummaryParametersValidator _validator = new();

    [Theory]
    [InlineData("123456, 654321", null, null, OverallPhase.Primary, "123456|654321", null, null, OverallPhase.Primary)]
    [InlineData(null, "12345678", null, null, "", "12345678", null, null)]
    [InlineData(null, null, "123", OverallPhase.Secondary, "", null, "123", OverallPhase.Secondary)]
    [InlineData(null, null, "123", OverallPhase.Nursery, "", null, "123", OverallPhase.Nursery)]
    [InlineData(null, null, "123", OverallPhase.Special, "", null, "123", OverallPhase.Special)]
    public async Task ShouldBeValidWithGoodParameters(
        string? urns,
        string? companyNumber,
        string? laCode,
        string? overallPhase,
        string? expectedUrns,
        string? expectedCompanyNumber,
        string? expectedLaCode,
        string? expectedOverallPhase)
    {
        var parameters = new MetricRagRatingSummaryParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "urns", urns },
            { "companyNumber", companyNumber },
            { "laCode", laCode },
            { "overallPhase", overallPhase }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.Equal(expectedUrns, string.Join("|", parameters.Urns));
        Assert.Equal(expectedCompanyNumber, parameters.CompanyNumber);
        Assert.Equal(expectedLaCode, parameters.LaCode);
        Assert.Equal(expectedOverallPhase, parameters.OverallPhase);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData(null, null, "123", "invalid")]
    [InlineData(null, null, "123", "also invalid")]
    public async Task ShouldBeInvalidWithBadParameters(
        string? urns,
        string? companyNumber,
        string? laCode,
        string? overallPhase)
    {
        var parameters = new MetricRagRatingSummaryParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "urns", urns },
            { "companyNumber", companyNumber },
            { "laCode", laCode },
            { "overallPhase", overallPhase }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}