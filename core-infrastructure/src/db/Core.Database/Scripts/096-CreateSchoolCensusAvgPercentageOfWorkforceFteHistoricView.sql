DROP VIEW IF EXISTS SchoolCensusAvgPercentageOfWorkforceFteHistoric
GO

CREATE VIEW SchoolCensusAvgPercentageOfWorkforceFteHistoric AS
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
    FROM SchoolCensusPercentageOfWorkforceFteHistoric
   GROUP
      BY Year
       , FinanceType
       , OverallPhase
GO
