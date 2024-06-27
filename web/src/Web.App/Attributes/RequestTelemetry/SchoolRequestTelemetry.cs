using Web.App.Extensions;
namespace Web.App.Attributes.RequestTelemetry;

public class SchoolRequestTelemetryAttribute(TrackedRequestFeature feature) : RequestTelemetryAttribute(new Dictionary<string, object?>
{
    {
        TrackedRequestType.Establishment.GetStringValue(), TrackedRequestEstablishmentType.School.GetStringValue()
    },
    {
        TrackedRequestType.Feature.GetStringValue(), feature.GetStringValue()
    }
}, TrackedRequestRouteParameters.Urn.GetStringValue());