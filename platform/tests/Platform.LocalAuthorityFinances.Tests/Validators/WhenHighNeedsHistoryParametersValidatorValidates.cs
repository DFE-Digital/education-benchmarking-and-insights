using System.Collections.Specialized;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.Validators;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.Validators;

public class WhenHighNeedsHistoryParametersValidatorValidates
{
    private readonly HighNeedsHistoryParametersValidator _validator = new();

    [Theory]
    [InlineData("code1")]
    [InlineData("code1,code2,code3")]
    [InlineData("code1,code2,code3,code4,code5,code6,code7,code9,code9,code10")]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string codes)
    {
        var parameters = new HighNeedsHistoryParameters();
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
    [InlineData("code1,code2,code3,code4,code5,code6,code7,code9,code9,code10,code11")]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string codes)
    {
        var parameters = new HighNeedsHistoryParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "code", codes }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}