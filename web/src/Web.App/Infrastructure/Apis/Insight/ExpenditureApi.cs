namespace Web.App.Infrastructure.Apis.Insight;

public interface IExpenditureApi
{
    Task<ApiResult> School(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SchoolCustom(string? urn, string? identifier, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SchoolHistoryComparatorSetAverage(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SchoolHistoryNationalAverage(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QuerySchools(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryTrusts(ApiQuery? query = null, CancellationToken cancellationToken = default);
}

public class ExpenditureApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IExpenditureApi
{
    public async Task<ApiResult> QuerySchools(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Expenditure.Schools}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> School(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Expenditure.School(urn)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> SchoolCustom(string? urn, string? identifier, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Expenditure.SchoolCustom(urn, identifier)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> SchoolHistory(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Expenditure.SchoolHistory(urn)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> SchoolHistoryComparatorSetAverage(string? urn, ApiQuery? query = null, CancellationToken cancellationToken = default) =>
        await GetAsync($"{Api.Expenditure.SchoolHistoryComparatorSetAverage(urn)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> SchoolHistoryNationalAverage(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Expenditure.SchoolHistoryNationalAverage}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> Trust(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Expenditure.Trust(companyNo)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> TrustHistory(string? companyNo, ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Expenditure.TrustHistory(companyNo)}{query?.ToQueryString()}", cancellationToken);

    public async Task<ApiResult> QueryTrusts(ApiQuery? query = null, CancellationToken cancellationToken = default) => await GetAsync($"{Api.Expenditure.Trusts}{query?.ToQueryString()}", cancellationToken);
}