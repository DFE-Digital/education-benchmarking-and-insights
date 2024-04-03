namespace Web.App.Domain;

public record Workforce
{
    public int YearEnd { get; set; }
    public string? Dimension { get; set; }
    public decimal? WorkforceFte { get; set; }
    public decimal? TeachersFte { get; set; }
    public decimal? SeniorLeadershipFte { get; set; }
    public decimal? TeachingAssistantsFte { get; set; }
    public decimal? NonClassroomSupportStaffFte { get; set; }
    public decimal? AuxiliaryStaffFte { get; set; }
    public decimal? WorkforceHeadcount { get; set; }
    public decimal? TeachersWithQts { get; set; }
}