using System;

namespace Platform.Api.Benchmark.ComparatorSets;

public record ComparatorSetDefault
{
    public string? URN { get; set; }
    public string? SetType { get; set; }
    public string[] Pupil { get; set; } = Array.Empty<string>();
    public string[] Building { get; set; } = Array.Empty<string>();
};