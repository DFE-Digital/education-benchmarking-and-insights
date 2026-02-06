using System.Collections.Specialized;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Validators;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.EducationHealthCarePlans.Validators;

public class WhenEducationHealthCarePlansParametersValidatorValidates
{
    private readonly EducationHealthCarePlansParametersValidator _validator = new();

    public static TheoryData<string, string> ValidCases => new()
    {
        { "code1", "" },
        { "code1", "Per1000" },
        { "code1", "Per1000Pupil" },
        { "code1,code2,code3", "Per1000" },
        { string.Join(",", Enumerable.Range(1, 30).Select(i => $"code{i}")), "Per1000" }
    };

    public static TheoryData<string, string> InvalidCases => new()
    {
        { "", "Per1000" },
        { string.Join(",", Enumerable.Range(1, 31).Select(i => $"code{i}")), "Per1000Pupil" },
        { "code1", "invalid" }
    };

    [Theory]
    [MemberData(nameof(ValidCases))]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string codes, string dimension)
    {
        var parameters = new EducationHealthCarePlansParameters();
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
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string codes, string dimension)
    {
        var parameters = new EducationHealthCarePlansParameters();
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