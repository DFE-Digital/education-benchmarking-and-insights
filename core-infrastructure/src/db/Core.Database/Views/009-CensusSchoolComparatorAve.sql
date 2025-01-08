DROP VIEW IF EXISTS VW_CensusSchoolDefaultComparatorAveTotal
GO

CREATE VIEW VW_CensusSchoolDefaultComparatorAveTotal AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
             CROSS APPLY Openjson(Pupil) Comparator
    WHERE RunType = 'default'
)
SELECT s.URN
     , s.RunId
     , Avg(c.TotalPupils)                       AS TotalPupils
     , Avg(c.Workforce)                         AS Workforce
     , Avg(c.WorkforceHeadcount)                AS WorkforceHeadcount
     , Avg(c.Teachers)                          AS Teachers
     , Avg(c.SeniorLeadership)                  AS SeniorLeadership
     , Avg(c.TeachingAssistant)                 AS TeachingAssistant
     , Avg(c.NonClassroomSupportStaff)          AS NonClassroomSupportStaff
     , Avg(c.AuxiliaryStaff)                    AS AuxiliaryStaff
     , Avg(c.PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
FROM comparators s
         INNER
             JOIN VW_CensusSchoolDefaultNormalisedTotal c
                  ON (
                      s.ComparatorURN = c.URN
                          AND s.RunId = c.RunId
                      )
GROUP
    BY s.URN
     , s.RunId
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultComparatorAveHeadcountPerFte
GO

CREATE VIEW VW_CensusSchoolDefaultComparatorAveHeadcountPerFte AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
             CROSS APPLY Openjson(Pupil) Comparator
    WHERE RunType = 'default'
)
SELECT s.URN
     , s.RunId
     , Avg(c.TotalPupils)                       AS TotalPupils
     , Avg(c.Workforce)                         AS Workforce
     , Avg(c.WorkforceHeadcount)                AS WorkforceHeadcount
     , Avg(c.Teachers)                          AS Teachers
     , Avg(c.SeniorLeadership)                  AS SeniorLeadership
     , Avg(c.TeachingAssistant)                 AS TeachingAssistant
     , Avg(c.NonClassroomSupportStaff)          AS NonClassroomSupportStaff
     , Avg(c.AuxiliaryStaff)                    AS AuxiliaryStaff
     , Avg(c.PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
FROM comparators s
         INNER
             JOIN VW_CensusSchoolDefaultNormalisedHeadcountPerFte c
                  ON (
                      s.ComparatorURN = c.URN
                          AND s.RunId = c.RunId
                      )
GROUP
    BY s.URN
     , s.RunId
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultComparatorAvePercentWorkforce
GO

CREATE VIEW VW_CensusSchoolDefaultComparatorAvePercentWorkforce AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
             CROSS APPLY Openjson(Pupil) Comparator
    WHERE RunType = 'default'
)
SELECT s.URN
     , s.RunId
     , Avg(c.TotalPupils)                       AS TotalPupils
     , Avg(c.Workforce)                         AS Workforce
     , Avg(c.WorkforceHeadcount)                AS WorkforceHeadcount
     , Avg(c.Teachers)                          AS Teachers
     , Avg(c.SeniorLeadership)                  AS SeniorLeadership
     , Avg(c.TeachingAssistant)                 AS TeachingAssistant
     , Avg(c.NonClassroomSupportStaff)          AS NonClassroomSupportStaff
     , Avg(c.AuxiliaryStaff)                    AS AuxiliaryStaff
     , Avg(c.PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
FROM comparators s
         INNER
             JOIN VW_CensusSchoolDefaultNormalisedPercentWorkforce c
                  ON (
                      s.ComparatorURN = c.URN
                          AND s.RunId = c.RunId
                      )
GROUP
    BY s.URN
     , s.RunId
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultComparatorAvePupilsPerStaffRole
GO

CREATE VIEW VW_CensusSchoolDefaultComparatorAvePupilsPerStaffRole AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
             CROSS APPLY Openjson(Pupil) Comparator
    WHERE RunType = 'default'
)
SELECT s.URN
     , s.RunId
     , Avg(c.TotalPupils)                       AS TotalPupils
     , Avg(c.Workforce)                         AS Workforce
     , Avg(c.WorkforceHeadcount)                AS WorkforceHeadcount
     , Avg(c.Teachers)                          AS Teachers
     , Avg(c.SeniorLeadership)                  AS SeniorLeadership
     , Avg(c.TeachingAssistant)                 AS TeachingAssistant
     , Avg(c.NonClassroomSupportStaff)          AS NonClassroomSupportStaff
     , Avg(c.AuxiliaryStaff)                    AS AuxiliaryStaff
     , Avg(c.PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
FROM comparators s
         INNER
             JOIN VW_CensusSchoolDefaultNormalisedPupilsPerStaffRole c
                  ON (
                      s.ComparatorURN = c.URN
                          AND s.RunId = c.RunId
                      )
GROUP
    BY s.URN
     , s.RunId
GO