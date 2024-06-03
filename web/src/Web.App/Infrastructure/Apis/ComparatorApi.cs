namespace Web.App.Infrastructure.Apis;

public class ComparatorApi(HttpClient httpClient, string? key = default)
    : ApiBase(httpClient, key), IComparatorApi
{
    public Task<ApiResult> CreateSchoolsAsync(PostSchoolComparatorsRequest request)
    {
        return PostAsync("api/comparators/schools", new JsonContent(request));
    }
}

public interface IComparatorApi
{
    Task<ApiResult> CreateSchoolsAsync(PostSchoolComparatorsRequest request);
}