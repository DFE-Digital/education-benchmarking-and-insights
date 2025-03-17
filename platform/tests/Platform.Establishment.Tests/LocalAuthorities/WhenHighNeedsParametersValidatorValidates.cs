using System.Collections.Specialized;
using Platform.Api.Establishment.Features.LocalAuthorities.Parameters;
using Platform.Api.Establishment.Features.LocalAuthorities.Validators;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities;

public class WhenLocalAuthoritiesNationalRankParametersValidatorValidates
{
    private readonly LocalAuthoritiesNationalRankParametersValidator _validator = new();

    [Theory]
    [InlineData("SpendAsPercentageOfBudget", "asc")]
    [InlineData("SpendAsPercentageOfBudget", "")]
    [InlineData("", "")]
    [InlineData("", null)]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string ranking, string? sort)
    {
        var parameters = new LocalAuthoritiesNationalRankParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "ranking", ranking },
            { "sort", sort }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("invalid", "asc")]
    [InlineData("SpendAsPercentageOfBudget", "invalid")]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string ranking, string sort)
    {
        var parameters = new LocalAuthoritiesNationalRankParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "ranking", ranking },
            { "sort", sort }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}