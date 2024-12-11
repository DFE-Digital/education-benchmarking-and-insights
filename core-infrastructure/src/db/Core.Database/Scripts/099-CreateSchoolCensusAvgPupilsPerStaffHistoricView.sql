DROP VIEW IF EXISTS SchoolCensusAvgPupilsPerStaffHistoric
GO

CREATE VIEW SchoolCensusAvgPupilsPerStaffHistoric AS
  SELECT Year
       , FinanceType
       , OverallPhase
       , Avg(TotalPupils)                       AS TotalPupils
       , Avg(Workforce)                         AS Workforce
       , Avg(WorkforceHeadcount)                AS WorkforceHeadcount
       , Avg(Teachers)                          AS Teachers
       , Avg(SeniorLeadership)                  AS SeniorLeadership
       , Avg(TeachingAssistant)                 AS TeachingAssistant
    , Avg(AuxiliaryStaff)                       AS AuxiliaryStaff
       , Avg(NonClassroomSupportStaff)          AS NonClassroomSupportStaff
       , Avg(PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
    FROM SchoolCensusPupilsPerStaffHistoric
   GROUP
      BY Year
       , FinanceType
       , OverallPhase
GO
