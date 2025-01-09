DROP VIEW IF EXISTS VW_CensusSchoolDefaultNationalAveTotal
GO

CREATE VIEW VW_CensusSchoolDefaultNationalAveTotal AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg(TotalPupils)                       AS TotalPupils
     , Avg(Workforce)                         AS Workforce
     , Avg(WorkforceHeadcount)                AS WorkforceHeadcount
     , Avg(Teachers)                          AS Teachers
     , Avg(SeniorLeadership)                  AS SeniorLeadership
     , Avg(TeachingAssistant)                 AS TeachingAssistant
     , Avg(NonClassroomSupportStaff)          AS NonClassroomSupportStaff
     , Avg(AuxiliaryStaff)                    AS AuxiliaryStaff
     , Avg(PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
FROM VW_CensusSchoolDefaultNormalisedTotal
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultNationalAveHeadcountPerFte
GO

CREATE VIEW VW_CensusSchoolDefaultNationalAveHeadcountPerFte AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg(TotalPupils)                       AS TotalPupils
     , Avg(Workforce)                         AS Workforce
     , Avg(WorkforceHeadcount)                AS WorkforceHeadcount
     , Avg(Teachers)                          AS Teachers
     , Avg(SeniorLeadership)                  AS SeniorLeadership
     , Avg(TeachingAssistant)                 AS TeachingAssistant
     , Avg(NonClassroomSupportStaff)          AS NonClassroomSupportStaff
     , Avg(AuxiliaryStaff)                    AS AuxiliaryStaff
     , Avg(PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
FROM VW_CensusSchoolDefaultNormalisedHeadcountPerFte
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultNationalAvePercentWorkforce
GO

CREATE VIEW VW_CensusSchoolDefaultNationalAvePercentWorkforce AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg(TotalPupils)                       AS TotalPupils
     , Avg(Workforce)                         AS Workforce
     , Avg(WorkforceHeadcount)                AS WorkforceHeadcount
     , Avg(Teachers)                          AS Teachers
     , Avg(SeniorLeadership)                  AS SeniorLeadership
     , Avg(TeachingAssistant)                 AS TeachingAssistant
     , Avg(NonClassroomSupportStaff)          AS NonClassroomSupportStaff
     , Avg(AuxiliaryStaff)                    AS AuxiliaryStaff
     , Avg(PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
FROM VW_CensusSchoolDefaultNormalisedPercentWorkforce
GROUP
    BY RunId
     , FinanceType
     , OverallPhase    
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultNationalAvePupilsPerStaffRole
GO

CREATE VIEW VW_CensusSchoolDefaultNationalAvePupilsPerStaffRole AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg(TotalPupils)                       AS TotalPupils
     , Avg(Workforce)                         AS Workforce
     , Avg(WorkforceHeadcount)                AS WorkforceHeadcount
     , Avg(Teachers)                          AS Teachers
     , Avg(SeniorLeadership)                  AS SeniorLeadership
     , Avg(TeachingAssistant)                 AS TeachingAssistant
     , Avg(NonClassroomSupportStaff)          AS NonClassroomSupportStaff
     , Avg(AuxiliaryStaff)                    AS AuxiliaryStaff
     , Avg(PercentTeacherWithQualifiedStatus) AS PercentTeacherWithQualifiedStatus
FROM VW_CensusSchoolDefaultNormalisedPupilsPerStaffRole
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO