ALTER VIEW [dbo].[SchoolCensus] AS
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
FROM
    School s
    LEFT JOIN CurrentDefaultNonFinancial nf ON nf.URN = s.URN
GO
    ALTER VIEW [dbo].[SchoolCensusCustom] AS
SELECT
    s.URN,
    s.SchoolName,
    s.SchoolType,
    s.LAName,
    nf.RunId,
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
FROM
    School s
    LEFT JOIN NonFinancial nf ON nf.URN = s.URN
WHERE
    nf.RunType = 'custom'
GO
    ALTER VIEW [dbo].[SchoolCensusHistoric] AS
SELECT
    s.URN,
    s.Year,
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
FROM
    (
        SELECT
            *
        FROM
            School,
            AARHistoryYears
    ) s
    LEFT OUTER JOIN NonFinancial nf ON s.Year = nf.RunId
    AND nf.URN = s.URN
    AND nf.RunType = 'default'
WHERE
    s.FinanceType = 'Academy'
UNION
SELECT
    s.URN,
    s.Year,
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
FROM
    (
        SELECT
            *
        FROM
            School,
            CFRHistoryYears
    ) s
    LEFT OUTER JOIN NonFinancial nf ON s.Year = nf.RunId
    AND nf.URN = s.URN
    AND nf.RunType = 'default'
WHERE
    s.FinanceType = 'Maintained'
GO