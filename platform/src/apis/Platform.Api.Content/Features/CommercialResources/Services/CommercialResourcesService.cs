using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Platform.Api.Content.Features.CommercialResources.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Content.Features.CommercialResources.Services;

public interface ICommercialResourcesService
{
    Task<IEnumerable<CommercialResource>> GetCommercialResources(CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class CommercialResourcesService : ICommercialResourcesService
{
    private readonly IDatabaseFactory _dbFactory;

    public CommercialResourcesService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
        SqlMapper.AddTypeHandler(new CategoryCollectionTypeHandler());
    }

    public async Task<IEnumerable<CommercialResource>> GetCommercialResources(CancellationToken cancellationToken = default)
    {
        var query = new CommercialResourcesQuery();
        using var conn = await _dbFactory.GetConnection();

        return await conn.QueryAsync<CommercialResource>(query, cancellationToken);
    }
}