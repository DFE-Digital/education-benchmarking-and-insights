namespace Web.App.Domain;

public record Workforce
{
    public string? Urn { get; set; }
    public string? Name { get; set; }
    public string? SchoolType { get; set; }
    public string? LocalAuthority { get; set; }
    public decimal NumberOfPupils { get; set; }
    public int YearEnd { get; set; }
    public string? Term { get; set; }
    public string? Dimension { get; set; }
    public decimal? WorkforceFte { get; set; }
    public decimal? TeachersFte { get; set; }
    public decimal? SeniorLeadershipFte { get; set; }
    public decimal? TeachingAssistantsFte { get; set; }
    public decimal? NonClassroomSupportStaffFte { get; set; }
    public decimal? AuxiliaryStaffFte { get; set; }
    public decimal? WorkforceHeadcount { get; set; }
    public decimal? TeachersQualified { get; set; }
    public bool HasIncompleteData { get; set; }
}