using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewModels.Components;

public class GivenALocalAuthoritySchoolWorkforceFormViewModel
{
    [Fact]
    public void ShouldBuildRouteValuesOnClearWithExpectedKeysAndValues()
    {
        // Arrange
        const string code = "123";
        const string formPrefix = "w.";
        const int maxRows = 7;
        const string defaultSort = "test-default-sort";
        const string tabId = "test-id";
        const string sort = "test-sort";
        const string path = "path";
        var query = new QueryCollection();

        var otherFormValues = new Dictionary<string, StringValues>
        {
            { "custom1", "value1" },
            { "custom2", "value2" }
        };

        var model = new LocalAuthoritySchoolWorkforceFormViewModel(
            code,
            formPrefix,
            maxRows,
            defaultSort,
            otherFormValues,
            tabId,
            path,
            query
        )
        {
            Sort = sort,
            FiltersVisible = true,
        };

        // Act
        var routeValues = model.RouteValuesOnClear;

        // Assert
        Assert.Equal(code, routeValues["code"]);
        Assert.Equal("show", routeValues[$"{formPrefix}filter"]);
        Assert.Equal(0, routeValues[$"{formPrefix}as"]);
        Assert.Equal(sort, routeValues[$"{formPrefix}sort"]);
        Assert.Equal("value1", routeValues["custom1"]?.ToString());
        Assert.Equal("value2", routeValues["custom2"]?.ToString());
    }
}