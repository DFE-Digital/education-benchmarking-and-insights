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
                TrustName = "ACME Trust " + q.Value,
                SchoolsInTrust = 12,
                TotalPupils = 345,
                TotalIncome = 123_456_789
            }) ?? [];

        return Task.FromResult(ApiResult.Ok(stubCompanies));
    }
}

public interface ITrustInsightApi
{
    Task<ApiResult> GetCharacteristicsAsync(ApiQuery? query = null);
}