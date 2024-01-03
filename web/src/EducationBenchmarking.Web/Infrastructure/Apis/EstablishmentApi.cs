namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class EstablishmentApi : BaseApi, IEstablishmentApi
{
    public EstablishmentApi(HttpClient httpClient, string? key = default) : base(httpClient, key)
    {
    }
    
    public Task<ApiResult> GetSchool(string identifier)
    {
        return GetAsync($"api/school/{identifier}");
    }
    
    public Task<ApiResult> GetTrust(string identifier)
    {
        return GetAsync($"api/trust/{identifier}");
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
    
    public Task<ApiResult> SuggestTrusts(string search, CancellationToken cancellation)
    {
        return SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("api/trusts/suggest", UriKind.Relative),
            Content = new JsonContent(new { SearchText = search, Size = 10, SuggesterName= "trust-suggester" })
        }, cancellation);
    }
    
    public Task<ApiResult> SuggestOrganisations(string search, CancellationToken cancellation)
    {
        return SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("api/organisations/suggest", UriKind.Relative),
            Content = new JsonContent(new { SearchText = search, Size = 10, SuggesterName= "organisation-suggester" })
        }, cancellation);
    }
}

public interface IEstablishmentApi
{
    Task<ApiResult> GetSchool(string identifier);
    Task<ApiResult> GetTrust(string identifier);
    Task<ApiResult> SuggestSchools(string search, CancellationToken cancellation);
    Task<ApiResult> SuggestTrusts(string search, CancellationToken cancellation);
    Task<ApiResult> SuggestOrganisations(string search, CancellationToken cancellation);
}