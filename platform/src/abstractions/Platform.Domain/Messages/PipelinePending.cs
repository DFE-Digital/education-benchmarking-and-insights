// ReSharper disable InconsistentNaming
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Platform.Domain.Messages;

public record PipelinePending : PipelineStart
{
    /// <summary>
    ///     This property could be serialised to an <c>int</c> or a <c>string</c>
    /// </summary>
    public object? RunId { get; set; }

    /// <summary>
    ///     This property could be serialised to an <c>int</c> or a <see cref="PipelineMessageYears">PipelineMessageYears</see>
    /// </summary>
    public object? Year { get; set; }

    public string? URN { get; set; }

    public Payload? Payload { get; set; }
}