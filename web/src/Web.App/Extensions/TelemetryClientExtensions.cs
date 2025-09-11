using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Web.App.Identity.Models;
using Web.App.Telemetry;

namespace Web.App.Extensions;

public static class TelemetryClientExtensions
{
    public static void TrackUserSignedInEvent(this ITelemetryClientWrapper telemetry, TokenValidatedContext context, Organisation? organisation)
    {
        var user = string.Empty;
        if (context.Principal?.Claims != null)
        {
            user = context.Principal.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value)
                .SingleOrDefault() ?? string.Empty;
        }

        var organisationType = organisation?.Category?.Id switch
        {
            OrganisationCategories.SingleAcademyTrust or OrganisationCategories.MultiAcademyTrust => TrackedRequestEstablishmentType.Trust,
            OrganisationCategories.LocalAuthority => TrackedRequestEstablishmentType.LocalAuthority,
            null => (TrackedRequestEstablishmentType?)null,
            _ => TrackedRequestEstablishmentType.School
        };

        var properties = new Dictionary<string, string>
        {
            { "User", user },
            { "Establishment", organisationType?.GetStringValue() ?? string.Empty }
        };

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (organisationType)
        {
            case TrackedRequestEstablishmentType.Trust:
                properties[nameof(TrackedRequestRouteParameters.CompanyNumber)] = organisation?.CompanyRegistrationNumber?.ToString("00000000") ?? string.Empty;
                break;
            case TrackedRequestEstablishmentType.LocalAuthority:
                properties[nameof(TrackedRequestRouteParameters.Code)] = organisation?.EstablishmentNumber?.ToString("000") ?? string.Empty;
                break;
            case TrackedRequestEstablishmentType.School:
                properties[nameof(TrackedRequestRouteParameters.Urn)] = organisation?.URN?.ToString("000000") ?? string.Empty;
                break;
        }

        telemetry.TrackEvent(TrackedEvents.UserSignInSuccess.GetStringValue(), properties);
    }
}