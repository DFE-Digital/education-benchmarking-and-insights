using Web.App.Infrastructure.Extensions;

namespace Web.App.Infrastructure.Apis
{
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
    }
}