IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolCensusHistoric')
    BEGIN
        DROP VIEW SchoolCensusHistoric
    END
GO

CREATE VIEW SchoolCensusHistoric
AS
SELECT s.URN,
       nf.RunId 'Year',
       nf.TotalPupils,
       nf.WorkforceFTE,
       nf.WorkforceHeadcount,
       nf.TeachersFTE,
       nf.SeniorLeadershipFTE,
       nf.TeachingAssistantFTE,
       nf.NonClassroomSupportStaffFTE,
       nf.AuxiliaryStaffFTE,
       nf.PercentTeacherWithQualifiedStatus
FROM School s,
     (SELECT Value FROM Parameters p WHERE p.Name = 'LatestAARYear') y,
     NonFinancial nf
WHERE s.URN = nf.URN
  AND nf.RunType = 'default'
  AND s.FinanceType = 'Academy'
  AND nf.RunId BETWEEN y.Value - 5 AND y.Value
UNION
SELECT s.URN,
       nf.RunId 'Year',
       nf.TotalPupils,
       nf.WorkforceFTE,
       nf.WorkforceHeadcount,
       nf.TeachersFTE,
       nf.SeniorLeadershipFTE,
       nf.TeachingAssistantFTE,
       nf.NonClassroomSupportStaffFTE,
       nf.AuxiliaryStaffFTE,
       nf.PercentTeacherWithQualifiedStatus
FROM School s,
     (SELECT Value FROM Parameters p WHERE p.Name = 'LatestCFRYear') y,
     NonFinancial nf
WHERE s.URN = nf.URN
  AND nf.RunType = 'default'
  AND s.FinanceType = 'Maintained'
  AND nf.RunId BETWEEN y.Value - 5 AND y.Value

GO


IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolCensus')
    BEGIN
        DROP VIEW SchoolCensus
    END
GO

CREATE VIEW SchoolCensus
AS
SELECT s.URN,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       nf.TotalPupils,
       nf.WorkforceFTE,
       nf.WorkforceHeadcount,
       nf.TeachersFTE,
       nf.SeniorLeadershipFTE,
       nf.TeachingAssistantFTE,
       nf.NonClassroomSupportStaffFTE,
       nf.AuxiliaryStaffFTE,
       nf.PercentTeacherWithQualifiedStatus
FROM School s
         LEFT JOIN CurrentDefaultNonFinancial nf on nf.URN = s.URN

GO