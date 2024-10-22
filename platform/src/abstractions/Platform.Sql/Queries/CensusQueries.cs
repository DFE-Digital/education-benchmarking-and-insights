using Dapper;

namespace Platform.Sql;

public static partial class Queries
{
    public static SqlBuilder.Template GetCensus(string urn)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            $"{GetCensus()} WHERE URN = @URN",
            new { URN = urn });

        return template;
    }

    public static SqlBuilder.Template GetCensus(string[] urns)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            $"{GetCensus()} WHERE URN IN @URNS",
            new { URNS = urns });

        return template;
    }
    
    private static string GetCensus() => @"
SELECT 
    s.URN, 
    s.SchoolName, 
    s.SchoolType, 
    s.LAName, 
    nf.TotalPupils, 
    nf.WorkforceFTE,
    nf.WorkforceHeadcount,
    nf.TeachersFTE,
    nf.TeachersHeadcount,
    nf.SeniorLeadershipFTE,
    nf.SeniorLeadershipHeadcount,
    nf.TeachingAssistantFTE,
    nf.TeachingAssistantHeadcount,
    nf.NonClassroomSupportStaffFTE,
    nf.NonClassroomSupportStaffHeadcount,
    nf.AuxiliaryStaffFTE,
    nf.AuxiliaryStaffHeadcount,
    nf.PercentTeacherWithQualifiedStatus
FROM School s 
    LEFT JOIN (SELECT *
               FROM NonFinancial
               WHERE RunType = 'default'
                 AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')) nf 
        ON nf.URN = s.URN";
}