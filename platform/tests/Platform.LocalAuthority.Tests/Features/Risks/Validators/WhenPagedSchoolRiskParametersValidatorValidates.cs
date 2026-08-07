using System.Collections.Specialized;
using Platform.Api.LocalAuthority.Features.Risks.Parameters;
using Platform.Api.LocalAuthority.Features.Risks.Validators;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Risks.Validators;

public class WhenPagedSchoolRisksParametersValidatorValidates
{
    private readonly PagedSchoolRisksParametersValidator _validator = new();

    public static TheoryData<string, int, int, string?, string?, string?> ValidCases => new()
    {
        // codes, page, pageSize, sortBy, sortDir, phase
        { "code1", 1, 10, null, null, null },
        { "code1", 5, 25, "Overall", "Asc", "Nursery" },
        { "code1,code2", 1, 1, "Financial", "Desc", "Primary" },
        { string.Join(",", Enumerable.Range(1, 30).Select(i => $"code{i}")), 1, 10, null, null, null }
    };

    public static TheoryData<string, int, int, string?, string?, string?, string> InvalidCases => new()
    {
        // codes empty
        { "", 1, 10, null, null, null, "'Codes' must not be empty." },

        // too many codes
        { string.Join(",", Enumerable.Range(1, 31).Select(i => $"code{i}")), 1, 10, null, null, null,
            "Between 1 and 30 local authority codes must be supplied" },

        // invalid page
        { "code1", 0, 10, null, null, null, "must be an int >=1" },
        { "code1", 101, 10, null, null, null, "must be an int >=1" },

        // invalid pageSize
        { "code1", 1, 0, null, null, null, "must be an int >=1" },
        { "code1", 1, -5, null, null, null, "must be an int >=1" },

        // invalid sortBy
        { "code1", 1, 10, "invalidField", null, null, "must be empty or one of the supported values" },

        // invalid sortDir
        { "code1", 1, 10, null, "invalidDirection", null, "must be empty or one of the supported values" },

        // invalid phase
        { "code1", 1, 10, null, null, "invalidPhase", "must be empty or one of the supported values" }
        };

    [Theory]
    [MemberData(nameof(ValidCases))]
    public async Task ShouldValidateAndEvaluateGoodParametersAsValid(
        string codes,
        int page,
        int pageSize,
        string? sortField,
        string? sortDir,
        string? phase)
    {
        var parameters = new PagedSchoolRisksParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "code", codes },
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() },
            { "sortField", sortField },
            { "sortDir", sortDir },
            { "phase", phase }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [MemberData(nameof(InvalidCases))]
    public async Task ShouldValidateAndEvaluateBadParametersAsInvalid(
        string codes,
        int page,
        int pageSize,
        string? sortField,
        string? sortOrder,
        string? phase,
        string expectedMessage)
    {
        var parameters = new PagedSchoolRisksParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "code", codes },
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() },
            { "sortField", sortField },
            { "sortOrder", sortOrder },
            { "phase", phase }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
        Assert.Contains(actual.Errors, e => e.ErrorMessage.Contains(expectedMessage));
    }
}
