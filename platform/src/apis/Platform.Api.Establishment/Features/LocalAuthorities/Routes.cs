namespace Platform.Api.Establishment.Features.LocalAuthorities;

public static class Routes
{
    public const string LocalAuthorities = "local-authorities";
    public const string LocalAuthoritiesSuggest = "local-authorities/suggest";
    public const string LocalAuthoritiesNationalRank = "local-authorities/national-rank";
    public const string LocalAuthority = "local-authority/{identifier}";
    public const string LocalAuthorityStatisticalNeighbours = "local-authority/{identifier}/statistical-neighbours";
    public const string LocalAuthoritiesSearch = "local-authorities/search";
}