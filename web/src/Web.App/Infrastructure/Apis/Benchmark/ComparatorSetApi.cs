namespace Web.App.Infrastructure.Apis.Benchmark;

public class ComparatorSetApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IComparatorSetApi
{
    public async Task<ApiResult> GetDefaultSchoolAsync(string urn) => await GetAsync(Api.ComparatorSet.SchoolDefault(urn));
    public async Task<ApiResult> GetCustomSchoolAsync(string urn, string identifier) => await GetAsync(Api.ComparatorSet.SchoolCustom(urn, identifier));
    public async Task<ApiResult> GetUserDefinedSchoolAsync(string urn, string? identifier) => await GetAsync(Api.ComparatorSet.SchoolUserDefined(urn, identifier));
    public async Task<ApiResult> RemoveUserDefinedSchoolAsync(string urn, string? identifier) => await DeleteAsync(Api.ComparatorSet.SchoolUserDefined(urn, identifier));
    public async Task<ApiResult> UpsertUserDefinedSchoolAsync(string urn, PutComparatorSetUserDefinedRequest request) => await PutAsync(Api.ComparatorSet.SchoolUserDefined(urn, request.Identifier.ToString()), new JsonContent(request));
    public async Task<ApiResult> GetUserDefinedTrustAsync(string companyNumber, string? identifier) => await GetAsync(Api.ComparatorSet.TrustUserDefined(companyNumber, identifier));
    public async Task<ApiResult> RemoveUserDefinedTrustAsync(string companyNumber, string? identifier) => await DeleteAsync(Api.ComparatorSet.TrustUserDefined(companyNumber, identifier));
    public async Task<ApiResult> UpsertUserDefinedTrustAsync(string companyNumber, PutComparatorSetUserDefinedRequest request) => await PutAsync(Api.ComparatorSet.TrustUserDefined(companyNumber, request.Identifier.ToString()), new JsonContent(request));
}

public interface IComparatorSetApi
{
    Task<ApiResult> GetDefaultSchoolAsync(string urn);
    Task<ApiResult> GetCustomSchoolAsync(string urn, string identifier);
    Task<ApiResult> GetUserDefinedSchoolAsync(string urn, string? identifier);
    Task<ApiResult> UpsertUserDefinedSchoolAsync(string urn, PutComparatorSetUserDefinedRequest request);
    Task<ApiResult> RemoveUserDefinedSchoolAsync(string urn, string? identifier);
    Task<ApiResult> GetUserDefinedTrustAsync(string companyNumber, string? identifier);
    Task<ApiResult> RemoveUserDefinedTrustAsync(string companyNumber, string? identifier);
    Task<ApiResult> UpsertUserDefinedTrustAsync(string companyNumber, PutComparatorSetUserDefinedRequest request);
}