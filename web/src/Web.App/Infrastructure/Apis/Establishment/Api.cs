namespace Web.App.Infrastructure.Apis.Establishment;

public static class Api
{
    public static class Establishment
    {
        public static string Schools => "api/schools";
        public static string SchoolSuggest => "api/schools/suggest";
        public static string SchoolSearch => "api/schools/search";
        public static string TrustSuggest => "api/trusts/suggest";
        public static string TrustSearch => "api/trusts/search";
        public static string LocalAuthorities => "api/local-authorities";
        public static string LocalAuthoritySuggest => "api/local-authorities/suggest";
        public static string LocalAuthorityNationalRank => "api/local-authorities/national-rank";
        public static string School(string? identifier)
        {
            return $"api/school/{identifier}";
        }
        public static string Trust(string? identifier)
        {
            return $"api/trust/{identifier}";
        }
        public static string LocalAuthority(string? identifier)
        {
            return $"api/local-authority/{identifier}";
        }
        public static string LocalAuthorityStatisticalNeighbours(string? identifier)
        {
            return $"api/local-authority/{identifier}/statistical-neighbours";
        }
    }
}