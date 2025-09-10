using Web.App.Extensions;

namespace Web.App.Attributes.RequestTelemetry;

public class SchoolFinancialBenchmarkingInsightsSummaryTelemetry : SchoolRequestTelemetryAttribute
{
    public SchoolFinancialBenchmarkingInsightsSummaryTelemetry(TrackedRequestQueryParameters referrerKey) : base(TrackedRequestFeature.FinancialBenchmarkingInsightsSummary)
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