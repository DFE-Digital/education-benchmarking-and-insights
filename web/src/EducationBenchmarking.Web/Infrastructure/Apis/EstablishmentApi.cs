namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class EstablishmentApi : BaseApi, IEstablishmentApi
{
    private const string SchoolRoute = "api/school";
    
    public EstablishmentApi(HttpClient httpClient, string? key = default) : base(httpClient, key)
    {
    }
    
    public async Task<ApiResult> Get(string identifier)
    {
        return await GetAsync($"{SchoolRoute}/{identifier}");
    }
}

public interface IEstablishmentApi
{
    Task<ApiResult> Get(string identifier);
}