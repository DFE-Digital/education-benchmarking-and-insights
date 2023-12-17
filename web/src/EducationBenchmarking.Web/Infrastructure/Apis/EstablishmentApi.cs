namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class EstablishmentApi : BaseApi, IEstablishmentApi
{
    public EstablishmentApi(HttpClient httpClient, string? key = default) : base(httpClient, key)
    {
    }
    
    public Task<ApiResult> Get(string identifier)
    {
        return GetAsync($"api/school/{identifier}");
    }

    public Task<ApiResult> SuggestSchools(string search, CancellationToken cancellation)
    {
        return SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("api/schools/suggest", UriKind.Relative),
            Content = new JsonContent(new { SearchText = search, Size = 10, SuggesterName= "school-suggester" })
        }, cancellation);
    }
}

public interface IEstablishmentApi
{
    Task<ApiResult> Get(string identifier);
    Task<ApiResult> SuggestSchools(string search, CancellationToken cancellation);
}