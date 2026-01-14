using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record SeniorLeadershipGroup
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? LAName { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? SeniorLeadership { get; set; }
    public decimal? HeadTeacher { get; set; }
    public decimal? DeputyHeadTeacher { get; set; }
    public decimal? AssistantHeadTeacher { get; set; }
    public decimal? LeadershipNonTeacher { get; set; }
}