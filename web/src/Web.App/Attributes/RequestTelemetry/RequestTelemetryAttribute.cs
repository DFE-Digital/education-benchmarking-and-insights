using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.App.Extensions;
namespace Web.App.Attributes.RequestTelemetry;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public abstract class RequestTelemetryAttribute : TypeFilterAttribute
{
    protected RequestTelemetryAttribute(
        Dictionary<string, object?> properties,
        params string[] routePropertyNames) : base(typeof(RequestTelemetryFilter))
    {
        // arguments list must match the `RequestTelemetryFilter` constructor arguments
        Arguments =
        [
            properties,
            routePropertyNames
        ];
    }
}

internal class RequestTelemetryFilter(
    ILogger<RequestTelemetryFilter> logger,
    Dictionary<string, object?> properties,
    string[] routePropertyNames) : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);

        var telemetry = context.HttpContext.Features.Get<Microsoft.ApplicationInsights.DataContracts.RequestTelemetry>();
        if (telemetry != null)
        {
            foreach (var property in properties.Keys)
            {
                var value = properties[property];
                telemetry.Properties[property.Capitalise()] = value?.ToString();
            }

            foreach (var property in routePropertyNames)
            {
                var value = context.RouteData.Values[property];
                telemetry.Properties[property.Capitalise()] = value?.ToString();
            }

            logger.LogDebug("Updated telemetry properties to {Properties} for {Method} {URL}",
                telemetry.Properties,
                context.HttpContext.Request.Method,
                context.HttpContext.Request.GetDisplayUrl());
        }
    }
}