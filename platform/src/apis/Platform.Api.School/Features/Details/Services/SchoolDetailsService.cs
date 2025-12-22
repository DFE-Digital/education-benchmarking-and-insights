using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.School.Features.Details.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.School.Features.Details.Services;

public interface ISchoolDetailsService
{
    Task<SchoolResponse?> GetAsync(string urn, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class SchoolDetailsService(IDatabaseFactory dbFactory) : ISchoolDetailsService
{
    public async Task<SchoolResponse?> GetAsync(string urn, CancellationToken cancellationToken = default)
    {
        var schoolBuilder = new SchoolQuery()
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        var school = await conn.QueryFirstOrDefaultAsync<SchoolResponse>(schoolBuilder, cancellationToken);

        if (school == null || string.IsNullOrEmpty(school.FederationLeadURN))
        {
            return school;
        }

        var childSchoolsBuilder = new SchoolQuery()
            .WhereFederationLeadUrnEqual(urn);

        school.FederationSchools = await conn.QueryAsync<SchoolResponse>(childSchoolsBuilder, cancellationToken);

        return school;
    }
}