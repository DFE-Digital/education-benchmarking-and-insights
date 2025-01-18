namespace Platform.Api.Insight.Features.Balance;

public static class Routes
{
    public const string Trusts = "balance/trusts";
    public const string TrustHistory = "balance/trust/{companyNumber}/history";
    public const string Trust = "balance/trust/{companyNumber}";
    public const string SchoolHistory = "balance/school/{urn}/history";
    public const string School = "balance/school/{urn}";
}