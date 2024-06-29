namespace Web.App.Infrastructure.Apis.Benchmark;

public class ComparatorApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IComparatorApi
{
    public Task<ApiResult> CreateSchoolsAsync(PostSchoolComparatorsRequest request) => PostAsync(Api.Comparator.Schools, new JsonContent(request));

    public Task<ApiResult> CreateTrustsAsync(PostTrustComparatorsRequest request) => PostAsync(Api.Comparator.Trusts, new JsonContent(request));
}

public interface IComparatorApi
{
    Task<ApiResult> CreateSchoolsAsync(PostSchoolComparatorsRequest request);
    Task<ApiResult> CreateTrustsAsync(PostTrustComparatorsRequest request);
}