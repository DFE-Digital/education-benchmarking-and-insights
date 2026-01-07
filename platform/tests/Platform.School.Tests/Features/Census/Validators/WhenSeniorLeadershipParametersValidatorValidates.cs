using System.Collections.Specialized;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Api.School.Features.Census.Validators;
using Platform.Domain;
using Xunit;

namespace Platform.School.Tests.Features.Census.Validators;

public class WhenItSpendSchoolsParametersValidatorValidates
{
    private readonly SeniorLeadershipParametersValidator _validator = new();

    [Theory]
    [InlineData(Dimensions.Census.Total)]
    [InlineData(Dimensions.Census.PercentWorkforce)]
    public async Task ShouldBeValidWithGoodParameters(string dimension)
    {
        var parameters = new SeniorLeadershipParameters();
        parameters.SetValues(new NameValueCollection
        {
            {"urns", "123456"},
            { "dimension", dimension }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData("alsoInvalid")]
    public async Task ShouldBeInvalidWithBadDimensionParameters(string dimension)
    {
        var parameters = new SeniorLeadershipParameters();
        parameters.SetValues(new NameValueCollection
        {
            {"urns", "123456"},
            { "dimension", dimension }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }

    [Fact]
    public async Task ShouldBeInvalidWithMissingUrnsParameters()
    {
        var parameters = new SeniorLeadershipParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "dimension", Dimensions.Census.Total }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}