using System.Collections.Specialized;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Validators;
using Xunit;

namespace Platform.NonFinancial.Tests.EducationHealthCarePlans.Validators;

public class WhenEducationHealthCarePlansDimensionedParametersValidatorValidates
{
    private readonly EducationHealthCarePlansDimensionedParametersValidator _validator = new();

    [Theory]
    [InlineData("101", "")]
    [InlineData("101", "Per1000")]
    [InlineData("101,102,103,104,105,106,107,108,109,110", "Per1000")]
    public async Task ShouldBeValidWithGoodParameters(string codes, string dimension)
    {
        var parameters = new EducationHealthCarePlansDimensionedParameters();
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
    [InlineData("", "Per1000")]
    [InlineData("101,102,103,104,105,106,107,108,109,110,111", "Per1000")]
    [InlineData("101", "invalid")]
    public async Task ShouldBeInvalidWithBadParameters(string codes, string dimension)
    {
        var parameters = new EducationHealthCarePlansDimensionedParameters();
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