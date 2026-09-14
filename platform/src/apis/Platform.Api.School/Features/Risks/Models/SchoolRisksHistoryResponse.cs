using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School.Features.Risks.Models;

[ExcludeFromCodeCoverage]
public record SchoolRisksHistoryResponse
{
    public int? StartYear { get; init; }
    public int? EndYear { get; init; }
    public IEnumerable<SchoolRisksHistoryRowResponse> Rows { get; init; } = [];
}

[ExcludeFromCodeCoverage]
public record SchoolRisksHistoryRowResponse : SchoolRisksResponse
{
    public int? Year { get; init; }
}
