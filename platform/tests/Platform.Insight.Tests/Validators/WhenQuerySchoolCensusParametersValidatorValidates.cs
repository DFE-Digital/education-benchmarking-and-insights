using Platform.Api.Insight.Census;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.Validators;

public class WhenQuerySchoolCensusParametersValidatorValidates
{
    private readonly QuerySchoolCensusParametersValidator _validator = new();

    [Theory]
    [InlineData(new[]
    {
        "urn"
    }, CensusCategories.TeachersFte, CensusDimensions.Total, null, null, null)]
    [InlineData(new[]
    {
        "urn"
    }, null, CensusDimensions.Total, null, null, null)]
    [InlineData(new string[0], null, CensusDimensions.Total, "12345678", null, OverallPhase.Primary)]
    [InlineData(new string[0], null, CensusDimensions.Total, null, "123", OverallPhase.Primary)]
    [InlineData(new string[0], null, CensusDimensions.Total, null, "123", "Pupil referral unit")]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string[] urns, string? category, string dimension, string? companyNumber, string? laCode, string? phase)
    {
        var parameters = new QuerySchoolCensusParameters
        {
            Urns = urns,
            Category = category,
            Dimension = dimension,
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
    }, "", "", null, null, null)]
    [InlineData(new[]
    {
        "urn"
    }, "Invalid", CensusDimensions.Total, null, null, null)]
    [InlineData(new[]
    {
        "urn"
    }, CensusCategories.TeachersFte, "Invalid", null, null, null)]
    [InlineData(new string[0], null, CensusDimensions.Total, "12345678", null, "Invalid")]
    [InlineData(new string[0], null, CensusDimensions.Total, "12345678", null, null)]
    [InlineData(new string[0], null, CensusDimensions.Total, null, "123", "Invalid")]
    [InlineData(new string[0], null, CensusDimensions.Total, null, "123", null)]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string[] urns, string? category, string dimension, string? companyNumber, string? laCode, string? phase)
    {
        var parameters = new QuerySchoolCensusParameters
        {
            Urns = urns,
            Category = category,
            Dimension = dimension,
            CompanyNumber = companyNumber,
            LaCode = laCode,
            Phase = phase
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}