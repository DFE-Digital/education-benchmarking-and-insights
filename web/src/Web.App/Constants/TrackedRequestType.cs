using Web.App.Attributes;
namespace Web.App;

public enum TrackedRequestType
{
    [StringValue("Establishment")]
    Establishment,
    [StringValue("Feature")]
    Feature
}

public enum TrackedRequestEstablishmentType
{
    [StringValue("school")]
    School,
    [StringValue("trust")]
    Trust,
    [StringValue("local-authority")]
    LocalAuthority
}

public enum TrackedRequestFeature
{
    [StringValue("home")]
    Home
}

public enum TrackedRequestRouteParameters
{
    [StringValue("urn")]
    Urn,
    [StringValue("companyNumber")]
    CompanyNumber,
    [StringValue("code")]
    Code
}