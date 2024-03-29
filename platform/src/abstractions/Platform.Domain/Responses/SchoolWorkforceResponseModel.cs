using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record SchoolWorkforceResponseModel
{
    public string? Urn { get; set; }
    public string? Name { get; set; }
    public string? SchoolType { get; set; }
    public string? LocalAuthority { get; set; }
    public decimal NumberOfPupils { get; set; }
    public decimal SchoolWorkforceFte { get; set; }
    public decimal TotalNumberOfTeachersFte { get; set; }
    public decimal TeachersWithQtsfte { get; set; }
    public decimal SeniorLeadershipFte { get; set; }
    public decimal TeachingAssistantsFte { get; set; }
    public decimal NonClassroomSupportStaffFte { get; set; }
    public decimal AuxiliaryStaffFte { get; set; }
    public decimal SchoolWorkforceHeadcount { get; set; }

    public static SchoolWorkforceResponseModel Create(SchoolTrustFinancialDataObject dataObject)
    {
        return new SchoolWorkforceResponseModel
        {
            Urn = dataObject.Urn.ToString(),
            Name = dataObject.SchoolName,
            SchoolType = dataObject.Type,
            LocalAuthority = dataObject.La.ToString(),
            NumberOfPupils = dataObject.NoPupils,
            SchoolWorkforceFte = dataObject.WorkforceTotal,
            TotalNumberOfTeachersFte = dataObject.TeachersTotal,
            TeachersWithQtsfte = dataObject.PercentageQualifiedTeachers,
            SeniorLeadershipFte = dataObject.TeachersLeader,
            TeachingAssistantsFte = dataObject.FullTimeTa,
            NonClassroomSupportStaffFte = dataObject.FullTimeOther,
            AuxiliaryStaffFte = dataObject.AuxStaff,
            SchoolWorkforceHeadcount = dataObject.WorkforceHeadcount
        };
    }
}


