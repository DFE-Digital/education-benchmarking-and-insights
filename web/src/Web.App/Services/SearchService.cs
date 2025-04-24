using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface ISearchService
{
    Task<SearchResponse<SchoolSummary>> SchoolSearch(string? term, int? pageSize = null, int? page = null, SearchFilters? filters = null, SearchOrderBy? orderBy = null);
    Task<SearchResponse<TrustSummary>> TrustSearch(string? term, int? pageSize = null, int? page = null, SearchOrderBy? orderBy = null);
    Task<SearchResponse<LocalAuthoritySummary>> LocalAuthoritySearch(string? term, int? pageSize = null, int? page = null, SearchOrderBy? orderBy = null);
}

public class SearchFilters : Dictionary<string, IEnumerable<string>>
{
    public SearchFilters() { }

    public SearchFilters(string field, string[] values)
    {
        this[field] = values;
    }
}

public record SearchOrderBy(string Field, string Order);

public class SearchService(IEstablishmentApi establishmentApi) : ISearchService
{
    public async Task<SearchResponse<SchoolSummary>> SchoolSearch(string? term, int? pageSize = null, int? page = null, SearchFilters? filters = null, SearchOrderBy? orderBy = null)
    {
        List<(string Field, string Filter)>? flattenedFilters = null;
        if (filters is { Keys.Count: > 0, Values.Count: > 0 })
        {
            flattenedFilters = [];
            flattenedFilters.AddRange(filters.Keys.SelectMany(key => filters[key], (key, value) => (key, value)));
        }

        var request = SearchRequest.Create(term, pageSize, page, flattenedFilters, orderBy == null ? null : (orderBy.Field, orderBy.Order));
        return await establishmentApi.SearchSchools(request)
            .GetResultOrThrow<SearchResponse<SchoolSummary>>();
    }

    public async Task<SearchResponse<TrustSummary>> TrustSearch(string? term, int? pageSize = null, int? page = null, SearchOrderBy? orderBy = null)
    {
        var request = SearchRequest.Create(term, pageSize, page, null, orderBy == null ? null : (orderBy.Field, orderBy.Order));
        return await establishmentApi.SearchTrusts(request)
            .GetResultOrThrow<SearchResponse<TrustSummary>>();
    }

    public async Task<SearchResponse<LocalAuthoritySummary>> LocalAuthoritySearch(string? term, int? pageSize = null, int? page = null, SearchOrderBy? orderBy = null)
    {
        var request = SearchRequest.Create(term, pageSize, page, null, orderBy == null ? null : (orderBy.Field, orderBy.Order));
        return await establishmentApi.SearchLocalAuthorities(request)
            .GetResultOrThrow<SearchResponse<LocalAuthoritySummary>>();
    }
}