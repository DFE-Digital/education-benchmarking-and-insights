// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.Domain;

public record RagRatingSummary
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
    public int? RedCount { get; set; }
    public int? AmberCount { get; set; }
    public int? GreenCount { get; set; }
}