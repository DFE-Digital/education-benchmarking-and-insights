using System.Collections.Specialized;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Validators;
using Platform.Domain;
using Xunit;

namespace Platform.School.Tests.Features.Accounts.Validators;

public class WhenBalanceParametersValidatorValidates
{
    private readonly BalanceParametersValidator _validator = new();

    [Theory]
    [InlineData(Dimensions.Finance.Actuals)]
    [InlineData(Dimensions.Finance.PercentExpenditure)]
    [InlineData(Dimensions.Finance.PercentIncome)]
    [InlineData(Dimensions.Finance.PerUnit)]
    public async Task ShouldBeValidWithGoodParameters(string dimension)
    {
        var parameters = new BalanceParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "dimension", dimension },
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Fact]
    public async Task ShouldBeInvalidWithBadParameters()
    {
        var parameters = new BalanceParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "dimension", "invalid" }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}