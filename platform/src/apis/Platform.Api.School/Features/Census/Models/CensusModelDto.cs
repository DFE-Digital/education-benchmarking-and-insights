using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
namespace Platform.Api.School.Features.Census.Models;

[ExcludeFromCodeCoverage]
public abstract record CensusModelBase
{
    public decimal? TotalPupils { get; set; }
    public decimal? Workforce { get; set; }
    public decimal? WorkforceHeadcount { get; set; }
    public decimal? Teachers { get; set; }
    public decimal? SeniorLeadership { get; set; }
    public decimal? TeachingAssistant { get; set; }
    public decimal? NonClassroomSupportStaff { get; set; }
    public decimal? AuxiliaryStaff { get; set; }
    public decimal? PercentTeacherWithQualifiedStatus { get; set; }
}

public record CensusHistoryModelDto : CensusModelBase
{
    public int? RunId { get; set; }
}

public record CensusModelDto : CensusModelBase
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}