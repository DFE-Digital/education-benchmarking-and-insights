namespace Web.App.Infrastructure.Apis;

public class ComparatorSetApi(HttpClient httpClient, string? key = default)
    : ApiBase(httpClient, key), IComparatorSetApi
{
    public async Task<ApiResult> GetDefaultSchoolAsync(string urn) => await GetAsync($"api/comparator-set/school/{urn}/default");

    public async Task<ApiResult> GetUserDefinedSchoolAsync(string urn, string? identifier) => await GetAsync($"api/comparator-set/school/{urn}/user-defined/{identifier}");

    public async Task<ApiResult> RemoveUserDefinedSchoolAsync(string urn, string? identifier) => await DeleteAsync($"api/comparator-set/school/{urn}/user-defined/{identifier}");

    public async Task<ApiResult> UpsertUserDefinedSchoolAsync(string urn, PutComparatorSetUserDefinedRequest request) => await PutAsync($"api/comparator-set/school/{urn}/user-defined/{request.Identifier}", new JsonContent(request));

    public async Task<ApiResult> GetUserDefinedTrustAsync(string companyNumber, string? identifier) => await GetAsync($"api/comparator-set/trust/{companyNumber}/user-defined/{identifier}");

    public async Task<ApiResult> RemoveUserDefinedTrustAsync(string companyNumber, string? identifier) => await DeleteAsync($"api/comparator-set/trust/{companyNumber}/user-defined/{identifier}");

    public async Task<ApiResult> UpsertUserDefinedTrustAsync(string companyNumber, PutComparatorSetUserDefinedRequest request) => await PutAsync($"api/comparator-set/trust/{companyNumber}/user-defined/{request.Identifier}", new JsonContent(request));
}

public interface IComparatorSetApi
{
    Task<ApiResult> GetDefaultSchoolAsync(string urn);
    Task<ApiResult> GetUserDefinedSchoolAsync(string urn, string? identifier);
    Task<ApiResult> UpsertUserDefinedSchoolAsync(string urn, PutComparatorSetUserDefinedRequest request);
    Task<ApiResult> RemoveUserDefinedSchoolAsync(string urn, string? identifier);
    Task<ApiResult> GetUserDefinedTrustAsync(string companyNumber, string? identifier);
    Task<ApiResult> RemoveUserDefinedTrustAsync(string companyNumber, string? identifier);
    Task<ApiResult> UpsertUserDefinedTrustAsync(string companyNumber, PutComparatorSetUserDefinedRequest request);
}