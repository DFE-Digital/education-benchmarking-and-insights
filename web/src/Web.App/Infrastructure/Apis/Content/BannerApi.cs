namespace Web.App.Infrastructure.Apis.Content;

public interface IBannerApi
{
    Task<ApiResult> GetBanner(string target);
}

public class BannerApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IBannerApi
{
    public async Task<ApiResult> GetBanner(string target)
    {
        return await GetAsync(Api.Banners.Banner(target));
    }
}