namespace Platform.Api.LocalAuthority.Features.Details;

public static class Routes
{
    public const string LocalAuthoritySingle = "local-authorities/{code:regex(^\\d{{3}}$)}";
    public const string LocalAuthorityCollection = "local-authorities";
    public const string MaintainedSchoolsFinance = "local-authorities/{code:regex(^\\d{{3}}$)}/maintained-schools/finance";
    public const string MaintainedSchoolsWorkforce = "local-authorities/{code:regex(^\\d{{3}}$)}/maintained-schools/workforce";
}