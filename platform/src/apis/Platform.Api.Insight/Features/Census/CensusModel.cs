using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
namespace Platform.Api.Insight.Features.Census;

[ExcludeFromCodeCoverage]
public record CensusYearsModel
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
}

[ExcludeFromCodeCoverage]
public abstract record CensusModel
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
public record CensusSchoolModel : CensusModel
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

[ExcludeFromCodeCoverage]
public record CensusHistoryModel : CensusModel
{
    public int? RunId { get; set; }
}