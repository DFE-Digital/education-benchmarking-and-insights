using Microsoft.ApplicationInsights.DataContracts;
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

public class GivenALocalAuthorityRequestTelemetryAttribute
{
    private readonly IServiceCollection _services = new ServiceCollection();

    public GivenALocalAuthorityRequestTelemetryAttribute()
    {
        _services.AddScoped<ILogger<RequestTelemetryFilter>, NullLogger<RequestTelemetryFilter>>();
    }

    [Theory]
    [InlineData(TrackedRequestFeature.Home, "123456")]
    [InlineData(TrackedRequestFeature.Home, null)]
    public async Task SetsPropertiesInRequestTelemetry(TrackedRequestFeature feature, string? code)
    {
        var routeData = new RouteData(new RouteValueDictionary
        {
            {
                "code", code
            }
        });

        var telemetry = await GetTelemetryFromRequest(feature, routeData);

        Assert.Equal("local-authority", telemetry.Properties["Establishment"]);
        Assert.Equal(feature.GetStringValue(), telemetry.Properties["Feature"]);
        Assert.Equal(code, telemetry.Properties["Code"]);
    }

    private async Task<RequestTelemetry> GetTelemetryFromRequest(TrackedRequestFeature feature, RouteData routeData)
    {
        var provider = _services.BuildServiceProvider();
        var httpContext = new DefaultHttpContext
        {
            RequestServices = provider
        };

        var telemetry = new RequestTelemetry();
        httpContext.Features.Set(telemetry);

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

        var attribute = new LocalAuthorityRequestTelemetryAttribute(feature);
        var filter = new RequestTelemetryFilter(
            provider.GetService<ILogger<RequestTelemetryFilter>>()!,
            attribute.Properties,
            attribute.RoutePropertyNames);
        filter.OnActionExecuted(actionExecutedContext);

        await (actionExecutedContext.Result?.ExecuteResultAsync(actionContext) ?? Task.CompletedTask);
        return telemetry;
    }

    private class FakeController : Controller;
}