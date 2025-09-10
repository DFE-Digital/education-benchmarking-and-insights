using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
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
    Task<SuggestResponse<LocalAuthoritySummary>> LocalAuthoritiesSuggestAsync(LocalAuthoritySuggestRequest request, CancellationToken cancellationToken = default);
    Task<LocalAuthority?> GetAsync(string code, CancellationToken cancellationToken = default);
    Task<LocalAuthorityStatisticalNeighboursResponse?> GetStatisticalNeighboursAsync(string identifier, CancellationToken cancellationToken = default);
    Task<IEnumerable<LocalAuthority>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SearchResponse<LocalAuthoritySummary>> LocalAuthoritiesSearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class LocalAuthoritiesService(
    [FromKeyedServices(ResourceNames.Search.Indexes.LocalAuthority)] IIndexClient client,
    IDatabaseFactory dbFactory)
    : SearchService<LocalAuthoritySummary>(client), ILocalAuthoritiesService
{
    public async Task<LocalAuthority?> GetAsync(string code, CancellationToken cancellationToken = default)
    {
        var laBuilder = new LocalAuthorityQuery()
            .WhereCodeEqual(code);

        using var conn = await dbFactory.GetConnection();
        var localAuthority = await conn.QueryFirstOrDefaultAsync<LocalAuthority>(laBuilder, cancellationToken);
        if (localAuthority is null)
        {
            return null;
        }

        var schoolsBuilder = new SchoolQuery(LocalAuthoritySchool.Fields)
            .WhereLaCodeEqual(code)
            .WhereFinanceTypeEqual(FinanceType.Maintained)
            .WhereUrnInCurrentFinances();

        localAuthority.Schools = await conn.QueryAsync<LocalAuthoritySchool>(schoolsBuilder, cancellationToken);
        return localAuthority;
    }

    public Task<SuggestResponse<LocalAuthoritySummary>> LocalAuthoritiesSuggestAsync(LocalAuthoritySuggestRequest request, CancellationToken cancellationToken = default)
    {
        var fields = new[]
        {
            nameof(LocalAuthority.Code),
            nameof(LocalAuthority.Name)
        };

        return SuggestAsync(request, CreateFilterExpression, fields, cancellationToken);

        string? CreateFilterExpression()
        {
            return request.Exclude is not { Length: > 0 }
                ? null
                : $"({string.Join(") and ( ", request.Exclude.Select(a => $"{nameof(LocalAuthority.Name)} ne '{a}'"))})";
        }
    }

    public async Task<LocalAuthorityStatisticalNeighboursResponse?> GetStatisticalNeighboursAsync(string code, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var laBuilder = new LocalAuthorityStatisticalNeighbourQuery()
            .WhereLaCodeEqual(code);

        var results = await conn.QueryAsync<LocalAuthorityStatisticalNeighbour>(laBuilder, cancellationToken);
        return LocalAuthorityStatisticalNeighbourResponseFactory.Create(results.ToArray());
    }

    public async Task<IEnumerable<LocalAuthority>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var laBuilder = new LocalAuthorityQuery()
            .OrderBy(nameof(LocalAuthority.Name));

        using var conn = await dbFactory.GetConnection();
        var localAuthorities = await conn.QueryAsync<LocalAuthority>(laBuilder, cancellationToken);
        return localAuthorities;
    }

    public Task<SearchResponse<LocalAuthoritySummary>> LocalAuthoritiesSearchAsync(SearchRequest request, CancellationToken cancellationToken = default) => SearchAsync(request, request.FilterExpression, cancellationToken: cancellationToken);
}