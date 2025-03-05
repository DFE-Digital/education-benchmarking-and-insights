using System.Collections.Specialized;
using Platform.Api.Benchmark.Features.UserData.Parameters;
using Platform.Api.Benchmark.Features.UserData.Validators;
using Xunit;

namespace Platform.Benchmark.Tests.UserData.Validators;

public class WhenHighNeedsHistoryParametersValidatorValidates
{
    private readonly UserDataParametersValidator _validator = new();

    [Theory]
    [InlineData("userId")]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string userId)
    {
        var parameters = new UserDataParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "userId", userId }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string? userId)
    {
        var parameters = new UserDataParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "userId", userId }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}