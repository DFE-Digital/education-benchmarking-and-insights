using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Domain;

[ExcludeFromCodeCoverage]
public record SchoolWorkforce
{
    public string Urn { get; set; }
    public string Name { get; set; }
    public string SchoolType { get; set; }
    public string LocalAuthority { get; set; }
    
    public decimal NumberOfPupils { get; set; }

    public decimal SchoolWorkforceFTE { get; set; }
    public decimal TotalNumberOfTeachersFTE { get; set; }
    public decimal TeachersWithQTSFTE { get; set; }
    public decimal SeniorLeadershipFTE { get; set; }
    public decimal TeachingAssistantsFTE { get; set; }
    public decimal NonClassroomSupportStaffFTE { get; set; }
    public decimal AuxiliaryStaffFTE { get; set; }
    public decimal SchoolWorkforceHeadcount { get; set; }
}