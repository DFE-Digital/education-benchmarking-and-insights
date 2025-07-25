// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Web.App.Infrastructure.Apis;

public abstract record ChartRequest<T>
{
    public T[]? Data { get; set; }
    public string? HighlightKey { get; set; }
    public string? Id { get; set; }
    public string? KeyField { get; set; }
    public string? Sort { get; set; }
    public string? ValueField { get; set; }
    public int? Width { get; set; }
}