using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Content.Features.Banners.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Content.Features.Banners.Services;

public interface IBannersService
{
    Task<Banner?> GetBannerOrDefault(string bannerType, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class BannersService(IDatabaseFactory dbFactory) : IBannersService
{
    public async Task<Banner?> GetBannerOrDefault(string bannerType, CancellationToken cancellationToken = default)
    {
        var query = new BannersQuery()
            .WhereValueEqual(bannerType)
            .OrderBy("ValidFrom DESC");
        using var conn = await dbFactory.GetConnection();

        return await conn.QueryFirstOrDefaultAsync<Banner>(query, cancellationToken);
    }
}