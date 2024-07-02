namespace Web.App.Domain;

public abstract record CensusBase
{
    public string? URN { get; set; }
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