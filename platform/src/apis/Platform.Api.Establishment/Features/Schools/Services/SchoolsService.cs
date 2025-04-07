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
    Task<SuggestResponse<SchoolSummary>> SchoolsSuggestAsync(SchoolSuggestRequest request);
    Task<School?> GetAsync(string urn);
    Task<SearchResponse<SchoolSummary>> SchoolsSearchAsync(SearchRequest request);
}

[ExcludeFromCodeCoverage]
public class SchoolsService(
    [FromKeyedServices(ResourceNames.Search.Indexes.School)] IIndexClient client,
    IDatabaseFactory dbFactory)
    : SearchService<SchoolSummary>(client), ISchoolsService
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

    public Task<SuggestResponse<SchoolSummary>> SchoolsSuggestAsync(SchoolSuggestRequest request)
    {
        var fields = new[]
        {
            nameof(SchoolSummary.SchoolName),
            nameof(SchoolSummary.URN),
            nameof(SchoolSummary.AddressStreet),
            nameof(SchoolSummary.AddressLocality),
            nameof(SchoolSummary.AddressLine3),
            nameof(SchoolSummary.AddressTown),
            nameof(SchoolSummary.AddressCounty),
            nameof(SchoolSummary.AddressPostcode)
        };

        return SuggestAsync(request, request.FilterExpression, fields);
    }

    public Task<SearchResponse<SchoolSummary>> SchoolsSearchAsync(SearchRequest request)
    {
        var facets = new[]
        {
            nameof(SchoolSummary.OverallPhase),
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