using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Domain.Messages;

[ExcludeFromCodeCoverage]
public record PipelineFinish
{
    public string? JobId { get; set; }
    public string? RunId { get; set; }
    public bool Success { get; set; }
}