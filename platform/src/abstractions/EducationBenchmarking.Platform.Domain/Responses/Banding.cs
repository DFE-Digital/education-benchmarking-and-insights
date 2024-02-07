using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public record Banding
{
    public string? Term { get; set; }
    public string? OverallPhase { get; set; }
    public bool HasSixthForm { get; set; }
    public decimal Min { get; set; }
    public decimal? Max { get; set; }
    public string? Scale { get; set; }
}