using System.Collections.Specialized;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Validators;
using Platform.Domain;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.Schools.Validators;

public class WhenWorkforceSummaryParametersValidatorValidates
{
    private readonly WorkforceSummaryParametersValidator _validator = new();

    [Theory]
    [InlineData(Dimensions.Workforce.Actuals)]
    [InlineData(Dimensions.Workforce.PercentPupil)]
    public async Task ShouldBeValidWithValidDimension(string dimension)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "dimension", dimension } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid")]
    public async Task ShouldBeInvalidWithInvalidDimension(string dimension)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "dimension", dimension } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Primary")]
    [InlineData("Secondary")]
    [InlineData("Primary, Secondary")]
    public async Task ShouldBeValidWithValidOverallPhase(string overallPhase)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "overallPhase", overallPhase } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("Unknown")]
    [InlineData("123")]
    [InlineData("Unknown, 123")]
    public async Task ShouldBeInvalidWithInvalidOverallPhase(string overallPhase)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "overallPhase", overallPhase } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("100")]
    public async Task ShouldBeValidWithValidLimit(string? limit)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "limit", limit } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("101")]
    [InlineData("abc")]
    public async Task ShouldBeInvalidWithInvalidLimit(string limit)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "limit", limit } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("Has Nursery Classes")]
    [InlineData("No Nursery Classes")]
    [InlineData("Not applicable")]
    [InlineData("Not recorded")]
    [InlineData("Not applicable, Not recorded")]
    public async Task ShouldBeValidWithValidNurseryProvision(string provision)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "nurseryProvision", provision } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ShouldBeInvalidWithInvalidNurseryProvision()
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "nurseryProvision", "InvalidValue" } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("Has a sixth form")]
    [InlineData("Does not have a sixth form")]
    [InlineData("Not applicable")]
    [InlineData("Not recorded")]
    [InlineData("Not applicable, Not recorded")]
    public async Task ShouldBeValidWithValidSixthFormProvision(string provision)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "sixthFormProvision", provision } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ShouldBeInvalidWithInvalidSixthFormProvision()
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "sixthFormProvision", "InvalidValue" } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("Has Special Classes")]
    [InlineData("No Special Classes")]
    [InlineData("Not applicable")]
    [InlineData("Not recorded")]
    [InlineData("Not applicable, Not recorded")]
    public async Task ShouldBeValidWithValidSpecialClassesProvision(string provision)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "specialClassesProvision", provision } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ShouldBeInvalidWithInvalidSpecialClassesProvision()
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "specialClassesProvision", "InvalidValue" } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("SchoolName")]
    [InlineData("PupilTeacherRatio")]
    [InlineData("TotalPupils")]
    public async Task ShouldBeValidWithValidSortField(string sortField)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "sortField", sortField } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("InvalidField")]
    [InlineData("")]
    public async Task ShouldBeInvalidWithInvalidSortField(string sortField)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "sortField", sortField } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("asc")]
    [InlineData("desc")]
    [InlineData("ASC")]
    [InlineData("DESC")]
    public async Task ShouldBeValidWithValidSortOrder(string sortOrder)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "sortOrder", sortOrder } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("Invalid")]
    [InlineData("ascending")]
    [InlineData("descending")]
    public async Task ShouldBeInvalidWithInvalidSortOrder(string sortOrder)
    {
        var parameters = new WorkforceSummaryParameters();
        parameters.SetValues(new NameValueCollection { { "sortOrder", sortOrder } });

        var result = await _validator.ValidateAsync(parameters);
        Assert.False(result.IsValid);
    }
}