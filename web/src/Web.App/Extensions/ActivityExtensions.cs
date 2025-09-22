using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Web.App.Identity.Models;

namespace Web.App.Extensions;

public static class ActivityExtensions
{
    public static void TrackUserSignInInitiatedEvent(this Activity? activity, RedirectContext context)
    {
        if (activity == null)
        {
            return;
        }
        
        var request = context.HttpContext.Request;
        var path = request.Path.Value;
        var redirectUri = request.Query["redirectUri"].ToString();

        var properties = new ActivityTagsCollection
        {
            { "custom.Path", path ?? string.Empty },
            { "custom.RedirectUri", redirectUri }
        };

        var route = request.RouteValues;
        foreach (var (key, value) in route)
        {
            properties.Add($"custom.Route.{key.Sanitise()}", value?.ToString() ?? string.Empty);
        }

        activity.AddEvent(new ActivityEvent(TrackedEvents.UserSignInInitiated.GetStringValue(), default, properties));
        
        // https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/monitor/Azure.Monitor.OpenTelemetry.AspNetCore#customevents
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ActivityEvent>>();
        logger.LogInformation("{microsoft.custom_event.name} {path}", TrackedEvents.UserSignInInitiated.GetStringValue(), path);
    }

    public static void TrackUserSignedInEvent(this Activity? activity, TokenValidatedContext context, Organisation? organisation)
    {
        if (activity == null)
        {
            return;
        }
        
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

        var properties = new ActivityTagsCollection
        {
            { "custom.User", user },
            { "custom.Establishment", organisationType?.GetStringValue() ?? string.Empty }
        };

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (organisationType)
        {
            case TrackedRequestEstablishmentType.Trust:
                properties[$"custom.{nameof(TrackedRequestRouteParameters.CompanyNumber)}"] = organisation?.CompanyRegistrationNumber?.ToString("00000000") ?? string.Empty;
                break;
            case TrackedRequestEstablishmentType.LocalAuthority:
                properties[$"custom.{nameof(TrackedRequestRouteParameters.Code)}"] = organisation?.EstablishmentNumber?.ToString("000") ?? string.Empty;
                break;
            case TrackedRequestEstablishmentType.School:
                properties[$"custom.{nameof(TrackedRequestRouteParameters.Urn)}"] = organisation?.URN?.ToString("000000") ?? string.Empty;
                break;
        }

        activity.AddEvent(new ActivityEvent(TrackedEvents.UserSignInSuccess.GetStringValue(), default, properties));
    }

    public static void TrackAuthenticatedUserId(this Activity? activity, HttpRequest request)
    {
        if (activity == null)
        {
            return;
        }
        
        var user = request.HttpContext.User;
        if (user is not { Identity.IsAuthenticated: true })
        {
            return;
        }

        var userId = user.UserGuid().ToString();
        
        // https://opentelemetry.io/docs/specs/semconv/registry/attributes/user/#user-id
        activity.SetTag("user.id", userId);
    }
}