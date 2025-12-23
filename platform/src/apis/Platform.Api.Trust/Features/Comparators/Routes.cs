namespace Platform.Api.Trust.Features.Comparators;

public static class Routes
{
    public const string Comparators = "trusts/{companyNumber:regex(^\\d{{8}}$)}/comparators";
}