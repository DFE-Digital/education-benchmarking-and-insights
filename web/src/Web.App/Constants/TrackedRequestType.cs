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
    [StringValue("local-authority")]
    LocalAuthority,
    [StringValue("school")]
    School,
    [StringValue("trust")]
    Trust
}

public enum TrackedRequestFeature
{
    [StringValue("benchmark-workforce")]
    BenchmarkWorkforce,
    [StringValue("benchmark-costs")]
    BenchmarkCosts,
    [StringValue("customised-data")]
    CustomisedData,
    [StringValue("details")]
    Details,
    [StringValue("forecast")]
    Forecast,
    [StringValue("history")]
    History,
    [StringValue("home")]
    Home,
    [StringValue("curriculum-financial-planning")]
    Planning,
    [StringValue("resources")]
    Resources,
    [StringValue("spending-priorities")]
    SpendingPriorities
}

public enum TrackedRequestRouteParameters
{
    [StringValue("code")]
    Code,
    [StringValue("companyNumber")]
    CompanyNumber,
    [StringValue("urn")]
    Urn
}