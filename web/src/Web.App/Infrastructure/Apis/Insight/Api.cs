namespace Web.App.Infrastructure.Apis.Insight;

public static class Api
{
    public static class Balance
    {
        public static string School(string? urn) => $"api/balance/school/{urn}";
        public static string SchoolHistory(string? urn) => $"api/balance/school/{urn}/history";
        public static string Trust(string? companyNo) => $"api/balance/trust/{companyNo}";
        public static string TrustHistory(string? companyNo) => $"api/balance/trust/{companyNo}/history";
        public static string Trusts => "api/balance/trusts";
    }

    public static class BudgetForecast { }

    public static class Census
    {
        public static string School(string? urn) => $"api/census/{urn}";
        public static string SchoolCustom(string? urn, string? identifier) => $"api/census/{urn}/custom/{identifier}";
        public static string SchoolHistory(string? urn) => $"api/census/{urn}/history";
        public static string Schools => "api/census";
    }

    public static class Expenditure
    {
        public static string School(string? urn) => $"api/expenditure/school/{urn}";
        public static string SchoolHistory(string? urn) => $"api/expenditure/school/{urn}/history";
        public static string SchoolCustom(string? urn, string? identifier) => $"api/expenditure/school/{urn}/custom/{identifier}";
        public static string Schools => "api/expenditure/schools";
        public static string Trust(string? companyNo) => $"api/expenditure/trust/{companyNo}";
        public static string TrustHistory(string? companyNo) => $"api/expenditure/trust/{companyNo}/history";
        public static string Trusts => "api/expenditure/trusts";
    }

    public static class Income
    {
        public static string School(string? urn) => $"api/income/school/{urn}";
        public static string SchoolHistory(string? urn) => $"api/income/school/{urn}/history";
        public static string TrustHistory(string? companyNo) => $"api/income/trust/{companyNo}/history";
    }

    public static class Insight
    {
        public static string CurrentReturnYears => "api/current-return-years";
    }

    public static class MetricRagRating
    {
        public static string Default => "api/metric-rag/default";
        public static string Single(string? identifier) => $"api/metric-rag/{identifier}";
    }

    public static class SchoolInsight
    {
        public static string SchoolCharacteristics(string? urn) => $"api/school/{urn}/characteristics";
        public static string SchoolsCharacteristics => "api/schools/characteristics";
    }

    public static class TrustInsight
    {
        public static string TrustsCharacteristics => "api/trusts/characteristics";
    }
}