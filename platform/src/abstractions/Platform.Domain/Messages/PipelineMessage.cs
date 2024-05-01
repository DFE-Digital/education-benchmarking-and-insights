using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain.Messages;

[ExcludeFromCodeCoverage]
public class PipelineMessage
{
    public string? JobId { get; set; }
}