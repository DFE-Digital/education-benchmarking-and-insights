namespace Web.App.Infrastructure.Apis.NonFinancial;

public class EducationHealthCarePlansApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IEducationHealthCarePlansApi
{
    public Task<ApiResult> GetEducationHealthCarePlans(ApiQuery? query = null, CancellationToken cancellationToken = default)
    {
        return GetAsync($"{Api.EducationHealthCarePlans.LocalAuthorities}{query?.ToQueryString()}", cancellationToken);
    }

    public Task<ApiResult> GetEducationHealthCarePlansHistory(ApiQuery? query = null, CancellationToken cancellationToken = default)
    {
        return GetAsync($"{Api.EducationHealthCarePlans.LocalAuthoritiesHistory}{query?.ToQueryString()}", cancellationToken);
    }
}

public interface IEducationHealthCarePlansApi
{
    Task<ApiResult> GetEducationHealthCarePlans(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetEducationHealthCarePlansHistory(ApiQuery? query = null, CancellationToken cancellationToken = default);
}