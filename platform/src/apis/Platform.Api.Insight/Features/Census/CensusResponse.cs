using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Api.Insight.Features.Census;

[ExcludeFromCodeCoverage]
public abstract record CensusResponse
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

[ExcludeFromCodeCoverage]
public record CensusSchoolResponse : CensusResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

[ExcludeFromCodeCoverage]
public record CensusHistoryResponse
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<CensusHistoryRowResponse> Rows { get; set; } = [];
}

[ExcludeFromCodeCoverage]
public record CensusHistoryRowResponse : CensusResponse
{
    public int? Year { get; set; }
}