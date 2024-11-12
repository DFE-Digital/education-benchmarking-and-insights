using Platform.Api.Insight.Income;
using Platform.Api.Insight.Validators;
using Xunit;
namespace Platform.Tests.Insight.Validators;

public class WhenIncomeParametersValidatorValidates
{
    private readonly IncomeParametersValidator _validator = new();

    [Theory]
    [InlineData(IncomeCategories.GrantFunding, IncomeDimensions.PercentIncome, false)]
    [InlineData(null, IncomeDimensions.PercentIncome, false)]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string? category, string dimension, bool excludeCentralServices)
    {
        var parameters = new IncomeParameters
        {
            Category = category,
            Dimension = dimension,
            ExcludeCentralServices = excludeCentralServices
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("", "", false)]
    [InlineData("Invalid", IncomeDimensions.Actuals, false)]
    [InlineData(IncomeCategories.GrantFunding, "Invalid", false)]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string? category, string dimension, bool excludeCentralServices)
    {
        var parameters = new IncomeParameters
        {
            Category = category,
            Dimension = dimension,
            ExcludeCentralServices = excludeCentralServices
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}