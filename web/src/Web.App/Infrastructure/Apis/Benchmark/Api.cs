namespace Web.App.Infrastructure.Apis.Benchmark;

public static class Api
{
    public static class Comparator
    {
        private const string BasePath = "api/comparators";

        public static string Schools => $"{BasePath}/schools";
        public static string Trusts => $"{BasePath}/trusts";
    }

    public static class ComparatorSet
    {
        private const string BasePath = "api/comparator-set";

        public static string SchoolDefault(string urn) => $"{BasePath}/school/{urn}/default";
        public static string SchoolCustom(string urn, string identifier) => $"{BasePath}/school/{urn}/custom/{identifier}";
        public static string SchoolUserDefined(string urn, string? identifier) => $"{BasePath}/school/{urn}/user-defined/{identifier}";
        public static string TrustUserDefined(string companyNumber, string? identifier) => $"{BasePath}/trust/{companyNumber}/user-defined/{identifier}";

    }

    public static class CustomData
    {
        public static string School(string urn, string identifier) => $"api/custom-data/school/{urn}/{identifier}";
    }

    public static class FinancialPlan
    {
        private const string BasePath = "api/financial-plan";

        public static string Plan(string? urn, int? year) => $"{BasePath}/{urn}/{year}";
        public static string DeploymentPlan(string? urn, int? year) => $"{BasePath}/{urn}/{year}/deployment";
        public static string All => "api/financial-plans";
    }

    public static class UserData
    {
        public static string All => "api/user-data";
    }
}