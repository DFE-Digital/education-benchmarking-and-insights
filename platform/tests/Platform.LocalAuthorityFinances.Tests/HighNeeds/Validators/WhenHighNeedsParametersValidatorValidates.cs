﻿using System.Collections.Specialized;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Validators;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.HighNeeds.Validators;

public class WhenHighNeedsParametersValidatorValidates
{
    private readonly HighNeedsParametersValidator _validator = new();

    [Theory]
    [InlineData("code1")]
    [InlineData("code1,code2,code3")]
    [InlineData("code1,code2,code3,code4,code5,code6,code7,code9,code9,code10")]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string codes)
    {
        var parameters = new HighNeedsParameters();
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
        var parameters = new HighNeedsParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "code", codes }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}