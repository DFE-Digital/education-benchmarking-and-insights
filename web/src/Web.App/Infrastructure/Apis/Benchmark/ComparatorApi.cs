namespace Web.App.Infrastructure.Apis.Benchmark;

public class ComparatorApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IComparatorApi
{
    public Task<ApiResult> CreateSchoolsAsync(string urn, PostSchoolComparatorsRequest request)
        => PostAsync(Api.Comparator.Schools(urn), new JsonContent(request));

    public Task<ApiResult> CreateTrustsAsync(string companyNumber, PostTrustComparatorsRequest request)
        => PostAsync(Api.Comparator.Trusts(companyNumber), new JsonContent(request));
}

public interface IComparatorApi
{
    Task<ApiResult> CreateSchoolsAsync(string urn, PostSchoolComparatorsRequest request);
    Task<ApiResult> CreateTrustsAsync(string companyNumber, PostTrustComparatorsRequest request);
}