// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Platform.Domain.Messages;

public abstract record PipelineStart
{
    public string? JobId { get; set; } = Guid.NewGuid().ToString();

    /// See
    /// <see cref="PipelineJobType" />
    /// for available values:
    /// <c>default</c>
    /// |
    /// <c>comparator-set</c>
    /// |
    /// <c>custom-data</c>
    public string? Type { get; set; }

    /// See
    /// <see cref="PipelineRunType" />
    /// for available values:
    /// <c>default</c>
    /// |
    /// <c>custom</c>
    public string? RunType { get; set; }
}

public record PipelineRunType
{
    public const string Default = "default";
    public const string Custom = "custom";
}

public record PipelineJobType
{
    public const string Default = "default";
    public const string ComparatorSet = "comparator-set";
    public const string CustomData = "custom-data";
}