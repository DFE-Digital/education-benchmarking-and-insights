namespace Web.App.Infrastructure.Apis.NonFinancial;

[Obsolete(message: "Use LocalAuthorityApi instead.")]
public class EducationHealthCarePlansApi(ILocalAuthorityApi localAuthorityApi) : IEducationHealthCarePlansApi
{
    public Task<ApiResult> GetEducationHealthCarePlans(ApiQuery? query = null, CancellationToken cancellationToken = default) => localAuthorityApi.QueryEhcpAsync(query, cancellationToken);
    public Task<ApiResult> GetEducationHealthCarePlansHistory(ApiQuery? query = null, CancellationToken cancellationToken = default) => localAuthorityApi.QueryEhcpHistoryAsync(query, cancellationToken);
}

[Obsolete(message: "Use ILocalAuthorityApi instead.")]
public interface IEducationHealthCarePlansApi
{
    Task<ApiResult> GetEducationHealthCarePlans(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetEducationHealthCarePlansHistory(ApiQuery? query = null, CancellationToken cancellationToken = default);
}