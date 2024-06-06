namespace Web.App.Infrastructure.Apis;

public class ComparatorSetApi(HttpClient httpClient, string? key = default)
    : ApiBase(httpClient, key), IComparatorSetApi
{
    public async Task<ApiResult> GetDefaultSchoolAsync(string urn)
    {
        return await GetAsync($"api/comparator-set/school/{urn}/default");
    }

    public async Task<ApiResult> GetUserDefinedSchoolAsync(string urn, string? identifier)
    {
        return await GetAsync($"api/comparator-set/school/{urn}/user-defined/{identifier}");
    }

    public async Task<ApiResult> RemoveUserDefinedSchoolAsync(string urn, string? identifier)
    {
        return await DeleteAsync($"api/comparator-set/school/{urn}/user-defined/{identifier}");
    }
    public async Task<ApiResult> UpsertUserDefinedSchoolAsync(PutComparatorSetUserDefinedRequest request)
    {
        return await PutAsync($"api/comparator-set/school/{request.URN}/user-defined/{request.Identifier}", new JsonContent(request));
    }
}


public interface IComparatorSetApi
{
    Task<ApiResult> GetDefaultSchoolAsync(string urn);
    Task<ApiResult> GetUserDefinedSchoolAsync(string urn, string? identifier);
    Task<ApiResult> UpsertUserDefinedSchoolAsync(PutComparatorSetUserDefinedRequest request);
    Task<ApiResult> RemoveUserDefinedSchoolAsync(string urn, string? identifier);
}