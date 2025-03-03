using System.Collections.Specialized;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Validators;
using Xunit;

namespace Platform.NonFinancial.Tests.EducationHealthCarePlansLocalAuthoritiesHistory.Validators;

public class WhenGivenEducationHealthCarePlansParameters
{
    private readonly EducationHealthCarePlansParametersValidator _validator = new();

    [Theory]
    [InlineData("101")]
    [InlineData("101,102,103,104,105,106,107,108,109,110")]
    public async Task ShouldBeValidWithGoodParameters(string codes)
    {
        var parameters = new EducationHealthCarePlansParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "code", codes }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("101,102,103,104,105,106,107,108,109,110,111")]
    public async Task ShouldBeInvalidWithBadParameters(string codes)
    {
        var parameters = new EducationHealthCarePlansParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "code", codes }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}