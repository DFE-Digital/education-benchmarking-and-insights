using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.App.Extensions;
namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class RequestTelemetryAttribute : TypeFilterAttribute
{
    public RequestTelemetryAttribute(TrackedRequests requestName, params string[] routePropertyNames) : base(typeof(RequestTelemetryFilter))
    {
        // arguments list must match the `RequestTelemetryFilter` constructor arguments
        Arguments =
        [
            requestName,
            routePropertyNames
        ];
    }
}

public class RequestTelemetryFilter(
    ILogger<RequestTelemetryFilter> logger,
    TrackedRequests requestName,
    string[] routePropertyNames) : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var telemetry = context.HttpContext.Features.Get<RequestTelemetry>();
        if (telemetry != null && routePropertyNames.Length > 0)
        {
            telemetry.Properties["Name"] = requestName.GetStringValue();

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

        base.OnActionExecuted(context);
    }
}