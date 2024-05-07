using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Platform.Domain;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Establishment.Db;

public interface ILocalAuthorityDb
{
    Task<LocalAuthorityResponseModel?> Get(string code);
}

[ExcludeFromCodeCoverage]
public class LocalAuthorityDb : ILocalAuthorityDb
{
    private readonly IDatabaseFactory _dbFactory;

    public LocalAuthorityDb(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<LocalAuthorityResponseModel?> Get(string code)
    {
        const string sql = "SELECT * from LocalAuthority where Code = @Code";
        var parameters = new { Code = code };

        using var conn = await _dbFactory.GetConnection();
        var results = conn.Query<LocalAuthorityDataObject>(sql, parameters);

        return results.Select(LocalAuthorityFactory.CreateResponse).FirstOrDefault();
    }
}