namespace Web.App.Infrastructure.Apis.LocalAuthorityFinances;

public static class Api
{
    public static class LocalAuthorityFinances
    {
        public static string HighNeeds => "api/high-needs";
        public static string HighNeedsHistory => "api/high-needs/history";
        public static string SchoolsFinance(string code) => $"api/local-authorities/{code}/schools/finance";
        public static string SchoolsWorkforce(string code) => $"api/local-authorities/{code}/schools/workforce";
    }
}