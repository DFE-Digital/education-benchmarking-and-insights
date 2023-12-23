using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class BenchmarkApi : BaseApi, IBenchmarkApi
{
    public BenchmarkApi(HttpClient httpClient, string? key = default) : base(httpClient, key)
    {
    }
    
    public async Task<ApiResult> CreateComparatorSet(PostBenchmarkSetRequest? request = default)
    {
        
        var urns = new[]
        {
            "140558", "143633", "142769", "141155", "142424", 
            "146726", "141197", "141634", "139696", "140327",
            "147334", "147380", "143226", "142197", "140183"
        };
        var schools = urns.Select(x => new School { Urn = x });

        return ApiResult.Ok(new ComparatorSet<School> { TotalResults = urns.Length, Results = schools});
        //return await PostAsync("api/benchmark-set", new JsonContent(request));
    }
}

public interface IBenchmarkApi
{
    Task<ApiResult> CreateComparatorSet(PostBenchmarkSetRequest? request = default);
}