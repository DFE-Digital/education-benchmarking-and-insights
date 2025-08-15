namespace Web.App.ViewModels;

public record SchoolChartTooltipViewModel
{
    public SchoolChartTooltipData[]? Data { get; init; }
}

public record SchoolChartTooltipData
{
    public string? Urn { get; init; }
    public string? SchoolName { get; init; }
    public string? LAName { get; init; }
    public string? SchoolType { get; init; }
    public decimal? TotalPupils { get; init; }
    public int? PeriodCoveredByReturn { get; init; }
}