namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class BenchmarkApi : BaseApi
{
    public BenchmarkApi(HttpClient httpClient, string? key = default) : base(httpClient, key)
    {
    }
}