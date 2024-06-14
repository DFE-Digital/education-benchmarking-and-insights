namespace Web.App.Domain.Benchmark;

public record TrustComparatorSetUserDefined
{
    public long? TotalTrusts { get; set; }
    public string[] Set { get; set; } = [];
}

public record UserDefinedTrustComparatorSet
{
    public string? RunId { get; set; }
    public long? TotalTrusts { get; set; }
    public string[] Set { get; set; } = [];
}