using Platform.Api.Insight.Domain;
using Platform.Api.Insight.MetricRagRatings;
using Platform.Api.Insight.Validators;
using Xunit;
namespace Platform.Tests.Insight.Validators;

public class WhenMetricRagRatingsParametersValidatorValidates
{
    private readonly MetricRagRatingsParametersValidator _validator = new();

    [Theory]
    [InlineData(new[]
    {
        "urn"
    }, new[]
    {
        CostCategory.TeachingStaff
    }, new[]
    {
        RagRating.Red
    }, null, null, null)]
    [InlineData(new[]
    {
        "urn"
    }, new[]
    {
        CostCategory.TeachingStaff
    }, new string[0], null, null, null)]
    [InlineData(new[]
    {
        "urn"
    }, new string[0], new string[0], null, null, null)]
    [InlineData(new string[0], new[]
    {
        CostCategory.TeachingStaff
    }, new[]
    {
        RagRating.Red
    }, "12345678", null, OverallPhase.Primary)]
    [InlineData(new string[0], new[]
    {
        CostCategory.TeachingStaff
    }, new[]
    {
        RagRating.Red
    }, null, "123", OverallPhase.Primary)]
    [InlineData(new string[0], new[]
    {
        CostCategory.TeachingStaff
    }, new[]
    {
        RagRating.Red
    }, null, "123", "Pupil referral unit")]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string[] urns, string[] categories, string[] statuses, string? companyNumber, string? laCode, string? phase)
    {
        var parameters = new MetricRagRatingsParameters
        {
            Urns = urns,
            Categories = categories,
            Statuses = statuses,
            CompanyNumber = companyNumber,
            LaCode = laCode,
            Phase = phase
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData(new[]
    {
        "urn"
    }, new[]
    {
        CostCategory.TeachingStaff
    }, new[]
    {
        "Invalid"
    }, null, null, null)]
    [InlineData(new[]
    {
        "urn"
    }, new[]
    {
        "Invalid"
    }, new[]
    {
        RagRating.Red
    }, null, null, null)]
    [InlineData(new string[0], new[]
    {
        CostCategory.TeachingStaff
    }, new[]
    {
        RagRating.Red
    }, null, null, null)]
    [InlineData(new string[0], new[]
    {
        CostCategory.TeachingStaff
    }, new[]
    {
        RagRating.Red
    }, "companyNumber", null, null)]
    [InlineData(new string[0], new[]
    {
        CostCategory.TeachingStaff
    }, new[]
    {
        RagRating.Red
    }, "companyNumber", null, "Invalid")]
    [InlineData(new string[0], new[]
    {
        CostCategory.TeachingStaff
    }, new[]
    {
        RagRating.Red
    }, null, "laCode", null)]
    [InlineData(new string[0], new[]
    {
        CostCategory.TeachingStaff
    }, new[]
    {
        RagRating.Red
    }, null, "laCode", "Invalid")]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string[] urns, string[] categories, string[] statuses, string? companyNumber, string? laCode, string? phase)
    {
        var parameters = new MetricRagRatingsParameters
        {
            Urns = urns,
            Categories = categories,
            Statuses = statuses,
            CompanyNumber = companyNumber,
            LaCode = laCode,
            Phase = phase
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}