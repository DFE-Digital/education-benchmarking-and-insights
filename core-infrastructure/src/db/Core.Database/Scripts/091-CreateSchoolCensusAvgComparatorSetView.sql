DROP VIEW IF EXISTS SchoolCensusAvgComparatorSet
GO

CREATE VIEW SchoolCensusAvgComparatorSet AS
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
       , Avg(WorkforceFTE)                      AS Workforce
       , Avg(WorkforceHeadcount)                AS WorkforceHeadcount
       , Avg(TeachersHeadcount)                 AS Teachers
       , Avg(SeniorLeadershipHeadcount)         AS SeniorLeadership
       , Avg(TeachingAssistantHeadcount)        AS TeachingAssistant
       , Avg(NonClassroomSupportStaffHeadcount) AS NonClassroomSupportStaff
       , Avg(AuxiliaryStaffHeadcount)           AS AuxiliaryStaff
       , Avg(PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
  FROM pupilComparator
   INNER
    JOIN SchoolCensusHistoricWithNulls
      ON (
               pupilComparator.PupilComparatorURN = SchoolCensusHistoricWithNulls.URN
           AND pupilComparator.RunId = SchoolCensusHistoricWithNulls.Year
         )
   GROUP
      BY pupilComparator.URN
       , pupilComparator.RunId
GO
