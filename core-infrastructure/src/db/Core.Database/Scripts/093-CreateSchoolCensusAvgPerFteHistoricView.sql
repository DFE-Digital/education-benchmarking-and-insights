DROP VIEW IF EXISTS SchoolCensusAvgPerFteHistoric
GO

CREATE VIEW SchoolCensusAvgPerFteHistoric AS
  SELECT Year
       , FinanceType
       , OverallPhase
       , Avg(TotalPupils)                       AS TotalPupils
       , Avg(Workforce)                         AS Workforce
       , Avg(WorkforceHeadcount)                AS WorkforceHeadcount
       , Avg(Teachers)                          AS Teachers
       , Avg(SeniorLeadership)                  AS SeniorLeadership
       , Avg(TeachingAssistant)                 AS TeachingAssistant
       , Avg(NonClassroomSupportStaff)          AS NonClassroomSupportStaff
       , Avg(AuxiliaryStaff)                    AS AuxiliaryStaff
       , Avg(PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
    FROM SchoolCensusPerFteHistoric
   GROUP
      BY Year
       , FinanceType
       , OverallPhase
GO
