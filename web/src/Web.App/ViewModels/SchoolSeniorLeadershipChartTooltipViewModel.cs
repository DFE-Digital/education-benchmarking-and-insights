namespace Web.App.ViewModels;

public record SchoolSeniorLeadershipChartTooltipViewModel
{
    public SchoolSeniorLeadershipChartTooltipData[]? Data { get; init; }
}

public record SchoolSeniorLeadershipChartTooltipData : SchoolChartTooltipData
{
    public decimal? HeadTeachers { get; init; }
    public decimal? DeputyHeadTeachers { get; init; }
    public decimal? AssistantHeadTeachers { get; init; }
    public decimal? LeadershipNonTeachers { get; init; }
}