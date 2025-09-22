using System.Diagnostics;
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
        Properties = properties;
        RoutePropertyNames = routePropertyNames;

        // arguments list must match the `RequestTelemetryFilter` constructor arguments
        Arguments =
        [
            Properties,
            RoutePropertyNames,
            ContextProperties
        ];
    }

    internal Dictionary<string, object?> Properties { get; }
    internal string[] RoutePropertyNames { get; }
    internal Dictionary<string, Func<HttpContext, object?>> ContextProperties { get; } = new();
}

internal class RequestTelemetryFilter(
    ILogger<RequestTelemetryFilter> logger,
    Dictionary<string, object?> properties,
    string[] routePropertyNames,
    Dictionary<string, Func<HttpContext, object?>> contextProperties) : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);

        var activity = Activity.Current;
        if (activity == null)
        {
            return;
        }

        foreach (var property in properties.Keys)
        {
            var value = properties[property];
            activity.SetTag($"custom.{property.Capitalise()}", value?.ToString());
        }

        foreach (var property in routePropertyNames)
        {
            var value = context.RouteData.Values[property];
            activity.SetTag($"custom.{property.Capitalise()}", value?.ToString());
        }

        foreach (var property in contextProperties)
        {
            var value = property.Value(context.HttpContext);
            activity.SetTag($"custom.{property.Key.Capitalise()}", value?.ToString());
        }

        logger.LogDebug("Updated telemetry tags to {Tags} for {Method} {URL}",
            activity.Tags,
            context.HttpContext.Request.Method,
            context.HttpContext.Request.GetDisplayUrl());
    }
}