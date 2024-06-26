using Web.App.Attributes;
namespace Web.App;

public enum TrackedLinks
{
    [StringValue("gias-school-details")]
    SchoolDetails,
    [StringValue("commercial-resource")]
    CommercialResource,
    [StringValue("guidance-resource")]
    GuidanceResource
}