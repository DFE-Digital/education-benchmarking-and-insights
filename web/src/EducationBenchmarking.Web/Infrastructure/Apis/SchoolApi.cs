namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class SchoolApi :BaseApi
{
    public SchoolApi(HttpClient httpClient, string? key = default) : base(httpClient, key)
    {
    }
}