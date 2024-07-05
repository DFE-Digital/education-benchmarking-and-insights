using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Web.App.Extensions;

namespace Web.App.Telemetry;

public class AuthenticatedUserTelemetryInitializer(IHttpContextAccessor? httpContextAccessor) : ITelemetryInitializer
{
    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry is not RequestTelemetry requestTelemetry)
        {
            return;
        }

        var user = httpContextAccessor?.HttpContext?.User;
        if (user is not { Identity.IsAuthenticated: true })
        {
            return;
        }

        requestTelemetry.Context.User.AuthenticatedUserId = user.UserGuid().ToString();
    }
}