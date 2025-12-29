namespace Platform.Api.LocalAuthority.Features.Details;

public static class Routes
{
    public const string LocalAuthoritySingle = $"local-authorities/{Constants.CodeParam}";
    public const string LocalAuthorityCollection = "local-authorities";
    public const string MaintainedSchoolsFinance = $"local-authorities/{Constants.CodeParam}/maintained-schools/finance";
    public const string MaintainedSchoolsWorkforce = $"local-authorities/{Constants.CodeParam}/maintained-schools/workforce";
}