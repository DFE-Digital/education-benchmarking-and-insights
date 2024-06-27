using Web.App.Extensions;
namespace Web.App.Attributes.RequestTelemetry;

public class TrustRequestTelemetryAttribute(TrackedRequestFeature feature) : RequestTelemetryAttribute(new Dictionary<string, object?>
{
    {
        TrackedRequestType.Establishment.GetStringValue(), TrackedRequestEstablishmentType.Trust.GetStringValue()
    },
    {
        TrackedRequestType.Feature.GetStringValue(), feature.GetStringValue()
    }
}, TrackedRequestRouteParameters.CompanyNumber.GetStringValue());