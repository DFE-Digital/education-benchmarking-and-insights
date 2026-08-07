using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthority.Features.Risks.Models;
using Platform.Domain;
using Platform.Functions;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.LocalAuthority.Features.Risks.Services;

public interface ISchoolRisksService
{
    Task<PagedResult<SchoolRiskResponse>> QueryPagedAsync(
        string[] codes,
        int page,
        int pageSize,
        string? sortField,
        string? sortOrder,
        string? phase,
        CancellationToken cancellationToken = default);
}

public class SchoolRisksService(IDatabaseFactory dbFactory) : ISchoolRisksService
{
    public async Task<PagedResult<SchoolRiskResponse>> QueryPagedAsync(string[] codes,
        int page,
        int pageSize,
        string? sortField,
        string? sortOrder,
        string? phase,
        CancellationToken cancellationToken = default)
    {
        var sortByField = sortField ?? LocalAuthoritySchoolRisksHeaders.Overall;
        var sortDirection = sortOrder ?? SortDirection.Desc;

        using var conn = await dbFactory.GetConnection();
        var itemsBuilder = new LocalAuthoritySchoolRisksPagedQuery()
            .WhereLaCodesIn(codes)
            .OrderBy(sortByField, sortDirection)
            .Offset((page - 1) * pageSize)
            .Fetch(pageSize);

        if (phase != null)
        {
            itemsBuilder = itemsBuilder.WhereOverallPhaseEqual(phase);
        }

        var items = (await conn.QueryAsync<SchoolRiskResponse>(itemsBuilder, cancellationToken)).ToList();

        var countBuilder = new LocalAuthoritySchoolRisksCountQuery()
            .WhereLaCodesIn(codes);

        if (phase != null)
        {
            countBuilder = countBuilder.WhereOverallPhaseEqual(phase);
        }

        var totalCount = (await conn.QueryAsync<int>(countBuilder, cancellationToken)).FirstOrDefault();

        return new PagedResult<SchoolRiskResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}
