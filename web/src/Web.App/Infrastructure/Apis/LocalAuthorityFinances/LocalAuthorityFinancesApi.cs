namespace Web.App.Infrastructure.Apis.LocalAuthorityFinances;

[Obsolete(message: "Use LocalAuthorityApi instead.")]
public class LocalAuthorityFinancesApi(ILocalAuthorityApi localAuthorityApi) : ILocalAuthorityFinancesApi
{
    public Task<ApiResult> GetHighNeeds(ApiQuery? query = null, CancellationToken cancellationToken = default) => localAuthorityApi.QueryHighNeedsAsync(query, cancellationToken);
    public Task<ApiResult> GetHighNeedsHistory(ApiQuery? query = null, CancellationToken cancellationToken = default) => localAuthorityApi.QueryHighNeedsHistoryAsync(query, cancellationToken);
    public Task<ApiResult> GetSchoolsFinance(string code, ApiQuery? query = null, CancellationToken cancellationToken = default) => localAuthorityApi.QueryMaintainedSchoolsFinanceAsync(code, query, cancellationToken);
    public Task<ApiResult> GetSchoolsWorkforce(string code, ApiQuery? query = null, CancellationToken cancellationToken = default) => localAuthorityApi.QueryMaintainedSchoolsWorkforceAsync(code, query, cancellationToken);
}

[Obsolete(message: "Use ILocalAuthorityApi instead.")]
public interface ILocalAuthorityFinancesApi
{
    Task<ApiResult> GetHighNeeds(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetHighNeedsHistory(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetSchoolsFinance(string code, ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetSchoolsWorkforce(string code, ApiQuery? query = null, CancellationToken cancellationToken = default);
}