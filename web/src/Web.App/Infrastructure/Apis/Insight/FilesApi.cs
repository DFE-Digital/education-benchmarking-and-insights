namespace Web.App.Infrastructure.Apis.Insight;

public interface IFilesApi
{
    Task<ApiResult> GetAarTransparencyFiles();
    Task<ApiResult> GetCfrTransparencyFiles();
}

public class FilesApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IFilesApi
{
    public async Task<ApiResult> GetAarTransparencyFiles() => await GetAsync(Api.Files.TransparencyAar);
    public async Task<ApiResult> GetCfrTransparencyFiles() => await GetAsync(Api.Files.TransparencyCfr);
}