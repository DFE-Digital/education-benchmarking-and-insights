using System;
using Dapper.Contrib.Extensions;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Orchestrator.Sql;

[Table("CompletedPipelineRun")]
public record CompletedPipelineRun
{
    [Key] public int Id { get; set; }
    public DateTimeOffset CompletedAt { get; set; }
    public string? OrchestrationId { get; set; }
    public string? Message { get; set; }
}