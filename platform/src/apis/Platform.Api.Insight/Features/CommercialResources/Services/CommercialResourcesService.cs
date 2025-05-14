using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.CommercialResources.Responses;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.CommercialResources.Services;

public interface ICommercialResourcesService
{
    Task<IEnumerable<CommercialResourcesResponse>> GetCommercialResourcesByCategory(CancellationToken cancellationToken = default, params string[] categories);
}

[ExcludeFromCodeCoverage]
public class CommercialResourcesService(IDatabaseFactory dbFactory) : ICommercialResourcesService
{
    public async Task<IEnumerable<CommercialResourcesResponse>> GetCommercialResourcesByCategory(CancellationToken cancellationToken = default, params string[] categories)
    {
        using var conn = await dbFactory.GetConnection();

        var builder = new CommercialResourcesQuery().WhereCategoryIn(categories);

        return await conn.QueryAsync<CommercialResourcesResponse>(builder, cancellationToken);
    }
}