DROP VIEW IF EXISTS SchoolCensusPerFteHistoric
GO

CREATE VIEW SchoolCensusPerFteHistoric AS
  SELECT URN
       , Year
       , FinanceType
       , OverallPhase
       , TotalPupils
       , WorkforceHeadcount / WorkforceHeadcount                         AS WorkforceHeadcount
       , WorkforceHeadcount / WorkforceFTE                               AS Workforce
       , TeachersHeadcount / TeachersFTE                                 AS Teachers
       , SeniorLeadershipHeadcount / SeniorLeadershipFTE                 AS SeniorLeadership
       , TeachingAssistantHeadcount / TeachingAssistantFTE               AS TeachingAssistant
       , NonClassroomSupportStaffHeadcount / NonClassroomSupportStaffFTE AS NonClassroomSupportStaff
       , AuxiliaryStaffHeadcount / AuxiliaryStaffFTE                     AS AuxiliaryStaff
       , PercentTeacherWithQualifiedStatus
    FROM SchoolCensusHistoricWithNulls
GO
