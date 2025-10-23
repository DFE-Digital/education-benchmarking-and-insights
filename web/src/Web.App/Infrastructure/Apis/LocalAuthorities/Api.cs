namespace Web.App.Infrastructure.Apis.LocalAuthorities;

public static class Api
{
    public static class LocalAuthorities
    {
        public static string HighNeeds => "api/high-needs";
        public static string HighNeedsHistory => "api/high-needs/history";
        public static string SchoolsFinance(string code) => $"api/local-authorities/{code}/schools/finance";
    }
}