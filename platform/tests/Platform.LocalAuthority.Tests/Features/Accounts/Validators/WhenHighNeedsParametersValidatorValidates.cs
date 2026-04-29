using System.Collections.Specialized;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Api.LocalAuthority.Features.Accounts.Validators;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Accounts.Validators;

public class WhenHighNeedsParametersValidatorValidates
{
    private readonly HighNeedsParametersValidator _validator = new();

    public static TheoryData<string, string> ValidCases => new()
    {
        { "code1", "" },
        { "code1", "PerHead" },
        { "code1", "PerPupil" },
        { "code1,code2,code3", "PerHead" },
        { string.Join(",", Enumerable.Range(1, 30).Select(i => $"code{i}")), "PerHead" }
    };

    public static TheoryData<string, string, string> InvalidCases => new()
    {
        { "", "PerHead", "'Codes' must not be empty" },
        { string.Join(",", Enumerable.Range(1, 31).Select(i => $"code{i}")), "PerHead", "Between 1 and 30 local authority codes must be supplied" },
        { "code1", "invalid", "must be empty or one of the supported values" }
    };

    [Theory]
    [MemberData(nameof(ValidCases))]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string codes, string dimension)
    {
        var parameters = new HighNeedsParameters();
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
    [MemberData(nameof(InvalidCases))]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string codes, string dimension, string expectedMessage)
    {
        var parameters = new HighNeedsParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "code", codes },
            { "dimension", dimension }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
        Assert.Contains(actual.Errors, e => e.ErrorMessage.Contains(expectedMessage));
    }
}