using System.Collections.Specialized;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Platform.Api.Insight.Features.ItSpend.Validators;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.ItSpend.Validators;

public class WhenItSpendSchoolsParametersValidatorValidates
{
    private readonly ItSpendSchoolsParametersValidator _validator = new();

    [Theory]
    [InlineData(Dimensions.Finance.Actuals)]
    [InlineData(Dimensions.Finance.PerUnit)]
    [InlineData(Dimensions.Finance.PercentExpenditure)]
    [InlineData(Dimensions.Finance.PercentIncome)]
    public async Task ShouldBeValidWithGoodParameters(string dimension)
    {
        var parameters = new ItSpendSchoolsParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "dimension", dimension }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData("alsoInvalid")]
    public async Task ShouldBeInvalidWithBadParameters(string dimension)
    {
        var parameters = new ItSpendSchoolsParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "dimension", dimension }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}