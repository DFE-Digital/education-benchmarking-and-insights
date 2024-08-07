using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Insight.Census;

[ExcludeFromCodeCoverage]
public abstract record CensusBaseModel
{
    public string? URN { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? WorkforceFTE { get; set; }
    public decimal? WorkforceHeadcount { get; set; }
    public decimal? TeachersFTE { get; set; }
    public decimal? TeachersHeadcount { get; set; }
    public decimal? SeniorLeadershipFTE { get; set; }
    public decimal? SeniorLeadershipHeadcount { get; set; }
    public decimal? TeachingAssistantFTE { get; set; }
    public decimal? TeachingAssistantHeadcount { get; set; }
    public decimal? NonClassroomSupportStaffFTE { get; set; }
    public decimal? NonClassroomSupportStaffHeadcount { get; set; }
    public decimal? AuxiliaryStaffFTE { get; set; }
    public decimal? AuxiliaryStaffHeadcount { get; set; }
    public decimal? PercentTeacherWithQualifiedStatus { get; set; }
}

[ExcludeFromCodeCoverage]
public record CensusModel : CensusBaseModel
{
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

[ExcludeFromCodeCoverage]
public record CensusHistoryModel : CensusBaseModel
{
    public int? Year { get; set; }
}