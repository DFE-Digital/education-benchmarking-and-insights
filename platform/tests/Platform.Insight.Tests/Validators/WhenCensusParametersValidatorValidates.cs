using Platform.Api.Insight.Census;
using Xunit;

namespace Platform.Insight.Tests.Validators;

public class WhenCensusParametersValidatorValidates
{
    private readonly CensusParametersValidator _validator = new();

    [Theory]
    [InlineData(CensusCategories.TeachersFte, CensusDimensions.Total)]
    [InlineData(null, CensusDimensions.Total)]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string? category, string dimension)
    {
        var parameters = new CensusParameters
        {
            Category = category,
            Dimension = dimension
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("Invalid", CensusDimensions.Total)]
    [InlineData(CensusCategories.TeachersFte, "Invalid")]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string? category, string dimension)
    {
        var parameters = new CensusParameters
        {
            Category = category,
            Dimension = dimension
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}