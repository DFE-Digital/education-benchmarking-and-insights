using EducationBenchmarking.Web.Infrastructure.Extensions;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public abstract class BaseApi
{
    private readonly HttpClient _httpClient;

    protected BaseApi(HttpClient httpClient, string? key = default)
    {
        _httpClient = httpClient;

        if (!string.IsNullOrEmpty(key))
        {
            _httpClient.DefaultRequestHeaders.Add("x-functions-key", key);
        }
    }

    protected async Task<ApiResult> GetAsync(string requestUri)
    {
        return await _httpClient.GetAsync(requestUri).ToApiResult();
    }
    
    protected async Task<ApiResult> PutAsync(string requestUri, JsonContent content)
    {
        return await _httpClient.PutAsync(requestUri, content).ToApiResult();
    }

    protected async Task<ApiResult> PostAsync(string requestUri, JsonContent content)
    {
        return await _httpClient.PostAsync(requestUri, content).ToApiResult();
    }
    
    protected async Task<ApiResult> PostAsync(string requestUri, MultipartFormDataContent content)
    {
        return await _httpClient.PostAsync(requestUri, content).ToApiResult();
    }

    protected async Task<ApiResult> SendAsync(HttpRequestMessage message)
    {
        return await _httpClient.SendAsync(message).ToApiResult();
    }
    
    protected async Task<ApiResult> SendAsync(HttpRequestMessage message, CancellationToken cancellationToken)
    {
        return await _httpClient.SendAsync(message, cancellationToken).ToApiResult();
    }
}