using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Api.School.Features.Census.Models;

[ExcludeFromCodeCoverage]
public abstract record CensusResponseBase
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

public record CensusResponse : CensusResponseBase
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

public record CensusHistoryResponse
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<CensusHistoryRowResponse> Rows { get; set; } = [];
}

public record CensusHistoryRowResponse : CensusResponseBase
{
    public int? Year { get; set; }
}