namespace Web.App.Infrastructure.Apis.Insight;

public interface IIncomeApi
{
    Task<ApiResult> School(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default);
}

public class IncomeApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IIncomeApi
{
    public async Task<ApiResult> School(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default)
    {
        return await GetAsync($"{Api.Income.School(urn)}{query?.ToQueryString()}", cancellationToken);
    }

    public async Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default)
    {
        return await GetAsync($"{Api.Income.SchoolHistory(urn)}{query?.ToQueryString()}", cancellationToken);
    }

    public async Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default)
    {
        return await GetAsync($"{Api.Income.TrustHistory(companyNo)}{query?.ToQueryString()}", cancellationToken);
    }
}