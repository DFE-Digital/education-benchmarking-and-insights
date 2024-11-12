using Platform.Api.Insight.Income;
using Platform.Api.Insight.Validators;
using Xunit;
namespace Platform.Tests.Insight.Validators;

public class WhenQueryTrustIncomeParametersValidatorValidates
{
    private readonly QueryTrustIncomeParametersValidator _validator = new();

    [Theory]
    [InlineData(new[]
    {
        "companyNumber"
    }, IncomeCategories.GrantFunding, IncomeDimensions.PercentIncome, false)]
    [InlineData(new[]
    {
        "companyNumber"
    }, null, IncomeDimensions.PercentIncome, false)]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string[] companyNumbers, string? category, string dimension, bool excludeCentralServices)
    {
        var parameters = new QueryTrustIncomeParameters
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
    [InlineData(new[]
    {
        "companyNumber"
    }, "", "", false)]
    [InlineData(new[]
    {
        "companyNumber"
    }, "Invalid", IncomeDimensions.Actuals, false)]
    [InlineData(new[]
    {
        "companyNumber"
    }, IncomeCategories.GrantFunding, "Invalid", false)]
    [InlineData(new string[0], IncomeCategories.GrantFunding, IncomeDimensions.PercentIncome, false)]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string[] companyNumbers, string? category, string dimension, bool excludeCentralServices)
    {
        var parameters = new QueryTrustIncomeParameters
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