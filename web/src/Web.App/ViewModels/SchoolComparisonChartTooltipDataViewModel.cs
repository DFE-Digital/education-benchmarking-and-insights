using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels;

public record SchoolComparisonChartTooltipViewModel
{
    public SchoolComparisonChartTooltipData[]? Data { get; init; }
}

public record SchoolComparisonChartTooltipData : SchoolChartTooltipData
{
    public string? ProgressBanding { get; init; }
    public string? SchoolType { get; init; }
    public string? ProgressBandingColour { get; init; }
    public bool ShouldShowTag { get; init; }
}
