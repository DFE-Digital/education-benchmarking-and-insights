namespace Platform.Api.Trust.Features.Details;

public static class Routes
{
    public const string TrustSingle = "trusts/{companyNumber:regex(^\\d{{8}}$)}";
    public const string TrustCollection = "trusts";
}