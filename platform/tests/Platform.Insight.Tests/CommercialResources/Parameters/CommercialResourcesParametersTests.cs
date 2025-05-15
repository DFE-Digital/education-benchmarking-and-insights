using System.Collections.Specialized;
using Platform.Api.Insight.Features.CommercialResources.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.Insight.Tests.CommercialResources.Parameters;

public class CommercialResourcesParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "categories", $"{CostCategories.TeachingStaff},{CostCategories.EducationalSupplies}" },
        };

        var parameters = new CommercialResourcesParameters();
        parameters.SetValues(values);

        Assert.Equal([CostCategories.TeachingStaff, CostCategories.EducationalSupplies,], parameters.Categories);
    }
}