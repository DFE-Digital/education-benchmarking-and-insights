namespace Web.App.Infrastructure.Apis.Establishment;

public class ComparatorApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IComparatorApi
{
    public Task<ApiResult> CreateSchoolsAsync(string urn, PostSchoolComparatorsRequest request)
        => PostAsync(Benchmark.Api.Comparator.Schools(urn), new JsonContent(request));

    public Task<ApiResult> CreateTrustsAsync(string companyNumber, PostTrustComparatorsRequest request)
        => PostAsync(Benchmark.Api.Comparator.Trusts(companyNumber), new JsonContent(request));
}

public interface IComparatorApi
{
    Task<ApiResult> CreateSchoolsAsync(string urn, PostSchoolComparatorsRequest request);
    
    [Obsolete("Needs to be updated to use new API structure")]
    Task<ApiResult> CreateTrustsAsync(string companyNumber, PostTrustComparatorsRequest request);
}