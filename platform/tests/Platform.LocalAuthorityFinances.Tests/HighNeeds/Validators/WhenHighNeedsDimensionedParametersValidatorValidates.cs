using System.Collections.Specialized;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Validators;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.HighNeeds.Validators;

public class WhenHighNeedsDimensionedParametersValidatorValidates
{
    private readonly HighNeedsDimensionedParametersValidator _validator = new();

    [Theory]
    [InlineData("code1", "")]
    [InlineData("code1", "PerHead")]
    [InlineData("code1,code2,code3", "PerHead")]
    [InlineData("code1,code2,code3,code4,code5,code6,code7,code9,code9,code10", "PerHead")]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string codes, string dimension)
    {
        var parameters = new HighNeedsDimensionedParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "code", codes },
            { "dimension", dimension }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("", "PerHead")]
    [InlineData("code1,code2,code3,code4,code5,code6,code7,code9,code9,code10,code11", "PerHead")]
    [InlineData("code1", "invalid")]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string codes, string dimension)
    {
        var parameters = new HighNeedsDimensionedParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "code", codes },
            { "dimension", dimension }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}