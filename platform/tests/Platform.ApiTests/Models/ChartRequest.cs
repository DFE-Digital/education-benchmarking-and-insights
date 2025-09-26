// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Platform.ApiTests.Models;

public abstract record ChartRequest<T>
{
    public T[]? Data { get; set; }
    public float? DomainMax { get; set; }
    public float? DomainMin { get; set; }
    public string? HighlightKey { get; set; }
    public string? Id { get; set; }
    public string? KeyField { get; set; }
    public string? Sort { get; set; }
    public string? ValueField { get; set; }
    public int? Width { get; set; }
}

public class TestDatum
{
    public string? Key { get; init; }
    public decimal? Value { get; init; }
}