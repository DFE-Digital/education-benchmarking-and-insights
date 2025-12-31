using System.Diagnostics.CodeAnalysis;

namespace Web.App.Infrastructure.Apis.Establishment;

[ExcludeFromCodeCoverage, Obsolete(message: "Use LocalAuthorityApi, SchoolApi or TrustApi instead.")]
public class EstablishmentApi(ILocalAuthorityApi localAuthorityApi, ISchoolApi schoolApi, ITrustApi trustApi) : IEstablishmentApi
{
    public Task<ApiResult> GetSchool(string? identifier, CancellationToken cancellationToken = default) => schoolApi.SingleAsync(identifier, cancellationToken);
    public Task<ApiResult> GetTrust(string? identifier, CancellationToken cancellationToken = default) => trustApi.SingleAsync(identifier, cancellationToken);
    public Task<ApiResult> GetLocalAuthority(string? identifier, CancellationToken cancellationToken = default) => localAuthorityApi.SingleAsync(identifier, cancellationToken);
    public Task<ApiResult> GetLocalAuthorityStatisticalNeighbours(string? identifier, CancellationToken cancellationToken = default) => localAuthorityApi.StatisticalNeighboursAsync(identifier, cancellationToken);
    public Task<ApiResult> GetLocalAuthorities(CancellationToken cancellationToken = default) => localAuthorityApi.QueryAsync(cancellationToken);
    public Task<ApiResult> SuggestSchools(string search, string[]? exclude = null, bool? excludeMissingFinancialData = null, CancellationToken cancellationToken = default) => schoolApi.SuggestAsync(search, exclude, excludeMissingFinancialData, cancellationToken);
    public Task<ApiResult> SuggestTrusts(string search, string[]? exclude = null, CancellationToken cancellationToken = default) => trustApi.SuggestAsync(search, exclude, cancellationToken);
    public Task<ApiResult> SuggestLocalAuthorities(string search, string[]? exclude = null, CancellationToken cancellationToken = default) => localAuthorityApi.SuggestAsync(search, exclude, cancellationToken);
    public Task<ApiResult> SearchSchools(SearchRequest request, CancellationToken cancellationToken = default) => schoolApi.SearchAsync(request, cancellationToken);
    public Task<ApiResult> SearchTrusts(SearchRequest request, CancellationToken cancellationToken = default) => trustApi.SearchAsync(request, cancellationToken);
    public Task<ApiResult> SearchLocalAuthorities(SearchRequest request, CancellationToken cancellationToken = default) => localAuthorityApi.SearchAsync(request, cancellationToken);
}

[Obsolete(message: "Use ILocalAuthorityApi, ISchoolApi or ITrustApi instead.")]
public interface IEstablishmentApi
{
    Task<ApiResult> GetSchool(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> GetTrust(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> GetLocalAuthority(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> GetLocalAuthorityStatisticalNeighbours(string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> GetLocalAuthorities(CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestSchools(string search, string[]? exclude = null, bool? excludeMissingFinancialData = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestTrusts(string search, string[]? exclude = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SuggestLocalAuthorities(string search, string[]? exclude = null, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchSchools(SearchRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchTrusts(SearchRequest request, CancellationToken cancellationToken = default);
    Task<ApiResult> SearchLocalAuthorities(SearchRequest request, CancellationToken cancellationToken = default);
}