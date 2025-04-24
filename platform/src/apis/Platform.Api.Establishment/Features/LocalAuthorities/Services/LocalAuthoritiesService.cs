using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Requests;
using Platform.Domain;
using Platform.Infrastructure;
using Platform.Search;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Establishment.Features.LocalAuthorities.Services;

public interface ILocalAuthoritiesService
{
    Task<SuggestResponse<LocalAuthoritySummary>> LocalAuthoritiesSuggestAsync(LocalAuthoritySuggestRequest request);
    Task<LocalAuthority?> GetAsync(string code);
    Task<LocalAuthorityStatisticalNeighboursResponse?> GetStatisticalNeighboursAsync(string identifier);
    Task<IEnumerable<LocalAuthority>> GetAllAsync();
    Task<SearchResponse<LocalAuthoritySummary>> LocalAuthoritiesSearchAsync(SearchRequest request);
}

[ExcludeFromCodeCoverage]
public class LocalAuthoritiesService(
    [FromKeyedServices(ResourceNames.Search.Indexes.LocalAuthority)] IIndexClient client,
    IDatabaseFactory dbFactory)
    : SearchService<LocalAuthoritySummary>(client), ILocalAuthoritiesService
{
    public async Task<LocalAuthority?> GetAsync(string code)
    {
        var laBuilder = new LocalAuthorityQuery()
            .WhereCodeEqual(code);

        using var conn = await dbFactory.GetConnection();
        var localAuthority = await conn.QueryFirstOrDefaultAsync<LocalAuthority>(laBuilder);
        if (localAuthority is null)
        {
            return null;
        }

        var schoolsBuilder = new SchoolQuery(LocalAuthoritySchool.Fields)
            .WhereLaCodeEqual(code)
            .WhereFinanceTypeEqual(FinanceType.Maintained)
            .WhereUrnInCurrentFinances();

        localAuthority.Schools = await conn.QueryAsync<LocalAuthoritySchool>(schoolsBuilder);
        return localAuthority;
    }

    public Task<SuggestResponse<LocalAuthoritySummary>> LocalAuthoritiesSuggestAsync(LocalAuthoritySuggestRequest request)
    {
        var fields = new[] { nameof(LocalAuthority.Code), nameof(LocalAuthority.Name) };

        return SuggestAsync(request, CreateFilterExpression, fields);

        string? CreateFilterExpression()
        {
            return request.Exclude is not { Length: > 0 }
                ? null
                : $"({string.Join(") and ( ", request.Exclude.Select(a => $"{nameof(LocalAuthority.Name)} ne '{a}'"))})";
        }
    }

    public async Task<LocalAuthorityStatisticalNeighboursResponse?> GetStatisticalNeighboursAsync(string code)
    {
        using var conn = await dbFactory.GetConnection();
        var laBuilder = new LocalAuthorityStatisticalNeighbourQuery()
            .WhereLaCodeEqual(code);

        var results = await conn.QueryAsync<LocalAuthorityStatisticalNeighbour>(laBuilder);
        return LocalAuthorityStatisticalNeighbourResponseFactory.Create(results.ToArray());
    }

    public async Task<IEnumerable<LocalAuthority>> GetAllAsync()
    {
        var laBuilder = new LocalAuthorityQuery()
            .OrderBy(nameof(LocalAuthority.Name));

        using var conn = await dbFactory.GetConnection();
        var localAuthorities = await conn.QueryAsync<LocalAuthority>(laBuilder);
        return localAuthorities;
    }

    public Task<SearchResponse<LocalAuthoritySummary>> LocalAuthoritiesSearchAsync(SearchRequest request) => SearchAsync(request, request.FilterExpression);
}