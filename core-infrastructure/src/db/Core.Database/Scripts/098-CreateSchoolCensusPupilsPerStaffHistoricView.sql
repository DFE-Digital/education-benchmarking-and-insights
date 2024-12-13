DROP VIEW IF EXISTS SchoolCensusPupilsPerStaffHistoric
GO

CREATE VIEW SchoolCensusPupilsPerStaffHistoric AS
  SELECT URN
       , Year
       , FinanceType
       , OverallPhase
       , TotalPupils
       , TotalPupils / WorkforceFTE                      AS Workforce
       , TotalPupils / WorkforceHeadcount                AS WorkforceHeadcount
       , TotalPupils / TeachersFTE                       AS Teachers
       , TotalPupils / SeniorLeadershipFTE               AS SeniorLeadership
       , TotalPupils / TeachingAssistantFTE              AS TeachingAssistant
       , TotalPupils / NonClassroomSupportStaffFTE       AS NonClassroomSupportStaff
       , TotalPupils / AuxiliaryStaffFTE                 AS AuxiliaryStaff
       , PercentTeacherWithQualifiedStatus
    FROM SchoolCensusHistoricWithNulls
GO
