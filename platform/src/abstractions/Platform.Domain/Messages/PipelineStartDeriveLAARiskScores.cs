using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain.Messages;

[ExcludeFromCodeCoverage]
public record PipelineStartDeriveLAARiskScores : PipelineStart
{
    /// <summary>
    ///     The target year for deriving risk scores (e.g. 2026)
    /// </summary>
    public int? RunId { get; set; }

    [SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly")]
    public static PipelineStartDeriveLAARiskScores FromPending(PipelinePending input)
    {
        int? runId = null;
        if (input.RunId != null && int.TryParse(input.RunId.ToString(), out var parsed))
        {
            runId = parsed;
        }

        if (runId == null)
        {
            throw new ArgumentException($"Unable to parse `{input.RunId}` as `int`", nameof(PipelinePending.RunId));
        }

        return new PipelineStartDeriveLAARiskScores
        {
            JobId = input.JobId,
            Type = input.Type ?? Pipeline.JobType.DeriveLAARiskScores,
            RunType = input.RunType,
            RunId = runId
        };
    }
}
