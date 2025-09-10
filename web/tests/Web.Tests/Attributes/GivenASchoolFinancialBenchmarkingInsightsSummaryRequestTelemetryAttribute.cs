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

public class GivenASchoolFinancialBenchmarkingInsightsSummaryRequestTelemetryAttribute
{
    private readonly IServiceCollection _services = new ServiceCollection();

    public GivenASchoolFinancialBenchmarkingInsightsSummaryRequestTelemetryAttribute()
    {
        _services.AddScoped<ILogger<RequestTelemetryFilter>, NullLogger<RequestTelemetryFilter>>();
    }

    [Theory]
    [InlineData("123456", null)]
    [InlineData(null, null)]
    [InlineData("123456", "somewhere")]
    public async Task SetsPropertiesInRequestTelemetry(string? urn, string? referrer)
    {
        var routeData = new RouteData(new RouteValueDictionary
        {
            { "urn", urn }
        });

        const TrackedRequestQueryParameters referrerKey = TrackedRequestQueryParameters.Referrer;
        var query = new QueryString();
        if (referrer != null)
        {
            query = query.Add(referrerKey.GetStringValue(), referrer);
        }

        var telemetry = await GetTelemetryFromRequest(routeData, query, referrerKey);

        Assert.Equal("school", telemetry.Properties["Establishment"]);
        Assert.Equal(TrackedRequestFeature.FinancialBenchmarkingInsightsSummary.GetStringValue(), telemetry.Properties["Feature"]);
        Assert.Equal(urn, telemetry.Properties["Urn"]);
        Assert.Equal(referrer, telemetry.Properties["Referrer"]);
    }

    private async Task<RequestTelemetry> GetTelemetryFromRequest(RouteData routeData, QueryString query, TrackedRequestQueryParameters referrerKey)
    {
        var provider = _services.BuildServiceProvider();
        var httpContext = new DefaultHttpContext
        {
            RequestServices = provider
        };

        var telemetry = new RequestTelemetry();
        httpContext.Features.Set(telemetry);
        httpContext.Request.QueryString = query;

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

        var attribute = new SchoolFinancialBenchmarkingInsightsSummaryTelemetry(referrerKey);
        var filter = new RequestTelemetryFilter(
            provider.GetService<ILogger<RequestTelemetryFilter>>()!,
            attribute.Properties,
            attribute.RoutePropertyNames,
            attribute.ContextProperties);
        filter.OnActionExecuted(actionExecutedContext);

        await (actionExecutedContext.Result?.ExecuteResultAsync(actionContext) ?? Task.CompletedTask);
        return telemetry;
    }

    private class FakeController : Controller;
}