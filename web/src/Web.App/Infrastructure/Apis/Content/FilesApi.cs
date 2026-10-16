namespace Web.App.Infrastructure.Apis.Content;

public interface IFilesApi
{
    Task<ApiResult> GetTransparencyFiles();
    Task<ApiResult> GetLocalAuthorityRiskScores(string year);
}

public class FilesApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IFilesApi
{
    public async Task<ApiResult> GetTransparencyFiles() => await GetAsync(Api.Files.Transparency);

    public async Task<ApiResult> GetLocalAuthorityRiskScores(string year) =>
        await GetAsync(Api.Files.LocalAuthorityRiskScores(year));
}
