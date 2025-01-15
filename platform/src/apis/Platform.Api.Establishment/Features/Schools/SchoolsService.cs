using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Platform.Search;
using Platform.Search.Requests;
using Platform.Search.Responses;
using Platform.Sql;

namespace Platform.Api.Establishment.Features.Schools;

public interface ISchoolsService
{
    Task<SuggestResponse<School>> SuggestAsync(SuggestRequest request, string[]? excludeSchools = null);
    Task<School?> GetAsync(string urn);
}

//TODO: Update SQL queries to use builders
[ExcludeFromCodeCoverage]
public class SchoolsService(ISearchConnection<School> searchConnection, IDatabaseFactory dbFactory) : ISchoolsService
{
    public async Task<School?> GetAsync(string urn)
    {
        const string schoolSql = "SELECT * FROM School WHERE URN = @URN";
        const string childSchoolsSql = "SELECT * FROM School WHERE FederationLeadURN = @URN";

        using var conn = await dbFactory.GetConnection();
        var school = await conn.QueryFirstOrDefaultAsync<School>(schoolSql, new
        {
            URN = urn
        });

        if (school != null && !string.IsNullOrEmpty(school.FederationLeadURN))
        {
            school.FederationSchools = await conn.QueryAsync<School>(childSchoolsSql, new
            {
                URN = urn
            });
        }

        return school;
    }

    public Task<SuggestResponse<School>> SuggestAsync(SuggestRequest request, string[]? excludeSchools = null)
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

        return searchConnection.SuggestAsync(request, CreateFilterExpression, fields);

        string? CreateFilterExpression()
        {
            if (excludeSchools is not { Length: > 0 })
            {
                return null;
            }

            var filterExpressions = new List<string>();
            if (excludeSchools.Length > 0)
            {
                filterExpressions.Add($"({string.Join(") and ( ", excludeSchools.Select(a => $"URN ne '{a}'"))})");
            }

            return string.Join(" and ", filterExpressions);
        }
    }
}