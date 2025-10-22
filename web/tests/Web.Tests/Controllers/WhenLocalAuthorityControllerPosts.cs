using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Moq;
using Web.App.Controllers;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Controllers;

[SuppressMessage("Usage", "xUnit1045:Avoid using TheoryData type arguments that might not be serializable")]
public class WhenLocalAuthorityControllerPosts
{
    private readonly LocalAuthorityController _api;
    private readonly Mock<ICommercialResourcesService> _commercialResourcesService = new();
    private readonly Mock<IEstablishmentApi> _establishmentApi = new();
    private readonly NullLogger<LocalAuthorityController> _logger = new();
    private readonly Mock<IMetricRagRatingApi> _metricRagRatingApi = new();

    public WhenLocalAuthorityControllerPosts()
    {
        _api = new LocalAuthorityController(_logger, _establishmentApi.Object, _metricRagRatingApi.Object, _commercialResourcesService.Object);
    }

    public static TheoryData<
        string,
        List<KeyValuePair<string, string[]>>,
        List<KeyValuePair<string, object>>> RedirectTestData => new()
    {
        { "123", [], [new KeyValuePair<string, object>("code", "123")] },
        {
            "123", [new KeyValuePair<string, string[]>("key", ["value"])], [
                new KeyValuePair<string, object>("code", "123"), new KeyValuePair<string, object>("key", new[]
                {
                    "value"
                })
            ]
        },
        {
            "123", [new KeyValuePair<string, string[]>("key", ["value1", "value2"])], [
                new KeyValuePair<string, object>("code", "123"), new KeyValuePair<string, object>("key", new[]
                {
                    "value1",
                    "value2"
                })
            ]
        },
        { "123", [new KeyValuePair<string, string[]>("__ignore", ["value"])], [new KeyValuePair<string, object>("code", "123")] },
        { "123", [new KeyValuePair<string, string[]>("xxx.sort", ["value1", "value2"])], [new KeyValuePair<string, object>("code", "123"), new KeyValuePair<string, object>("xxx.sort", "value2")] },
        { "123", [new KeyValuePair<string, string[]>("xxx.filter", ["value1", "value2"])], [new KeyValuePair<string, object>("code", "123"), new KeyValuePair<string, object>("xxx.filter", "value2")] },
        {
            "123", [new KeyValuePair<string, string[]>("xxx.keep", ["value1"]), new KeyValuePair<string, string[]>("xxx.drop", ["value2"]), new KeyValuePair<string, string[]>("__resetFields", ["xxx.drop"])], [
                new KeyValuePair<string, object>("code", "123"), new KeyValuePair<string, object>("xxx.keep", new[]
                {
                    "value1"
                })
            ]
        }
    };

    [Theory]
    [MemberData(nameof(RedirectTestData))]
    public void ShouldRedirectToIndexActionWithRouteValues(string code, List<KeyValuePair<string, string[]>> formFields, List<KeyValuePair<string, object>> expectedRouteValues)
    {
        var form = new FormCollection(formFields.ToDictionary(k => k.Key, v => new StringValues(v.Value)));
        var route = RouteValueDictionary.FromArray(expectedRouteValues.Select(k => new KeyValuePair<string, object>(k.Key, k.Value)).ToArray()!);

        var result = _api.Index(code, form);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(route, redirect.RouteValues);
    }
}