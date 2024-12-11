DROP VIEW IF EXISTS SchoolCensusAvgHistoric
GO

CREATE VIEW SchoolCensusAvgHistoric AS
  SELECT Year
       , FinanceType
       , OverallPhase
       , Avg(TotalPupils)                       AS TotalPupils
       , Avg(WorkforceFTE)                      AS Workforce
       , Avg(WorkforceHeadcount)                AS WorkforceHeadcount
       , Avg(TeachersHeadcount)                 AS Teachers
       , Avg(SeniorLeadershipHeadcount)         AS SeniorLeadership
       , Avg(TeachingAssistantHeadcount)        AS TeachingAssistant
       , Avg(NonClassroomSupportStaffHeadcount) AS NonClassroomSupportStaff
       , Avg(AuxiliaryStaffHeadcount)           AS AuxiliaryStaff
       , Avg(PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
    FROM SchoolCensusHistoricWithNulls
   GROUP
      BY Year
       , FinanceType
       , OverallPhase
GO
