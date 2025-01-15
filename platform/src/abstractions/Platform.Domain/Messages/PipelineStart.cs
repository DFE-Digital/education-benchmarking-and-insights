// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain.Messages;

[ExcludeFromCodeCoverage]
public abstract record PipelineStart
{
    public string? JobId { get; set; } = Guid.NewGuid().ToString();

    /// See
    /// <see cref="Pipeline.JobType" />
    /// for available values:
    /// <c>default</c>
    /// |
    /// <c>comparator-set</c>
    /// |
    /// <c>custom-data</c>
    public string? Type { get; set; }

    /// See
    /// <see cref="Pipeline.RunType" />
    /// for available values:
    /// <c>default</c>
    /// |
    /// <c>custom</c>
    public string? RunType { get; set; }
}