using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
namespace Platform.Api.School.Features.Census.Models;

[ExcludeFromCodeCoverage]
public record SeniorLeadershipResponse
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