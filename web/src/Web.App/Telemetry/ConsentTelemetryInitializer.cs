using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Web.App.Telemetry;

public class ConsentTelemetryInitializer(IHttpContextAccessor accessor) : ITelemetryInitializer
{
    private const string CookieName = "cookie_policy";

    public void Initialize(ITelemetry telemetry)
    {
        var ctx = accessor.HttpContext;
        if (ctx == null) return;

        var status = GetTrackingConsentStatus(ctx.Request);

        telemetry.Context.GlobalProperties["tracking_consent_status"] = status;
    }

    private static string GetTrackingConsentStatus(HttpRequest request)
    {
        if (!request.Cookies.TryGetValue(CookieName, out var value))
        {
            return TrackingConsentStatus.OptOutImplicit;
        }

        return value == "enabled"
            ? TrackingConsentStatus.OptIn
            : TrackingConsentStatus.OptOutExplicit;
    }
}

public static class TrackingConsentStatus
{
    public const string OptIn = "opt_in";
    public const string OptOutExplicit = "opt_out_explicit";
    public const string OptOutImplicit = "opt_out_implicit";
}
