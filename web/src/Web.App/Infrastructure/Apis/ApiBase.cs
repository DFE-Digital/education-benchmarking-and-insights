using Web.App.Infrastructure.Extensions;

namespace Web.App.Infrastructure.Apis;

public abstract class ApiBase
{
    private readonly HttpClient _httpClient;

    protected ApiBase(HttpClient httpClient, string? key = default)
    {
        _httpClient = httpClient;

        if (!string.IsNullOrEmpty(key))
        {
            _httpClient.DefaultRequestHeaders.Add("x-functions-key", key);
        }
    }

    protected async Task<ApiResult> GetAsync(string requestUri, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetAsync(requestUri, cancellationToken).ToApiResult(cancellationToken);
    }

    protected async Task<ApiResult> PutAsync(string requestUri, JsonContent content)
    {
        return await _httpClient.PutAsync(requestUri, content).ToApiResult();
    }

    protected async Task<ApiResult> PostAsync(string requestUri, JsonContent content, CancellationToken cancellationToken = default)
    {
        return await _httpClient.PostAsync(requestUri, content, cancellationToken).ToApiResult(cancellationToken: cancellationToken);
    }

    protected async Task<ApiResult> DeleteAsync(string requestUri)
    {
        return await _httpClient.DeleteAsync(requestUri).ToApiResult();
    }

    protected async Task<ApiResult> PostAsync(string requestUri, MultipartFormDataContent content)
    {
        return await _httpClient.PostAsync(requestUri, content).ToApiResult();
    }

    protected async Task<ApiResult> SendAsync(HttpRequestMessage message, CancellationToken cancellationToken = default)
    {
        return await _httpClient.SendAsync(message, cancellationToken).ToApiResult(cancellationToken);
    }
}