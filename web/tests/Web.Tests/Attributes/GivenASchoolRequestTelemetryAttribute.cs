using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Web.App;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Extensions;
using Xunit;

namespace Web.Tests.Attributes;

public class GivenASchoolRequestTelemetryAttribute
{
    private readonly IServiceCollection _services = new ServiceCollection();

    public GivenASchoolRequestTelemetryAttribute()
    {
        _services.AddScoped<ILogger<RequestTelemetryFilter>, NullLogger<RequestTelemetryFilter>>();
    }

    [Theory]
    [InlineData(TrackedRequestFeature.BenchmarkWorkforce, "123456")]
    [InlineData(TrackedRequestFeature.BenchmarkWorkforce, null)]
    public async Task SetsPropertiesInRequestTelemetry(TrackedRequestFeature feature, string? urn)
    {
        var routeData = new RouteData(new RouteValueDictionary
        {
            { "urn", urn }
        });

        var telemetry = await GetTelemetryFromRequest(feature, routeData);

        Assert.Equal("school", telemetry["Establishment"]);
        Assert.Equal(feature.GetStringValue(), telemetry["Feature"]);
        Assert.Equal(urn, telemetry["Urn"]);
    }

    private async Task<Dictionary<string, string?>> GetTelemetryFromRequest(TrackedRequestFeature feature, RouteData routeData)
    {
        var provider = _services.BuildServiceProvider();
        var httpContext = new DefaultHttpContext
        {
            RequestServices = provider
        };

        using var activity = new Activity("TestActivity");
        activity.Start();

        var actionContext = new ActionContext
        {
            HttpContext = httpContext,
            RouteData = routeData,
            ActionDescriptor = new ControllerActionDescriptor()
        };

        var actionExecutedContext = new ActionExecutedContext(
            actionContext,
            new List<IFilterMetadata>(),
            new FakeController
            {
                ControllerContext = new ControllerContext(actionContext)
            }
        );

        var attribute = new SchoolRequestTelemetryAttribute(feature);
        var filter = new RequestTelemetryFilter(
            provider.GetService<ILogger<RequestTelemetryFilter>>()!,
            attribute.Properties,
            attribute.RoutePropertyNames,
            attribute.ContextProperties);
        filter.OnActionExecuted(actionExecutedContext);

        await (actionExecutedContext.Result?.ExecuteResultAsync(actionContext) ?? Task.CompletedTask);
        activity.Stop();
        return activity.Tags.ToDictionary();
    }

    private class FakeController : Controller;
}