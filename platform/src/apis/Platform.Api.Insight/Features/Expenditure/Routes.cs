namespace Platform.Api.Insight.Features.Expenditure;

public static class Routes
{
    public const string SchoolCustom = "expenditure/school/{urn}/custom/{identifier}";
    public const string School = "expenditure/school/{urn}";
    public const string SchoolHistoryComparatorSetAverage = "expenditure/school/{urn}/history/comparator-set-average";
    public const string SchoolHistory = "expenditure/school/{urn}/history";
    public const string SchoolHistoryNationalAverage = "expenditure/school/history/national-average";
    public const string Schools = "expenditure/schools";
    public const string Trust = "expenditure/trust/{companyNumber}";
    public const string TrustHistory = "expenditure/trust/{companyNumber}/history";
    public const string Trusts = "expenditure/trusts";
}