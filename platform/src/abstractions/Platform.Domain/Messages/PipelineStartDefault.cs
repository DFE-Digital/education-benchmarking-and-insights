using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Platform.Domain.Messages;

public record PipelineStartDefault : PipelineStart
{
    /// <summary>
    ///     For the <c>default</c> message, this is a year
    /// </summary>
    public int? RunId { get; set; }

    public PipelineMessageYears? Year { get; set; }

    [SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly")]
    public static PipelineStartDefault FromPending(PipelinePending input)
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

        PipelineMessageYears? year = null;
        if (input.Year is JObject yearObj)
        {
            year = yearObj.ToObject<PipelineMessageYears>();
        }

        if (year == null)
        {
            throw new ArgumentException($"Unable to parse `{input.Year}` as `PipelineMessageYears`", nameof(PipelinePending.Year));
        }

        return new PipelineStartDefault
        {
            JobId = input.JobId,
            Type = input.Type,
            RunType = input.RunType,
            RunId = runId,
            Year = year
        };
    }
}

public record PipelineMessageYears
{
    public int? Aar { get; set; }
    public int? Cfr { get; set; }
    public int? Bfr { get; set; }
    public int? S251 { get; set; }
}