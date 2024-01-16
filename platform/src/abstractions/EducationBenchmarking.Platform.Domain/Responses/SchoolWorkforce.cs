using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Domain.DataObjects;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public class SchoolWorkforce
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
    
    public static SchoolWorkforce Create(SchoolTrustFinancialDataObject dataObject)
    {
        return new SchoolWorkforce
        {
            Urn = dataObject.URN.ToString(),
            Name = dataObject.SchoolName,
            SchoolType = dataObject.Type,
            LocalAuthority = dataObject.LA.ToString(),
            NumberOfPupils = dataObject.NoPupils,
            SchoolWorkforceFTE = dataObject.WorkforceTotal,
            TotalNumberOfTeachersFTE = dataObject.TeachersTotal,
            TeachersWithQTSFTE = dataObject.PercentageQualifiedTeachers,
            SeniorLeadershipFTE = dataObject.TeachersLeader,
            TeachingAssistantsFTE = dataObject.FullTimeTA,
            NonClassroomSupportStaffFTE = dataObject.FullTimeOther,
            AuxiliaryStaffFTE = dataObject.AuxStaff,
            SchoolWorkforceHeadcount = dataObject.WorkforceHeadcount
        };
    }
}


