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
    Task<SuggestResponse<LocalAuthority>> SuggestAsync(LocalAuthoritySuggestRequest request);
    Task<LocalAuthority?> GetAsync(string code);
    Task<LocalAuthorityStatisticalNeighbours?> GetStatisticalNeighbours(string identifier);
}

[ExcludeFromCodeCoverage]
public class LocalAuthoritiesService(
    [FromKeyedServices(ResourceNames.Search.Indexes.LocalAuthority)] IIndexClient client,
    IDatabaseFactory dbFactory)
    : SearchService<LocalAuthority>(client), ILocalAuthoritiesService
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

    public Task<SuggestResponse<LocalAuthority>> SuggestAsync(LocalAuthoritySuggestRequest request)
    {
        var fields = new[]
        {
            nameof(LocalAuthority.Code),
            nameof(LocalAuthority.Name)
        };

        return SuggestAsync(request, CreateFilterExpression, fields);

        string? CreateFilterExpression()
        {
            return request.Exclude is not { Length: > 0 }
                ? null
                : $"({string.Join(") and ( ", request.Exclude.Select(a => $"{nameof(LocalAuthority.Name)} ne '{a}'"))})";
        }
    }

    // stubbed implementation for the time being
    public async Task<LocalAuthorityStatisticalNeighbours?> GetStatisticalNeighbours(string code)
    {
        var laBuilder = new LocalAuthorityQuery()
            .WhereCodeEqual(code);

        using var conn = await dbFactory.GetConnection();
        var localAuthority = await conn.QueryFirstOrDefaultAsync<LocalAuthority>(laBuilder);
        if (localAuthority is null)
        {
            return null;
        }

        var neighbours = new List<LocalAuthorityStatisticalNeighbour>();
        for (var i = 0; i < 10; i++)
        {
            var stubbedCode = (200 + i).ToString();
            var value = new LocalAuthorityStatisticalNeighbour
            {
                Code = stubbedCode,
                Name = $"Local authority {stubbedCode}",
                Order = i + 1
            };

            neighbours.Add(value);
        }

        return new LocalAuthorityStatisticalNeighbours
        {
            Code = localAuthority.Code,
            Name = localAuthority.Name,
            StatisticalNeighbours = neighbours
        };
    }
}