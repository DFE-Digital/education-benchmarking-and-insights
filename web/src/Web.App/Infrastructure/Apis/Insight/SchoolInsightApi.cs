namespace Web.App.Infrastructure.Apis.Insight;

[Obsolete(message: "Use SchoolApi instead.")]
public class SchoolInsightApi(ISchoolApi schoolApi) : ISchoolInsightApi
{
    public async Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null, CancellationToken cancellationToken = default) => await schoolApi.QueryAsync(query, cancellationToken);
    public async Task<ApiResult> GetCharacteristicsAsync(string urn, CancellationToken cancellationToken = default) => await schoolApi.CharacteristicsAsync(urn, cancellationToken);
}

[Obsolete(message: "Use ISchoolApi instead.")]
public interface ISchoolInsightApi
{
    Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null, CancellationToken cancellationToken = default);
    Task<ApiResult> GetCharacteristicsAsync(string urn, CancellationToken cancellationToken = default);
}