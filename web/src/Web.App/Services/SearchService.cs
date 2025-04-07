using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface ISearchService
{
    Task<SearchResponse<SchoolSummary>> SchoolSearch(string? term, int? pageSize = null, int? page = null, Dictionary<string, IEnumerable<string>>? filters = null, (string Field, string Order)? orderBy = null);
}

public class SearchService(IEstablishmentApi establishmentApi) : ISearchService
{
    public async Task<SearchResponse<SchoolSummary>> SchoolSearch(string? term, int? pageSize = null, int? page = null, Dictionary<string, IEnumerable<string>>? filters = null, (string Field, string Order)? orderBy = null)
    {
        List<(string Field, string Filter)>? flattenedFilters = null;
        if (filters is { Keys.Count: > 0, Values.Count: > 0 })
        {
            flattenedFilters = [];
            flattenedFilters.AddRange(filters.Keys.SelectMany(key => filters[key], (key, value) => (key, value)));
        }

        return await establishmentApi.SearchSchools(SearchRequest.Create(term, pageSize, page, flattenedFilters, orderBy))
            .GetResultOrThrow<SearchResponse<SchoolSummary>>();
    }
}