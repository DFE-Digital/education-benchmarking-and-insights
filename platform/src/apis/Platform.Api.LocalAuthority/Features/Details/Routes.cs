namespace Platform.Api.LocalAuthority.Features.Details;

public static class Routes
{
    public const string LocalAuthoritySingle = "local-authorities/{code:regex(^\\d{{3}}$)}";
    public const string LocalAuthorityCollection = "local-authorities";
}