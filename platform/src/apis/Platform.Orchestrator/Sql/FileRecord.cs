using System;
using Dapper.Contrib.Extensions;

namespace Platform.Orchestrator.Sql;

[Table("[File]")]
public record FileRecord
{
    [ExplicitKey]
    public string Type { get; init; } = string.Empty;
    [ExplicitKey]
    public string Label { get; init; } = string.Empty;
    public string FileName { get; init; } = string.Empty;
    public DateTimeOffset? ValidFrom { get; init; }
    public DateTimeOffset? ValidTo { get; init; }
    public string RunId { get; init; } = string.Empty;
    public decimal? Filesize { get; init; }
}
