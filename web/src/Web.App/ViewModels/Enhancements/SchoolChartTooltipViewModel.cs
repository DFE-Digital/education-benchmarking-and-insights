namespace Web.App.ViewModels.Enhancements;

public record SchoolChartTooltipViewModel
{
    public SchoolChartTooltipData[]? Data { get; init; }
    public Guid Uuid { get; init; }
}

public record SchoolChartTooltipData
{
    public string? Urn { get; init; }
    public string? SchoolName { get; init; }
    public string? LAName { get; init; }
    public string? SchoolType { get; init; }
    public decimal? TotalPupils { get; init; }
}