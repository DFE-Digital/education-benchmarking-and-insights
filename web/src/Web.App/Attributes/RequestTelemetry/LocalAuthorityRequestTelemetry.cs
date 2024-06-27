using Web.App.Extensions;
namespace Web.App.Attributes.RequestTelemetry;

public class LocalAuthorityRequestTelemetryAttribute(TrackedRequestFeature feature) : RequestTelemetryAttribute(new Dictionary<string, object?>
{
    {
        TrackedRequestType.Establishment.GetStringValue(), TrackedRequestEstablishmentType.LocalAuthority.GetStringValue()
    },
    {
        TrackedRequestType.Feature.GetStringValue(), feature.GetStringValue()
    }
}, TrackedRequestRouteParameters.Code.GetStringValue());