using System.Security.Claims;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Web.App.Telemetry;

namespace Web.App.Extensions;

public static class TelemetryClientExtensions
{
    private static readonly string[] SensitiveClaims = [ClaimTypes.Email, ClaimTypes.GivenName, ClaimTypes.Surname, "sid", "nonce", "at_hash"];

    public static void TrackUserSignedInEvent(this ITelemetryClientWrapper telemetry, TokenValidatedContext context)
    {
        var user = string.Empty;
        var claims = Array.Empty<string>();
        if (context.Principal?.Claims != null)
        {
            user = context.Principal.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value)
                .SingleOrDefault() ?? string.Empty;

            claims = context.Principal.Claims
                .Select(c =>
                {
                    var value = c.Value;
                    if (SensitiveClaims.Contains(c.Type))
                    {
                        value = new string('*', c.Value.Length);
                    }

                    return $"{c.Type}: {value}";
                })
                .ToArray();
        }

        telemetry.TrackEvent(TrackedEvents.UserSignInSuccess.GetStringValue(), new Dictionary<string, string>
        {
            { "User", user },
            { "Claims", string.Join(Environment.NewLine, claims) }
        });
    }
}