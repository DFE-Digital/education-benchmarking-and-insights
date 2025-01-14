using Platform.Api.Insight.Expenditure;
using Platform.Api.Insight.Validators;
using Xunit;

namespace Platform.Insight.Tests.Validators;

public class WhenExpenditureParametersValidatorValidates
{
    private readonly ExpenditureParametersValidator _validator = new();

    [Theory]
    [InlineData(ExpenditureCategories.TotalExpenditure, ExpenditureDimensions.PercentExpenditure, false)]
    [InlineData(null, ExpenditureDimensions.PercentIncome, false)]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string? category, string dimension, bool excludeCentralServices)
    {
        var parameters = new ExpenditureParameters
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
    [InlineData("Invalid", ExpenditureDimensions.Actuals, false)]
    [InlineData(ExpenditureCategories.TotalExpenditure, "Invalid", false)]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string? category, string dimension, bool excludeCentralServices)
    {
        var parameters = new ExpenditureParameters
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