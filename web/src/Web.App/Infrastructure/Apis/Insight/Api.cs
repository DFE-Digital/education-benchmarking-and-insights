namespace Web.App.Infrastructure.Apis.Insight;

public static class Api
{
    public static class Balance
    {
        public static string Trusts => "api/balance/trusts";
        public static string School(string? urn) => $"api/balance/school/{urn}";
        public static string SchoolHistory(string? urn) => $"api/balance/school/{urn}/history";
        public static string Trust(string? companyNo) => $"api/balance/trust/{companyNo}";
        public static string TrustHistory(string? companyNo) => $"api/balance/trust/{companyNo}/history";
    }

    public static class BudgetForecast { }

    public static class Census
    {
        public static string Schools => "api/census";
        public static string SchoolHistoryNationalAverage => "api/census/history/national-average"; // proposed endpoint
        public static string School(string? urn) => $"api/census/{urn}";
        public static string SchoolCustom(string? urn, string? identifier) => $"api/census/{urn}/custom/{identifier}";
        public static string SchoolHistory(string? urn) => $"api/census/{urn}/history";
        public static string SchoolHistoryComparatorSetAverage(string? urn) => $"api/census/{urn}/history/comparator-set-average"; // proposed endpoint
    }

    public static class Expenditure
    {
        public static string Schools => "api/expenditure/schools";
        public static string Trusts => "api/expenditure/trusts";
        public static string SchoolHistoryNationalAverage => "api/expenditure/school/history/national-average";
        public static string School(string? urn) => $"api/expenditure/school/{urn}";
        public static string SchoolHistory(string? urn) => $"api/expenditure/school/{urn}/history";
        public static string SchoolHistoryComparatorSetAverage(string? urn) => $"api/expenditure/school/{urn}/history/comparator-set-average";
        public static string SchoolCustom(string? urn, string? identifier) => $"api/expenditure/school/{urn}/custom/{identifier}";
        public static string Trust(string? companyNo) => $"api/expenditure/trust/{companyNo}";
        public static string TrustHistory(string? companyNo) => $"api/expenditure/trust/{companyNo}/history";
    }

    public static class Income
    {
        public static string School(string? urn) => $"api/income/school/{urn}";
        public static string SchoolHistory(string? urn) => $"api/income/school/{urn}/history";
        public static string TrustHistory(string? companyNo) => $"api/income/trust/{companyNo}/history";
    }

    public static class MetricRagRating
    {
        public static string Default => "api/metric-rag/default";
        public static string Single(string? identifier) => $"api/metric-rag/{identifier}";
    }

    public static class SchoolInsight
    {
        public static string SchoolsCharacteristics => "api/schools/characteristics";
        public static string SchoolCharacteristics(string? urn) => $"api/school/{urn}/characteristics";
    }

    public static class TrustInsight
    {
        public static string TrustsCharacteristics => "api/trusts/characteristics";
    }

    public static class ItSpend
    {
        public static string Schools => "api/it-spend/schools";
    }
}