using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Validators;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets.Validators;

public class WhenComparatorSetUserDefinedSchoolValidatorValidates
{
    private readonly ComparatorSetUserDefinedSchoolValidator _validator = new();

    [Theory]
    [InlineData("urn", "urn", "urn2", "urn3")]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(string urn, params string[] set)
    {
        var instance = new ComparatorSetUserDefinedSchool
        {
            URN = urn,
            Set = ComparatorSetIds.FromCollection(set)
        };

        var actual = await _validator.ValidateAsync(instance);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("urn", "", "urn2", "urn3")]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(string urn, params string[] set)
    {
        var instance = new ComparatorSetUserDefinedSchool
        {
            URN = urn,
            Set = ComparatorSetIds.FromCollection(set)
        };

        var actual = await _validator.ValidateAsync(instance);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}