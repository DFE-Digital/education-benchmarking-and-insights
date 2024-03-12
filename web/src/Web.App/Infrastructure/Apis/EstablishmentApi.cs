namespace Web.App.Infrastructure.Apis
{
    public class EstablishmentApi(HttpClient httpClient, string? key = default)
        : ApiBase(httpClient, key), IEstablishmentApi
    {
        public Task<ApiResult> GetSchool(string? identifier)
        {
            return GetAsync($"api/school/{identifier}");
        }

        public Task<ApiResult> GetTrust(string? identifier)
        {
            return GetAsync($"api/trust/{identifier}");
        }

        public Task<ApiResult> GetTrustSchools(string? identifier)
        {
            return GetAsync($"api/trust/{identifier}/schools");
        }

        public Task<ApiResult> SuggestSchools(string search)
        {
            return SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("api/schools/suggest", UriKind.Relative),
                Content = new JsonContent(new { SearchText = search, Size = 10, SuggesterName = "school-suggester" })
            });
        }

        public Task<ApiResult> SuggestTrusts(string search)
        {
            return SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("api/trusts/suggest", UriKind.Relative),
                Content = new JsonContent(new { SearchText = search, Size = 10, SuggesterName = "trust-suggester" })
            });
        }

        public Task<ApiResult> SuggestOrganisations(string search)
        {
            return SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("api/organisations/suggest", UriKind.Relative),
                Content = new JsonContent(new { SearchText = search, Size = 10, SuggesterName = "organisation-suggester" })
            });
        }
    }

    public interface IEstablishmentApi
    {
        Task<ApiResult> GetSchool(string? identifier);
        Task<ApiResult> GetTrust(string? identifier);
        Task<ApiResult> SuggestSchools(string search);
        Task<ApiResult> SuggestTrusts(string search);
        Task<ApiResult> SuggestOrganisations(string search);
        Task<ApiResult> GetTrustSchools(string id);
    }
}