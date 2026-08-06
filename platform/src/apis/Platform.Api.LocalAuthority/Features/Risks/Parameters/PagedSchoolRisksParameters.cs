using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Risks.Parameters;

public record PagedSchoolRisksParameters : QueryParameters
{
    public string[] Codes { get; private set; } = [];
    public int Page { get; private set; } = 1;
    public int PageSize { get; private set; } = 10;
    public string? SortField { get; private set; }
    public string? SortOrder { get; private set; }
    public string? Phase { get; private set; }

    public override void SetValues(NameValueCollection query)
    {
        Codes = query.ToStringArray("code");

        if (query.TryGetValue("page", out var page))
        {
            Page = int.TryParse(page, out var p) ? p : Page;
        }

        if (query.TryGetValue("pageSize", out var pageSize))
        {
            PageSize = int.TryParse(pageSize, out var p) ? p : PageSize;
        }

        if (query.TryGetValue("sortField", out var sortField))
        {
            SortField = sortField;
        }

        if (query.TryGetValue("sortOrder", out var sortOrder))
        {
            SortOrder = sortOrder;
        }

        if (query.TryGetValue("phase", out var phase))
        {
            Phase = phase;
        }
    }
}
