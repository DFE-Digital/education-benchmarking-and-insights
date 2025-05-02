using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.Schools.Models;
using Platform.Sql;

namespace Platform.Api.Insight.Features.Schools.Services;

public interface ISchoolsService
{
    Task<IEnumerable<SchoolCharacteristic>> QueryCharacteristicAsync(string[] urns, CancellationToken cancellationToken = default);
    Task<SchoolCharacteristic?> CharacteristicAsync(string urn, CancellationToken cancellationToken = default);
}

public class SchoolsService(IDatabaseFactory dbFactory) : ISchoolsService
{
    public async Task<IEnumerable<SchoolCharacteristic>> QueryCharacteristicAsync(string[] urns, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        const string sql = "SELECT * FROM SchoolCharacteristic WHERE URN IN @URNS";
        var parameters = new
        {
            URNS = urns
        };

        return await conn.QueryAsync<SchoolCharacteristic>(sql, parameters, cancellationToken);
    }

    public async Task<SchoolCharacteristic?> CharacteristicAsync(string urn, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        const string sql = "SELECT * FROM SchoolCharacteristic WHERE URN = @URN";
        var parameters = new
        {
            URN = urn
        };

        return await conn.QueryFirstOrDefaultAsync<SchoolCharacteristic>(sql, parameters, cancellationToken);
    }
}