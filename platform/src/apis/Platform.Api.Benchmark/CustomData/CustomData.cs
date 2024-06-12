namespace Platform.Api.Benchmark.CustomData;

public record CustomDataSchool()
{
    public string? URN;
    public string? RunId { get; set; }
    public string? RunType { get; set; }
}