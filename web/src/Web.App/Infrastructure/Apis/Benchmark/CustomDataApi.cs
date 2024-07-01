namespace Web.App.Infrastructure.Apis.Benchmark;

public class CustomDataApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ICustomDataApi
{
    public async Task<ApiResult> GetSchoolAsync(string urn, string identifier) => await GetAsync(Api.CustomData.School(urn, identifier));

    public async Task<ApiResult> RemoveSchoolAsync(string urn, string identifier) => await DeleteAsync(Api.CustomData.School(urn, identifier));

    public async Task<ApiResult> UpsertSchoolAsync(string urn, PutCustomDataRequest request) => await PutAsync(Api.CustomData.School(urn, request.Identifier.ToString()), new JsonContent(request));
}

public interface ICustomDataApi
{
    Task<ApiResult> GetSchoolAsync(string urn, string identifier);
    Task<ApiResult> RemoveSchoolAsync(string urn, string identifier);
    Task<ApiResult> UpsertSchoolAsync(string urn, PutCustomDataRequest request);
}