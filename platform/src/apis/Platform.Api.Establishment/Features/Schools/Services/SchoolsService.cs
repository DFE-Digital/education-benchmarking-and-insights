using System.Threading;
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
    Task<School?> GetAsync(string urn, CancellationToken cancellationToken = default);
    Task<SchoolStatus?> GetSchoolStatusAsync(string urn, CancellationToken cancellationToken = default);
    Task<SuggestResponse<SchoolSummary>> SchoolsSuggestAsync(SchoolSuggestRequest request, CancellationToken cancellationToken = default);
    Task<SearchResponse<SchoolSummary>> SchoolsSearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
}

public class SchoolsService(
    [FromKeyedServices(ResourceNames.Search.Indexes.School)] IIndexClient client,
    IDatabaseFactory dbFactory)
    : SearchService<SchoolSummary>(client), ISchoolsService
{
    public async Task<School?> GetAsync(string urn, CancellationToken cancellationToken = default)
    {
        var schoolBuilder = new SchoolQuery()
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        var school = await conn.QueryFirstOrDefaultAsync<School>(schoolBuilder, cancellationToken);

        if (school == null || string.IsNullOrEmpty(school.FederationLeadURN))
        {
            return school;
        }

        var childSchoolsBuilder = new SchoolQuery()
            .WhereFederationLeadUrnEqual(urn);

        school.FederationSchools = await conn.QueryAsync<School>(childSchoolsBuilder, cancellationToken);

        return school;
    }

    public async Task<SchoolStatus?> GetSchoolStatusAsync(string urn, CancellationToken cancellationToken = default)
    {
        var schoolBuilder = new SchoolStatusQuery()
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<SchoolStatus>(schoolBuilder, cancellationToken);
    }

    public Task<SuggestResponse<SchoolSummary>> SchoolsSuggestAsync(SchoolSuggestRequest request, CancellationToken cancellationToken = default)
    {
        var fields = new[] { nameof(SchoolSummary.SchoolName), nameof(SchoolSummary.URN), nameof(SchoolSummary.AddressStreet), nameof(SchoolSummary.AddressLocality), nameof(SchoolSummary.AddressLine3), nameof(SchoolSummary.AddressTown), nameof(SchoolSummary.AddressCounty), nameof(SchoolSummary.AddressPostcode) };

        return SuggestAsync(request, request.FilterExpression, fields, cancellationToken);
    }

    public Task<SearchResponse<SchoolSummary>> SchoolsSearchAsync(SearchRequest request, CancellationToken cancellationToken = default) => SearchAsync(request, request.FilterExpression, cancellationToken: cancellationToken);
}