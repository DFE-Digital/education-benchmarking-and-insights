using System.Collections.Specialized;
using Platform.Api.Insight.Features.CommercialResources.Parameters;
using Platform.Api.Insight.Features.CommercialResources.Validators;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.CommercialResources.Validators;

public class WhenCommercialResourcesParametersValidatorValidates
{
    private readonly CommercialResourcesParametersValidator _validator = new();

    [Theory]
    [InlineData($"{CostCategories.TeachingStaff}")]
    [InlineData($"{CostCategories.TeachingStaff},{CostCategories.EducationalSupplies},{CostCategories.Other}")]
    [InlineData(null)]
    public async Task ShouldBeValidWithGoodParameters(string? categories)
    {
        var parameters = new CommercialResourcesParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "categories", categories }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.True(actual.IsValid);
        Assert.Empty(actual.Errors);
    }

    [Theory]
    [InlineData("this,should,fail")]
    public async Task ShouldBeInvalidWithBadParameters(string categories)
    {
        var parameters = new CommercialResourcesParameters();
        parameters.SetValues(new NameValueCollection
        {
            { "categories", categories }
        });

        var actual = await _validator.ValidateAsync(parameters);
        Assert.False(actual.IsValid);
        Assert.NotEmpty(actual.Errors);
    }
}