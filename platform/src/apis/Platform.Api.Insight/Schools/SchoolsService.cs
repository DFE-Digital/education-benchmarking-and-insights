﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Insight.Schools;

public interface ISchoolsService
{
    Task<IEnumerable<SchoolCharacteristic>> QueryCharacteristicAsync(string[] urns);
    Task<SchoolCharacteristic?> CharacteristicAsync(string urn);
}

public class SchoolsService : ISchoolsService
{
    private readonly IDatabaseFactory _dbFactory;

    public SchoolsService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }


    public async Task<IEnumerable<SchoolCharacteristic>> QueryCharacteristicAsync(string[] urns)
    {
        using var conn = await _dbFactory.GetConnection();
        const string sql = "SELECT * FROM SchoolCharacteristic WHERE URN IN @URNS";
        var parameters = new { URNS = urns };

        return await conn.QueryAsync<SchoolCharacteristic>(sql, parameters);
    }

    public async Task<SchoolCharacteristic?> CharacteristicAsync(string urn)
    {
        using var conn = await _dbFactory.GetConnection();
        const string sql = "SELECT * FROM SchoolCharacteristic WHERE URN = @URN";
        var parameters = new { URN = urn };

        return await conn.QueryFirstOrDefaultAsync<SchoolCharacteristic>(sql, parameters);
    }
}