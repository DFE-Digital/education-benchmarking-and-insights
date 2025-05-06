using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Validators;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets.Validators;

public class WhenComparatorSetUserDefinedTrustValidatorValidates
{
    private readonly ComparatorSetUserDefinedTrustValidator _validator = new();

    [Theory]
    [InlineData("companyNumber", "companyNumber", "companyNumber2", "companyNumber3")]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string companyNumber, params string[] set)
    {
        var instance = new ComparatorSetUserDefinedTrust
        {
            CompanyNumber = companyNumber,
            Set = ComparatorSetIds.FromCollection(set)
        };

        var actual = await _validator.ValidateAsync(instance);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("companyNumber", "", "companyNumber2", "companyNumber3")]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string companyNumber, params string[] set)
    {
        var instance = new ComparatorSetUserDefinedTrust
        {
            CompanyNumber = companyNumber,
            Set = ComparatorSetIds.FromCollection(set)
        };

        var actual = await _validator.ValidateAsync(instance);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}