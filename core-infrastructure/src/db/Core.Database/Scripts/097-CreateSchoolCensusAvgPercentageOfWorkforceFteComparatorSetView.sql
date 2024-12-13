DROP VIEW IF EXISTS SchoolCensusAvgPercentageOfWorkforceFteComparatorSet
GO

CREATE VIEW SchoolCensusAvgPercentageOfWorkforceFteComparatorSet AS
  WITH pupilComparator AS (
    SELECT RunId
         , URN
         , Comparator.value AS PupilComparatorURN
      FROM ComparatorSet
     CROSS APPLY Openjson(Pupil) Comparator
     WHERE RunType = 'default'
  )
  SELECT pupilComparator.URN
       , pupilComparator.RunId                  AS Year
       , Avg(TotalPupils)                       AS TotalPupils
       , Avg(Workforce)                         AS Workforce
       , Avg(WorkforceHeadcount)                AS WorkforceHeadcount
       , Avg(Teachers)                          AS Teachers
       , Avg(SeniorLeadership)                  AS SeniorLeadership
       , Avg(TeachingAssistant)                 AS TeachingAssistant
       , Avg(NonClassroomSupportStaff)          AS NonClassroomSupportStaff
       , Avg(AuxiliaryStaff)                    AS AuxiliaryStaff
       , Avg(PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
  FROM pupilComparator
   INNER
    JOIN SchoolCensusPercentageOfWorkforceFteHistoric
      ON (
               pupilComparator.PupilComparatorURN = SchoolCensusPercentageOfWorkforceFteHistoric.URN
           AND pupilComparator.RunId = SchoolCensusPercentageOfWorkforceFteHistoric.Year
         )
   GROUP
      BY pupilComparator.URN
       , pupilComparator.RunId
GO
