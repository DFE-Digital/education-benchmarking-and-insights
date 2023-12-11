namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class BenchmarkApi : BaseApi
{
    private const string BenchmarkSet = "api/benchmark-set";
    
    public BenchmarkApi(HttpClient httpClient, string? key = default) : base(httpClient, key)
    {
    }
    
    public async Task<ApiResult> Post(PostBenchmarkSetRequest request)
    {
        return await PostAsync($"{BenchmarkSet}", new JsonContent(request));
    }
}