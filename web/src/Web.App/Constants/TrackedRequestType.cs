using Web.App.Attributes;
namespace Web.App;

public enum TrackedRequestType
{
    [StringValue(nameof(Establishment))]
    Establishment,
    [StringValue(nameof(Feature))]
    Feature,
    [StringValue(nameof(Referrer))]
    Referrer
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
    [StringValue("benchmark-central-costs")]
    BenchmarkCentralCosts,
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
    SpendingPriorities,
    [StringValue("financial-benchmarking-insights-summary")]
    FinancialBenchmarkingInsightsSummary
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

public enum TrackedRequestQueryParameters
{
    [StringValue("referrer")]
    Referrer
}