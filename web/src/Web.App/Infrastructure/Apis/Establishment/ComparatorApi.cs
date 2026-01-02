namespace Web.App.Infrastructure.Apis.Establishment;

[Obsolete(message: "Use SchoolApi or TrustApi instead.")]
public class ComparatorApi(ITrustApi trustApi, ISchoolApi schoolApi) : IComparatorApi
{
    public Task<ApiResult> CreateSchoolsAsync(string urn, PostSchoolComparatorsRequest request) => schoolApi.CreateComparatorsAsync(urn, request);

    public Task<ApiResult> CreateTrustsAsync(string companyNumber, PostTrustComparatorsRequest request) => trustApi.CreateComparatorsAsync(companyNumber, request);
}


[Obsolete(message: "Use ISchoolApi or ITrustApi instead.")]
public interface IComparatorApi
{
    Task<ApiResult> CreateSchoolsAsync(string urn, PostSchoolComparatorsRequest request);
    Task<ApiResult> CreateTrustsAsync(string companyNumber, PostTrustComparatorsRequest request);
}