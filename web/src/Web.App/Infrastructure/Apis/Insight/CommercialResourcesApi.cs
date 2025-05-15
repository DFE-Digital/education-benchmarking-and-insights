namespace Web.App.Infrastructure.Apis.Insight;

public interface ICommercialResourcesApi
{
    Task<ApiResult> GetCommercialResources();
}

public class CommercialResourcesApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ICommercialResourcesApi
{
    public async Task<ApiResult> GetCommercialResources() => await GetAsync(Api.CommercialResources.Resources);
}