namespace Web.App.Infrastructure.Apis;

public class EstablishmentApi(HttpClient httpClient, string? key = default)
    : ApiBase(httpClient, key), IEstablishmentApi
{
    public Task<ApiResult> GetSchool(string? identifier)
    {
        return GetAsync($"api/school/{identifier}");
    }

    public Task<ApiResult> QuerySchools(ApiQuery? query)
    {
        return GetAsync($"api/schools{query?.ToQueryString()}");
    }

    public Task<ApiResult> GetTrust(string? identifier)
    {
        return GetAsync($"api/trust/{identifier}");
    }

    public Task<ApiResult> GetLocalAuthority(string? identifier)
    {
        return GetAsync($"api/local-authority/{identifier}");
    }

    public Task<ApiResult> SuggestSchools(string search, ApiQuery? query = null)
    {
        return SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"api/schools/suggest{query?.ToQueryString()}", UriKind.Relative),
            Content = new JsonContent(new { SearchText = search, Size = 10, SuggesterName = "school-suggester" })
        });
    }

    public Task<ApiResult> SuggestTrusts(string search, ApiQuery? query = null)
    {
        return SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"api/trusts/suggest{query?.ToQueryString()}", UriKind.Relative),
            Content = new JsonContent(new { SearchText = search, Size = 10, SuggesterName = "trust-suggester" })
        });
    }

    public Task<ApiResult> SuggestLocalAuthorities(string search, ApiQuery? query = null)
    {
        return SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"api/local-authorities/suggest{query?.ToQueryString()}", UriKind.Relative),
            Content = new JsonContent(new { SearchText = search, Size = 10, SuggesterName = "local-authority-suggester" })
        });
    }
}

public interface IEstablishmentApi
{
    Task<ApiResult> GetSchool(string? identifier);
    Task<ApiResult> GetTrust(string? identifier);
    Task<ApiResult> GetLocalAuthority(string? identifier);
    Task<ApiResult> QuerySchools(ApiQuery? query);
    Task<ApiResult> SuggestSchools(string search, ApiQuery? query = null);
    Task<ApiResult> SuggestTrusts(string search, ApiQuery? query = null);
    Task<ApiResult> SuggestLocalAuthorities(string search, ApiQuery? query = null);
}