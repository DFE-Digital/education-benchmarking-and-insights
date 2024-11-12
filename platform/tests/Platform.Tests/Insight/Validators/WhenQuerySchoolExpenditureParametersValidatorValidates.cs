using Platform.Api.Insight.Domain;
using Platform.Api.Insight.Expenditure;
using Platform.Api.Insight.Validators;
using Xunit;
namespace Platform.Tests.Insight.Validators;

public class WhenQuerySchoolExpenditureParametersValidatorValidates
{
    private readonly QuerySchoolExpenditureParametersValidator _validator = new();

    [Theory]
    [InlineData(new[]
    {
        "urn"
    }, ExpenditureCategories.TotalExpenditure, ExpenditureDimensions.PercentExpenditure, false, null, null, null)]
    [InlineData(new[]
    {
        "urn"
    }, null, ExpenditureDimensions.PercentIncome, false, null, null, null)]
    [InlineData(new string[0], null, ExpenditureDimensions.PercentIncome, false, "12345678", null, OverallPhase.Primary)]
    [InlineData(new string[0], null, ExpenditureDimensions.PercentIncome, false, null, "123", OverallPhase.Primary)]
    [InlineData(new string[0], null, ExpenditureDimensions.PercentIncome, false, null, "123", "Pupil referral unit")]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string[] urns, string? category, string dimension, bool excludeCentralServices, string? companyNumber, string? laCode, string? phase)
    {
        var parameters = new QuerySchoolExpenditureParameters
        {
            Urns = urns,
            Category = category,
            Dimension = dimension,
            ExcludeCentralServices = excludeCentralServices,
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
    }, "", "", false, null, null, null)]
    [InlineData(new[]
    {
        "urn"
    }, "Invalid", ExpenditureDimensions.Actuals, false, null, null, null)]
    [InlineData(new[]
    {
        "urn"
    }, ExpenditureCategories.TotalExpenditure, "Invalid", false, null, null, null)]
    [InlineData(new string[0], null, ExpenditureDimensions.PercentIncome, false, "12345678", null, "Invalid")]
    [InlineData(new string[0], null, ExpenditureDimensions.PercentIncome, false, "12345678", null, null)]
    [InlineData(new string[0], null, ExpenditureDimensions.PercentIncome, false, null, "123", "Invalid")]
    [InlineData(new string[0], null, ExpenditureDimensions.PercentIncome, false, null, "123", null)]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string[] urns, string? category, string dimension, bool excludeCentralServices, string? companyNumber, string? laCode, string? phase)
    {
        var parameters = new QuerySchoolExpenditureParameters
        {
            Urns = urns,
            Category = category,
            Dimension = dimension,
            ExcludeCentralServices = excludeCentralServices,
            CompanyNumber = companyNumber,
            LaCode = laCode,
            Phase = phase
        };

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}