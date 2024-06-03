namespace Web.App.Infrastructure.Apis;

public class ComparatorSetApi(HttpClient httpClient, string? key = default)
    : ApiBase(httpClient, key), IComparatorSetApi
{
    public async Task<ApiResult> GetDefaultAsync(string urn)
    {
        return await GetAsync($"api/comparator-set/{urn}/default");
    }

    public async Task<ApiResult> GetUserDefinedAsync(string urn, string? identifier)
    {
        return await GetAsync($"api/comparator-set/{urn}/user-defined/{identifier}");
    }

    public async Task<ApiResult> UpsertUserDefinedAsync(PutComparatorSetUserDefinedRequest request)
    {
        return await PutAsync($"api/comparator-set/{request.URN}/user-defined/{request.Identifier}", new JsonContent(request));
    }
}


public interface IComparatorSetApi
{
    Task<ApiResult> GetDefaultAsync(string urn);
    Task<ApiResult> GetUserDefinedAsync(string urn, string? identifier);
    Task<ApiResult> UpsertUserDefinedAsync(PutComparatorSetUserDefinedRequest request);
}