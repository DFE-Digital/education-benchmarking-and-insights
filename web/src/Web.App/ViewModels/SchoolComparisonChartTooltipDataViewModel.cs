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
    public bool ShouldShowKs4ProgressTag { get; init; }
    public string? SetType { get; init; }
    public bool TopRankedAttainment8 { get; init; }
    public bool ShouldShowAttainment8Tag { get; init; }
}
