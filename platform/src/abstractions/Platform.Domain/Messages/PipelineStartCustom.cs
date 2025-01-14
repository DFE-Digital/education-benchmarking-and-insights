using System.Diagnostics.CodeAnalysis;

// ReSharper disable InconsistentNaming
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Platform.Domain.Messages;

public record PipelineStartCustom : PipelineStart
{
    /// <summary>
    ///     For the <c>custom</c> message, this is a year or a custom run GUID
    /// </summary>
    public string? RunId { get; set; }

    public int? Year { get; set; }

    public string? URN { get; set; }

    public Payload? Payload { get; set; }

    [SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly")]
    public static PipelineStartCustom FromPending(PipelinePending input)
    {
        int? year;
        if (input.Year != null && int.TryParse(input.Year.ToString(), out var parsed))
        {
            year = parsed;
        }
        else
        {
            throw new ArgumentException($"Unable to parse `{input.Year}` as `int`", nameof(PipelinePending.Year));
        }

        return new PipelineStartCustom
        {
            JobId = input.JobId,
            Type = input.Type,
            RunType = input.RunType,
            RunId = input.RunId?.ToString(),
            Year = year,
            URN = input.URN,
            Payload = input.Payload
        };
    }
}