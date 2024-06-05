namespace Web.App.Domain;

public abstract record CensusBase
{
    public string? URN { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? WorkforceFTE { get; set; }
    public decimal? WorkforceHeadcount { get; set; }
    public decimal? TeachersFTE { get; set; }
    public decimal? SeniorLeadershipFTE { get; set; }
    public decimal? TeachingAssistantFTE { get; set; }
    public decimal? NonClassroomSupportStaffFTE { get; set; }
    public decimal? AuxiliaryStaffFTE { get; set; }
    public decimal? PercentTeacherWithQualifiedStatus { get; set; }
}


public record Census : CensusBase
{
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

public record CensusHistory : CensusBase
{
    public int? Year { get; set; }
    public string? Term { get; set; }
}