using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Platform.Infrastructure.Search;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Establishment.Schools;

public interface ISchoolsService
{
    Task<SuggestResponse<School>> SuggestAsync(SuggestRequest request);
    Task<School?> GetAsync(string urn);
    Task<IEnumerable<School>> QueryAsync(string? companyNumber, string? laCode, string? phase);
}


[ExcludeFromCodeCoverage]
public class SchoolsService : SearchService, ISchoolsService
{
    private const string IndexName = SearchResourceNames.Indexes.School;
    private readonly IDatabaseFactory _dbFactory;
    public SchoolsService(IDatabaseFactory dbFactory, IOptions<SearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
        _dbFactory = dbFactory;
    }

    public async Task<School?> GetAsync(string urn)
    {
        const string sql = "SELECT * from School where URN = @URN";
        var parameters = new { URN = urn };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<School>(sql, parameters);
    }

    public async Task<IEnumerable<School>> QueryAsync(string? companyNumber, string? laCode, string? phase)
    {

        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from School /**where**/");

        if (!string.IsNullOrEmpty(companyNumber))
        {
            builder.Where("TrustCompanyNumber = @CompanyNumber", new { companyNumber });
        }

        if (!string.IsNullOrEmpty(laCode))
        {
            builder.Where("LaCode = @LaCode", new { laCode });
        }

        if (!string.IsNullOrEmpty(phase))
        {
            builder.Where("OverallPhase = @phase", new { phase });
        }

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<School>(template.RawSql, template.Parameters);
    }

    public Task<SuggestResponse<School>> SuggestAsync(SuggestRequest request)
    {
        var fields = new[]
        {
            nameof(School.URN),
            nameof(School.SchoolName),
            //nameof(School.Town),
            //nameof(School.Postcode)
        };

        return SuggestAsync<School>(request, selectFields: fields);
    }
}