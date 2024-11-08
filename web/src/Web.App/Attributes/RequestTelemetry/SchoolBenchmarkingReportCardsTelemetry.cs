using Web.App.Extensions;
namespace Web.App.Attributes.RequestTelemetry;

public class SchoolBenchmarkingReportCardsTelemetry : SchoolRequestTelemetryAttribute
{
    public SchoolBenchmarkingReportCardsTelemetry(TrackedRequestQueryParameters referrerKey) : base(TrackedRequestFeature.BenchmarkingReportCards)
    {
        ContextProperties.Add(TrackedRequestType.Referrer.GetStringValue(), c =>
        {
            if (c.Request.Query.TryGetValue(referrerKey.GetStringValue(), out var referrer))
            {
                return referrer;
            }

            return null;
        });
    }
}