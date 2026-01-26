namespace Web.App.ViewModels;

public record SchoolSeniorLeadershipChartTooltipViewModel
{
    public SchoolSeniorLeadershipChartTooltipData[]? Data { get; init; }
}

public record SchoolSeniorLeadershipChartTooltipData : SchoolChartTooltipData
{
    public string? HeadTeachers { get; init; }
    public string? DeputyHeadTeachers { get; init; }
    public string? AssistantHeadTeachers { get; init; }
    public string? LeadershipNonTeachers { get; init; }
}