using Platform.Api.Insight.Income;
using Platform.Api.Insight.Validators;
using Xunit;

namespace Platform.Tests.Insight.Validators;

public class WhenIncomeParametersValidatorValidates
{
    private readonly IncomeParametersValidator _validator = new();

    [Theory]
    [InlineData(IncomeDimensions.PercentIncome)]
    [InlineData(IncomeDimensions.Actuals)]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string dimension)
    {
        var parameters = new IncomeParameters
        {
            Dimension = dimension,
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Invalid")]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string dimension)
    {
        var parameters = new IncomeParameters
        {
            Dimension = dimension,
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}