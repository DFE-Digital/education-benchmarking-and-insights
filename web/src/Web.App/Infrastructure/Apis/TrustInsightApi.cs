using Web.App.Domain;
namespace Web.App.Infrastructure.Apis;

public class TrustInsightApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ITrustInsightApi
{
    public Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null)
    {
        var stubCompanies = query?
            .Where(q => q.Key == "companyNumbers")
            .Select(q => new TrustCharacteristicUserDefined
            {
                CompanyNumber = q.Value,
                TrustName = "ACME Trust",
                Address = "Stub Street, Fakesville"
            }) ?? [];

        return Task.FromResult(ApiResult.Ok(stubCompanies));
    }
}

public interface ITrustInsightApi
{
    Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null);
}