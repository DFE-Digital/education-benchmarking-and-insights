using System.Collections.Specialized;
using Platform.Api.LocalAuthority.Features.Details.Parameters;
using Platform.Api.LocalAuthority.Features.Details.Validators;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Details.Validators;

public class WhenWorkforceSummaryParametersValidatorValidates
{
    private readonly WorkforceSummaryParametersValidator _validator = new();

    [Fact]
    public async Task ShouldValidateDefaultParametersAsValid()
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection());
        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
    }

    public static TheoryData<string, string, string> InvalidCases => new()
    {
        { "dimension", "invalid", "Dimension must be one of the supported values" },
        { "overallPhase", "invalid", "Overall Phase must be empty or one of the supported values" },
        { "limit", "0", "Limit must be empty or a number between 1 and 100." },
        { "limit", "101", "Limit must be empty or a number between 1 and 100." },
        { "limit", "not_a_number", "Limit must be empty or a number between 1 and 100." },
        { "nurseryProvision", "invalid", "Nursery Provision must be empty or one of the supported values" },
        { "sixthFormProvision", "invalid", "Sixth Form Provision must be empty or one of the supported values" },
        { "specialClassesProvision", "invalid", "Special Classes Provision must be empty or one of the supported values" },
        { "sortField", "invalid", "Sort Field must be one of the supported values" },
        { "sortOrder", "invalid", "Sort Order must be one of the supported values" }
    };

    [Theory]
    [MemberData(nameof(InvalidCases))]
    public async Task ShouldInvalidateBadParameters(string key, string value, string expectedError)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { key, value } });

        var actual = await _validator.ValidateAsync(parameters);

        Assert.False(actual.IsValid);
        Assert.Contains(actual.Errors, e => e.ErrorMessage.Contains(expectedError));
    }
}
