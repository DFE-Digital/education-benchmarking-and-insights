using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class BenchmarkApi(HttpClient httpClient, string? key = default) : BaseApi(httpClient, key), IBenchmarkApi
{
    public async Task<ApiResult> CreateComparatorSet(PostBenchmarkSetRequest? request = default)
    {
        var schools = new School[]
        {
            new() { Urn = "140558", Name = "St Joseph's Catholic Primary School, Moorthorpe" },
            new() { Urn = "143633", Name = "St Gregory's Catholic Primary School" },
            new() { Urn = "142769", Name = "Horninglow Primary: A De Ferrers Trust Academy" },
            new() { Urn = "141155", Name = "St Joseph's Catholic Primary School, Banbury" },
            new() { Urn = "142424", Name = "Elm Road Primary School" },
            new() { Urn = "146726", Name = "Braybrook Primary Academy" },
            new() { Urn = "141197", Name = "Sandfield Primary School" },
            new() { Urn = "141634", Name = "Robin Hood Primary And Nursery School" },
            new() { Urn = "139696", Name = "Wells Free School" },
            new() { Urn = "140327", Name = "Green Oaks Primary Academy" },
            new() { Urn = "147334", Name = "St Edward's Catholic Primary School - Kettering" },
            new() { Urn = "147380", Name = "Ashbrook School" },
            new() { Urn = "143226", Name = "St George's Primary School" },
            new() { Urn = "142197", Name = "Good Shepherd Catholic School" },
            new() { Urn = "140183", Name = "St Thomas Cantilupe Cofe Academy" }
        };

        return ApiResult.Ok(new ComparatorSet<School> { TotalResults = schools.Length, Results = schools });
    }

    public async Task<ApiResult> GetFreeSchoolMealBandings(ApiQuery? query = null)
    {
        return await GetAsync($"api/free-school-meal/bandings{query?.ToQueryString()}");
    }

    public async Task<ApiResult> GetSchoolSizeBandings(ApiQuery? query = null)
    {
        return await GetAsync($"api/school-size/bandings{query?.ToQueryString()}");
    }

    public async Task<ApiResult> UpsertFinancialPlan(PutFinancialPlanRequest request)
    {
        return await PutAsync($"api/financial-plan/{request.Urn}/{request.Year}", new JsonContent(request));
    }

    public async Task<ApiResult> GetFinancialPlan(string urn, int year)
    {
        return await GetAsync($"api/financial-plan/{urn}/{year}");
    }
}

public interface IBenchmarkApi
{
    Task<ApiResult> CreateComparatorSet(PostBenchmarkSetRequest? request = default);
    Task<ApiResult> GetFreeSchoolMealBandings(ApiQuery? query = null);
    Task<ApiResult> GetSchoolSizeBandings(ApiQuery? query = null);
    Task<ApiResult> UpsertFinancialPlan(PutFinancialPlanRequest request);
    Task<ApiResult> GetFinancialPlan(string urn, int year);
}