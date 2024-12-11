DROP VIEW IF EXISTS SchoolCensusPercentageOfWorkforceFteHistoric
GO

CREATE VIEW SchoolCensusPercentageOfWorkforceFteHistoric AS
  SELECT URN
       , Year
       , FinanceType
       , OverallPhase
       , TotalPupils
       , (WorkforceHeadcount / WorkforceFTE) * 100                AS WorkforceHeadcount
       , (WorkforceFTE / WorkforceFTE) * 100                      AS Workforce
       , (TeachersFTE / WorkforceFTE) * 100                       AS Teachers
       , (SeniorLeadershipFTE / WorkforceFTE) * 100               AS SeniorLeadership
       , (TeachingAssistantFTE / WorkforceFTE) * 100              AS TeachingAssistant
       , (NonClassroomSupportStaffFTE / WorkforceFTE) * 100       AS NonClassroomSupportStaff
       , (AuxiliaryStaffFTE / WorkforceFTE) * 100                 AS AuxiliaryStaff
       , PercentTeacherWithQualifiedStatus
    FROM SchoolCensusHistoricWithNulls
GO
