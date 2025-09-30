using System.Collections.Specialized;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Platform.Api.Insight.Features.ItSpend.Validators;
using Xunit;

namespace Platform.Insight.Tests.ItSpend.Validators;

public class WhenItSpendTrustsParametersValidatorValidates
{
    private readonly ItSpendTrustsParametersValidator _validator = new();

    [Fact]
    public async Task ShouldBeValidWithGoodParameters()
    {
        var parameters = new ItSpendTrustsParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "companyNumbers", "12345678" },
            { "companyNumbers", "87654321" }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Fact]
    public async Task ShouldBeInvalidWithBadParameters()
    {
        var parameters = new ItSpendTrustsParameters();
        parameters.SetValues(new NameValueCollection());

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}