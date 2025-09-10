namespace Web.App.Infrastructure.Apis.Benchmark;

public class ComparatorSetApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), IComparatorSetApi
{
    public async Task<ApiResult> GetDefaultSchoolAsync(string urn, CancellationToken cancellationToken = default) => await GetAsync(Api.ComparatorSet.SchoolDefault(urn), cancellationToken);

    public async Task<ApiResult> GetCustomSchoolAsync(string urn, string identifier, CancellationToken cancellationToken = default) => await GetAsync(Api.ComparatorSet.SchoolCustom(urn, identifier), cancellationToken);

    public async Task<ApiResult> RemoveUserDefinedSchoolAsync(string urn, string? identifier) => await DeleteAsync(Api.ComparatorSet.SchoolUserDefined(urn, identifier));

    public async Task<ApiResult> UpsertUserDefinedSchoolAsync(string urn, PostComparatorSetUserDefinedRequest request) => await PostAsync(Api.ComparatorSet.SchoolUserDefined(urn), new JsonContent(request));

    public async Task<ApiResult> GetUserDefinedTrustAsync(string companyNumber, string? identifier, CancellationToken cancellationToken = default) => await GetAsync(Api.ComparatorSet.TrustUserDefined(companyNumber, identifier), cancellationToken);

    public async Task<ApiResult> RemoveUserDefinedTrustAsync(string companyNumber, string? identifier) => await DeleteAsync(Api.ComparatorSet.TrustUserDefined(companyNumber, identifier));

    public async Task<ApiResult> UpsertUserDefinedTrustAsync(string companyNumber, PostComparatorSetUserDefinedRequest request) => await PostAsync(Api.ComparatorSet.TrustUserDefined(companyNumber), new JsonContent(request));

    public async Task<ApiResult> GetUserDefinedSchoolAsync(string urn, string? identifier, CancellationToken cancellationToken = default) => await GetAsync(Api.ComparatorSet.SchoolUserDefined(urn, identifier), cancellationToken);
}

public interface IComparatorSetApi
{
    Task<ApiResult> GetDefaultSchoolAsync(string urn, CancellationToken cancellationToken = default);
    Task<ApiResult> GetCustomSchoolAsync(string urn, string identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> GetUserDefinedSchoolAsync(string urn, string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> UpsertUserDefinedSchoolAsync(string urn, PostComparatorSetUserDefinedRequest request);
    Task<ApiResult> RemoveUserDefinedSchoolAsync(string urn, string? identifier);
    Task<ApiResult> GetUserDefinedTrustAsync(string companyNumber, string? identifier, CancellationToken cancellationToken = default);
    Task<ApiResult> RemoveUserDefinedTrustAsync(string companyNumber, string? identifier);
    Task<ApiResult> UpsertUserDefinedTrustAsync(string companyNumber, PostComparatorSetUserDefinedRequest request);
}