using System.Collections.Specialized;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Validators;
using Platform.Domain;
using Xunit;

namespace Platform.School.Tests.Features.Accounts.Validators;

public class WhenBalanceNationalAvgParametersValidatorValidates
{
    private readonly BalanceNationalAvgParametersValidator _validator = new();

    [Fact]
    public async Task ShouldBeValidWithGoodParameters()
    {
        var parameters = new BalanceNationalAvgParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "dimension", Dimensions.Finance.PercentExpenditure },
            { "financeType", FinanceType.Academy },
            { "phase", OverallPhase.Primary }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("invalid", FinanceType.Academy, OverallPhase.Primary)]
    [InlineData(Dimensions.Finance.PercentExpenditure, "invalid", OverallPhase.Primary)]
    [InlineData(Dimensions.Finance.PercentExpenditure, FinanceType.Academy, "invalid")]
    public async Task ShouldBeInvalidWithBadParameters(string dimension, string financeType, string phase)
    {
        var parameters = new BalanceNationalAvgParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "dimension", dimension },
            { "financeType", financeType },
            { "phase", phase }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}