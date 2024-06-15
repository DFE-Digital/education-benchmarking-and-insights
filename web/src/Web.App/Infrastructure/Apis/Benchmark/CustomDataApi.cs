namespace Web.App.Infrastructure.Apis;

public class CustomDataApi(HttpClient httpClient, string? key = default)
    : ApiBase(httpClient, key), ICustomDataApi
{
    public async Task<ApiResult> GetSchoolAsync(string urn, string identifier) => await GetAsync($"api/custom-data/school/{urn}/{identifier}");

    public async Task<ApiResult> RemoveSchoolAsync(string urn, string identifier) => await DeleteAsync($"api/custom-data/school/{urn}/{identifier}");

    public async Task<ApiResult> UpsertSchoolAsync(string urn, PutCustomDataRequest request) => await PutAsync($"api/custom-data/school/{urn}/{request.Identifier}", new JsonContent(request));
}

public interface ICustomDataApi
{
    Task<ApiResult> GetSchoolAsync(string urn, string identifier);
    Task<ApiResult> RemoveSchoolAsync(string urn, string identifier);
    Task<ApiResult> UpsertSchoolAsync(string urn, PutCustomDataRequest request);
}