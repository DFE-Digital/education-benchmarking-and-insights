using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Moq;
using Web.App.Controllers;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Apis.LocalAuthorityFinances;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Controllers;

[SuppressMessage("Usage", "xUnit1045:Avoid using TheoryData type arguments that might not be serializable")]
public class WhenLocalAuthorityControllerPosts
{
    private readonly LocalAuthorityController _controller;
    private readonly NullLogger<LocalAuthorityController> _logger = new();
    private readonly Mock<IUrlHelper> _mockUrlHelper = new();

    public WhenLocalAuthorityControllerPosts()
    {
        string? actualController = null;
        string? actualAction = null;
        string? actualFragment = null;
        RouteValueDictionary? actualValues = null;
        _mockUrlHelper
            .Setup(u => u.Action(It.IsAny<UrlActionContext>()))
            .Callback<UrlActionContext>(u =>
            {
                actualController = u.Controller;
                actualAction = u.Action;
                actualFragment = u.Fragment;
                actualValues = u.Values as RouteValueDictionary;
            })
            .Returns(() =>
            {
                var routeValuesAsQuery = new StringBuilder();
                if (actualValues != null)
                {
                    foreach (var kvp in actualValues)
                    {
                        switch (kvp.Value)
                        {
                            case string:
                                routeValuesAsQuery.Append($"{kvp.Key}={kvp.Value}&");
                                break;
                            case StringValues values:
                                {
                                    foreach (var value in values)
                                    {
                                        routeValuesAsQuery.Append($"{kvp.Key}={value}&");
                                    }

                                    break;
                                }
                        }
                    }
                }

                return $"{actualController}/{actualAction}?{routeValuesAsQuery.ToString().TrimEnd('&')}{actualFragment}";
            });

        _controller = new LocalAuthorityController(_logger, Mock.Of<IEstablishmentApi>(), Mock.Of<IMetricRagRatingApi>(), Mock.Of<ICommercialResourcesService>(), Mock.Of<ILocalAuthorityFinancesApi>())
        {
            Url = _mockUrlHelper.Object
        };
    }

    public static TheoryData<
        string,
        List<KeyValuePair<string, string[]>>,
        List<KeyValuePair<string, StringValues>>,
        string?> RedirectTestData => new()
    {
        { "123", [], [new KeyValuePair<string, StringValues>("code", "123")], null },
        {
            "123", [new KeyValuePair<string, string[]>("key", ["value"])], [
                new KeyValuePair<string, StringValues>("code", "123"), new KeyValuePair<string, StringValues>("key", new[]
                {
                    "value"
                })
            ],
            null
        },
        {
            "123", [new KeyValuePair<string, string[]>("key", ["value1", "value2"])], [
                new KeyValuePair<string, StringValues>("code", "123"), new KeyValuePair<string, StringValues>("key", new[]
                {
                    "value1",
                    "value2"
                })
            ],
            null
        },
        { "123", [new KeyValuePair<string, string[]>("__ignore", ["value"])], [new KeyValuePair<string, StringValues>("code", "123")], null },
        { "123", [new KeyValuePair<string, string[]>("xxx.sort", ["value1", "value2"])], [new KeyValuePair<string, StringValues>("code", "123"), new KeyValuePair<string, StringValues>("xxx.sort", "value2")], null },
        { "123", [new KeyValuePair<string, string[]>("xxx.filter", ["value1", "value2"])], [new KeyValuePair<string, StringValues>("code", "123"), new KeyValuePair<string, StringValues>("xxx.filter", "value2")], null },
        {
            "123", [new KeyValuePair<string, string[]>("xxx.keep", ["value1"]), new KeyValuePair<string, string[]>("xxx.drop", ["value2"]), new KeyValuePair<string, string[]>("__resetFields", ["xxx.drop"])], [
                new KeyValuePair<string, StringValues>("code", "123"), new KeyValuePair<string, StringValues>("xxx.keep", new[]
                {
                    "value1"
                })
            ],
            null
        },
        { "123", [new KeyValuePair<string, string[]>("__fragment", ["tab-id"])], [new KeyValuePair<string, StringValues>("code", "123")], "#tab-id" }
    };

    [Theory]
    [MemberData(nameof(RedirectTestData))]
    public void ShouldRedirectToIndexActionWithRouteValues(string code, List<KeyValuePair<string, string[]>> formFields, List<KeyValuePair<string, StringValues>> expectedRouteValues, string? expectedFragment)
    {
        var form = new FormCollection(formFields.ToDictionary(k => k.Key, v => new StringValues(v.Value)));
        var routeValuesAsQuery = QueryString.Create(expectedRouteValues);

        var result = _controller.Index(code, form);

        var redirect = Assert.IsType<RedirectResult>(result);
        Assert.Equal($"/Index{routeValuesAsQuery}{expectedFragment}", redirect.Url);
    }
}