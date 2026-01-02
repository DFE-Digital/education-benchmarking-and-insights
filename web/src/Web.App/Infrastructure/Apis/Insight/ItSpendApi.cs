namespace Web.App.Infrastructure.Apis.Insight;

[Obsolete(message: "Use ITrustApi or ISchoolApi instead.")]
public interface IItSpendApi
{
    Task<ApiResult> QuerySchools(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> QueryTrusts(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> TrustForecast(string? companyNumber, CancellationToken cancellationToken = default);
}

[Obsolete(message: "Use TrustApi or SchoolApi instead.")]
public class ItSpendApi(ITrustApi trustApi, ISchoolApi schoolApi) : IItSpendApi
{
    public async Task<ApiResult> QuerySchools(ApiQuery? query = null, CancellationToken cancellationToken = default) => await schoolApi.QueryItSpendingAsync(query, cancellationToken);
    public async Task<ApiResult> QueryTrusts(ApiQuery? query = null, CancellationToken cancellationToken = default) => await trustApi.QueryItSpendingAsync(query, cancellationToken);
    public async Task<ApiResult> TrustForecast(string? companyNumber, CancellationToken cancellationToken = default) => await trustApi.ItSpendingForecastAsync(companyNumber, cancellationToken);
}