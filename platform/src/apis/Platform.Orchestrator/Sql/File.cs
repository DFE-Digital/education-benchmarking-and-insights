using System;
using Dapper.Contrib.Extensions;

namespace Platform.Orchestrator.Sql;

[Table("[File]")]
public record FileRecord
{
    [ExplicitKey]
    public string Type { get; set; } = string.Empty;
    [ExplicitKey]
    public string Label { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public DateTimeOffset? ValidFrom { get; set; }
    public DateTimeOffset? ValidTo { get; set; }
    public string RunId { get; set; } = string.Empty;
    public decimal? Filesize { get; set; }
}
