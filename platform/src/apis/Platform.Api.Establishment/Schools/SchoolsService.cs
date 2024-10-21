using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Platform.Search;
using Platform.Sql;

namespace Platform.Api.Establishment.Schools;

public interface ISchoolsService
{
    Task<SuggestResponse<School>> SuggestAsync(SuggestRequest request, string[]? excludeSchools = null);
    Task<School?> GetAsync(string urn);
    Task<IEnumerable<School>> QueryAsync(string? companyNumber, string? laCode, string? phase);
}

[ExcludeFromCodeCoverage]
public class SchoolsService(ISearchConnection<School> searchConnection, IDatabaseFactory dbFactory) : ISchoolsService
{
    public async Task<School?> GetAsync(string urn)
    {
        var template = Queries.GetSchool(urn);
        
        using var conn = await dbFactory.GetConnection();
        var school = await conn.QueryFirstOrDefaultAsync<School>(template);

        if (school != null && !string.IsNullOrEmpty(school.FederationLeadURN))
        {
            var schoolsTemplate = Queries.GetFederationSchools(urn);
            school.FederationSchools = await conn.QueryAsync<School>(schoolsTemplate);
        }

        return school;
    }

    public async Task<IEnumerable<School>> QueryAsync(string? companyNumber, string? laCode, string? phase)
    {
        var template = Queries.GetSchools(companyNumber, laCode, phase);
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<School>(template);
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

        return searchConnection.SuggestAsync(request, CreateFilterExpression, selectFields: fields);

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