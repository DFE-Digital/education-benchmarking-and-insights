namespace Platform.Api.Benchmark.ComparatorSets;

public record ComparatorSetDefaultSchool
{
    public string? URN { get; set; }
    public string? SetType { get; set; }

    public ComparatorSetIds? Pupil { get; set; }
    public ComparatorSetIds? Building { get; set; }
}


