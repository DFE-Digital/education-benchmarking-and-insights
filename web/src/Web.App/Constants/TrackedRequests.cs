using Web.App.Attributes;
namespace Web.App;

public enum TrackedRequests
{
    [StringValue("School homepage")]
    SchoolHome,
    [StringValue("Trust homepage")]
    TrustHome,
    [StringValue("Local authority homepage")]
    LocalAuthorityHome
}