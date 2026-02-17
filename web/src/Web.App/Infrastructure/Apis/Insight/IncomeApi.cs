namespace Web.App.Infrastructure.Apis.Insight;

public interface IIncomeApi
{
    Task<ApiResult> School(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
}

public class IncomeApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IIncomeApi
{
    public async Task<ApiResult> School(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Income.School(urn)}{query?.ToQueryString()}", cancellationToken);
}