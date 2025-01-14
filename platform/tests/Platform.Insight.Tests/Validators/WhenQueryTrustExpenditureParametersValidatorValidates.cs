using Platform.Api.Insight.Expenditure;
using Platform.Api.Insight.Validators;
using Xunit;

namespace Platform.Insight.Tests.Validators;

public class WhenQueryTrustExpenditureParametersValidatorValidates
{
    private readonly QueryTrustExpenditureParametersValidator _validator = new();

    [Theory]
    [InlineData(new[] { "companyNumber" }, ExpenditureCategories.TotalExpenditure, ExpenditureDimensions.PercentExpenditure, false)]
    [InlineData(new[] { "companyNumber" }, null, ExpenditureDimensions.PercentIncome, false)]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string[] companyNumbers, string? category, string dimension, bool excludeCentralServices)
    {
        var parameters = new QueryTrustExpenditureParameters
        {
            CompanyNumbers = companyNumbers,
            Category = category,
            Dimension = dimension,
            ExcludeCentralServices = excludeCentralServices
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData(new[] { "companyNumber" }, "", "", false)]
    [InlineData(new[] { "companyNumber" }, "Invalid", ExpenditureDimensions.Actuals, false)]
    [InlineData(new[] { "companyNumber" }, ExpenditureCategories.TotalExpenditure, "Invalid", false)]
    [InlineData(new string[0], ExpenditureCategories.TotalExpenditure, ExpenditureDimensions.PercentExpenditure, false)]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string[] companyNumbers, string? category, string dimension, bool excludeCentralServices)
    {
        var parameters = new QueryTrustExpenditureParameters
        {
            CompanyNumbers = companyNumbers,
            Category = category,
            Dimension = dimension,
            ExcludeCentralServices = excludeCentralServices
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}