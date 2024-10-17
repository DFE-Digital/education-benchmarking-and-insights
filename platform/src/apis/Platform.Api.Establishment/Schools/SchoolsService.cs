using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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
        const string schoolSql = "SELECT * FROM School WHERE URN = @URN";
        const string childSchoolsSql = "SELECT * FROM School WHERE FederationLeadURN = @URN";

        using var conn = await dbFactory.GetConnection();
        var school = await conn.QueryFirstOrDefaultAsync<School>(schoolSql, new { URN = urn });

        if (school != null && !string.IsNullOrEmpty(school.FederationLeadURN))
        {
            school.Schools = await conn.QueryAsync<School>(childSchoolsSql, new { URN = urn });
        }

        return school;
    }

    public async Task<IEnumerable<School>> QueryAsync(string? companyNumber, string? laCode, string? phase)
    {

        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from School /**where**/");

        if (!string.IsNullOrEmpty(companyNumber))
        {
            builder.Where("TrustCompanyNumber = @CompanyNumber AND FinanceType = 'Academy'", new { companyNumber });
        }

        if (!string.IsNullOrEmpty(laCode))
        {
            builder.Where("LaCode = @LaCode AND FinanceType = 'Maintained'", new { laCode });
        }

        if (!string.IsNullOrEmpty(phase))
        {
            builder.Where("OverallPhase = @phase", new { phase });
        }

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<School>(template.RawSql, template.Parameters);
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