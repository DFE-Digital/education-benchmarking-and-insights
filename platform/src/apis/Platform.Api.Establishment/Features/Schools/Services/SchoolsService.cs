using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Requests;
using Platform.Infrastructure;
using Platform.Search;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Establishment.Features.Schools.Services;

public interface ISchoolsService
{
    Task<SuggestResponse<School>> SchoolsSuggestAsync(SchoolSuggestRequest request);
    Task<School?> GetAsync(string urn);
    Task<SearchResponse<School>> SchoolsSearchAsync(SearchRequest request);
}

[ExcludeFromCodeCoverage]
public class SchoolsService(
    [FromKeyedServices(ResourceNames.Search.Indexes.School)] IIndexClient client,
    IDatabaseFactory dbFactory)
    : SearchService<School>(client), ISchoolsService
{
    public async Task<School?> GetAsync(string urn)
    {
        var schoolBuilder = new SchoolQuery()
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        var school = await conn.QueryFirstOrDefaultAsync<School>(schoolBuilder);

        if (school == null || string.IsNullOrEmpty(school.FederationLeadURN))
        {
            return school;
        }

        var childSchoolsBuilder = new SchoolQuery()
            .WhereFederationLeadUrnEqual(urn);

        school.FederationSchools = await conn.QueryAsync<School>(childSchoolsBuilder);

        return school;
    }

    public Task<SuggestResponse<School>> SchoolsSuggestAsync(SchoolSuggestRequest request)
    {
        var fields = new[]
        {
            nameof(School.SchoolName),
            nameof(School.URN),
            nameof(School.AddressStreet),
            nameof(School.AddressLocality),
            nameof(School.AddressLine3),
            nameof(School.AddressTown),
            nameof(School.AddressCounty),
            nameof(School.AddressPostcode)
        };

        return SuggestAsync(request, CreateFilterExpression, fields);

        string? CreateFilterExpression()
        {
            return request.Exclude is not { Length: > 0 }
                ? null
                : $"({string.Join(") and ( ", request.Exclude.Select(a => $"URN ne '{a}'"))})";
        }
    }

    public Task<SearchResponse<School>> SchoolsSearchAsync(SearchRequest request)
    {
        var facets = new[]
        {
            nameof(School.OverallPhase),
        };

        var response = SearchAsync(request, CreateSearchFilterExpression, facets);

        return response;

        string? CreateSearchFilterExpression(FilterCriteria[]? filterCriteriaArray)
        {
            if (filterCriteriaArray == null || filterCriteriaArray.Length == 0)
                return null;

            return $"({string.Join(" or ", filterCriteriaArray.Select(f => $"{f.Field} eq '{f.Value}'"))})";
        }
    }
}