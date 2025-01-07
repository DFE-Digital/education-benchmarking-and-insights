DROP VIEW IF EXISTS VW_CensusSchoolTotal
GO

CREATE VIEW VW_CensusSchoolTotal AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       WorkforceFTE AS 'Workforce',
       WorkforceHeadcount,
       TeachersFTE AS 'Teachers',
       SeniorLeadershipFTE AS 'SeniorLeadership',
       TeachingAssistantFTE AS 'TeachingAssistant',
       NonClassroomSupportStaffFTE AS 'NonClassroomSupportStaff',
       AuxiliaryStaffFTE AS 'AuxiliaryStaff',
       PercentTeacherWithQualifiedStatus
FROM NonFinancial
GO

DROP VIEW IF EXISTS VW_CensusSchoolHeadcountPerFte
GO

CREATE VIEW VW_CensusSchoolHeadcountPerFte AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       IIF(WorkforceFTE != 0, WorkforceHeadcount /  WorkforceFTE, NULL) AS 'Workforce',
       IIF(WorkforceHeadcount != 0, WorkforceHeadcount /  WorkforceHeadcount, NULL) AS 'WorkforceHeadcount',
       IIF(TeachersFTE != 0, TeachersHeadcount /  TeachersFTE, NULL) AS 'Teachers',
       IIF(SeniorLeadershipFTE != 0, SeniorLeadershipHeadcount /  SeniorLeadershipFTE, NULL) AS 'SeniorLeadership',
       IIF(TeachingAssistantFTE != 0, TeachingAssistantHeadcount /  TeachingAssistantFTE, NULL) AS 'TeachingAssistant',
       IIF(NonClassroomSupportStaffFTE != 0, NonClassroomSupportStaffHeadcount /  NonClassroomSupportStaffFTE, NULL) AS 'NonClassroomSupportStaff',
       IIF(AuxiliaryStaffFTE != 0, AuxiliaryStaffHeadcount /  AuxiliaryStaffFTE, NULL) AS 'AuxiliaryStaff',
       PercentTeacherWithQualifiedStatus
FROM NonFinancial
GO

DROP VIEW IF EXISTS VW_CensusSchoolPercentWorkforce
GO

CREATE VIEW VW_CensusSchoolPercentWorkforce AS
SELECT RunId,
       RunType,       
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       IIF(WorkforceFTE != 0, (WorkforceFTE /  WorkforceFTE) * 100, NULL) AS 'Workforce',
       IIF(WorkforceHeadcount != 0, (WorkforceHeadcount /  WorkforceHeadcount) * 100, NULL) AS 'WorkforceHeadcount',
       IIF(WorkforceFTE != 0, (TeachersFTE /  WorkforceFTE) * 100, NULL) AS 'Teachers',
       IIF(WorkforceFTE != 0, (SeniorLeadershipFTE /  WorkforceFTE) * 100, NULL) AS 'SeniorLeadership',
       IIF(WorkforceFTE != 0, (TeachingAssistantFTE /  WorkforceFTE) * 100, NULL) AS 'TeachingAssistant',
       IIF(WorkforceFTE != 0, (NonClassroomSupportStaffFTE /  WorkforceFTE) * 100, NULL) AS 'NonClassroomSupportStaff',
       IIF(WorkforceFTE != 0, (AuxiliaryStaffFTE /  WorkforceFTE) * 100, NULL) AS 'AuxiliaryStaff',
       PercentTeacherWithQualifiedStatus
FROM NonFinancial
GO

DROP VIEW IF EXISTS VW_CensusSchoolPupilsPerStaffRole
GO

CREATE VIEW VW_CensusSchoolPupilsPerStaffRole AS
SELECT RunId,
       RunType,       
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       IIF(WorkforceFTE != 0, TotalPupils /  WorkforceFTE, NULL) AS 'Workforce',
       IIF(WorkforceHeadcount != 0, TotalPupils /  WorkforceHeadcount, NULL) AS 'WorkforceHeadcount',
       IIF(TeachersFTE != 0, TotalPupils /  TeachersFTE, NULL) AS 'Teachers',
       IIF(SeniorLeadershipFTE != 0, TotalPupils /  SeniorLeadershipFTE, NULL) AS 'SeniorLeadership',
       IIF(TeachingAssistantFTE != 0, TotalPupils /  TeachingAssistantFTE, NULL) AS 'TeachingAssistant',
       IIF(NonClassroomSupportStaffFTE != 0, TotalPupils /  NonClassroomSupportStaffFTE, NULL) AS 'NonClassroomSupportStaff',
       IIF(AuxiliaryStaffFTE != 0, TotalPupils /  AuxiliaryStaffFTE, NULL) AS 'AuxiliaryStaff',
       PercentTeacherWithQualifiedStatus
FROM NonFinancial
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultNormalisedTotal
GO

CREATE VIEW VW_CensusSchoolDefaultNormalisedTotal AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       IIF(Workforce IS NULL OR Workforce = 0, NULL, Workforce) AS 'Workforce',
       IIF(WorkforceHeadcount IS NULL OR WorkforceHeadcount = 0, NULL, WorkforceHeadcount) AS 'WorkforceHeadcount',
       IIF(Teachers IS NULL OR Teachers = 0, NULL,Teachers) AS 'Teachers',
       IIF(SeniorLeadership IS NULL OR SeniorLeadership = 0, NULL, SeniorLeadership) AS 'SeniorLeadership',
       IIF(TeachingAssistant IS NULL OR TeachingAssistant = 0, NULL, TeachingAssistant) AS 'TeachingAssistant',
       IIF(NonClassroomSupportStaff IS NULL OR NonClassroomSupportStaff = 0, NULL, NonClassroomSupportStaff) AS 'NonClassroomSupportStaff',
       IIF(AuxiliaryStaff IS NULL OR AuxiliaryStaff = 0, NULL, AuxiliaryStaff) AS 'AuxiliaryStaff',
       IIF(PercentTeacherWithQualifiedStatus IS NULL OR PercentTeacherWithQualifiedStatus = 0, NULL, PercentTeacherWithQualifiedStatus) AS 'PercentTeacherWithQualifiedStatus'
FROM VW_CensusSchoolTotal
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultNormalisedHeadcountPerFte
GO

CREATE VIEW VW_CensusSchoolDefaultNormalisedHeadcountPerFte AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       IIF(Workforce IS NULL OR Workforce = 0, NULL, Workforce) AS 'Workforce',
       IIF(WorkforceHeadcount IS NULL OR WorkforceHeadcount = 0, NULL, WorkforceHeadcount) AS 'WorkforceHeadcount',
       IIF(Teachers IS NULL OR Teachers = 0, NULL,Teachers) AS 'Teachers',
       IIF(SeniorLeadership IS NULL OR SeniorLeadership = 0, NULL, SeniorLeadership) AS 'SeniorLeadership',
       IIF(TeachingAssistant IS NULL OR TeachingAssistant = 0, NULL, TeachingAssistant) AS 'TeachingAssistant',
       IIF(NonClassroomSupportStaff IS NULL OR NonClassroomSupportStaff = 0, NULL, NonClassroomSupportStaff) AS 'NonClassroomSupportStaff',
       IIF(AuxiliaryStaff IS NULL OR AuxiliaryStaff = 0, NULL, AuxiliaryStaff) AS 'AuxiliaryStaff',
       IIF(PercentTeacherWithQualifiedStatus IS NULL OR PercentTeacherWithQualifiedStatus = 0, NULL, PercentTeacherWithQualifiedStatus) AS 'PercentTeacherWithQualifiedStatus'
FROM VW_CensusSchoolHeadcountPerFte
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultNormalisedPercentWorkforce
GO

CREATE VIEW VW_CensusSchoolDefaultNormalisedPercentWorkforce AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       IIF(Workforce IS NULL OR Workforce = 0, NULL, Workforce) AS 'Workforce',
       IIF(WorkforceHeadcount IS NULL OR WorkforceHeadcount = 0, NULL, WorkforceHeadcount) AS 'WorkforceHeadcount',
       IIF(Teachers IS NULL OR Teachers = 0, NULL,Teachers) AS 'Teachers',
       IIF(SeniorLeadership IS NULL OR SeniorLeadership = 0, NULL, SeniorLeadership) AS 'SeniorLeadership',
       IIF(TeachingAssistant IS NULL OR TeachingAssistant = 0, NULL, TeachingAssistant) AS 'TeachingAssistant',
       IIF(NonClassroomSupportStaff IS NULL OR NonClassroomSupportStaff = 0, NULL, NonClassroomSupportStaff) AS 'NonClassroomSupportStaff',
       IIF(AuxiliaryStaff IS NULL OR AuxiliaryStaff = 0, NULL, AuxiliaryStaff) AS 'AuxiliaryStaff',
       IIF(PercentTeacherWithQualifiedStatus IS NULL OR PercentTeacherWithQualifiedStatus = 0, NULL, PercentTeacherWithQualifiedStatus) AS 'PercentTeacherWithQualifiedStatus'
FROM VW_CensusSchoolPercentWorkforce
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultNormalisedPupilsPerStaffRole
GO

CREATE VIEW VW_CensusSchoolDefaultNormalisedPupilsPerStaffRole AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       IIF(Workforce IS NULL OR Workforce = 0, NULL, Workforce) AS 'Workforce',
       IIF(WorkforceHeadcount IS NULL OR WorkforceHeadcount = 0, NULL, WorkforceHeadcount) AS 'WorkforceHeadcount',
       IIF(Teachers IS NULL OR Teachers = 0, NULL,Teachers) AS 'Teachers',
       IIF(SeniorLeadership IS NULL OR SeniorLeadership = 0, NULL, SeniorLeadership) AS 'SeniorLeadership',
       IIF(TeachingAssistant IS NULL OR TeachingAssistant = 0, NULL, TeachingAssistant) AS 'TeachingAssistant',
       IIF(NonClassroomSupportStaff IS NULL OR NonClassroomSupportStaff = 0, NULL, NonClassroomSupportStaff) AS 'NonClassroomSupportStaff',
       IIF(AuxiliaryStaff IS NULL OR AuxiliaryStaff = 0, NULL, AuxiliaryStaff) AS 'AuxiliaryStaff',
       IIF(PercentTeacherWithQualifiedStatus IS NULL OR PercentTeacherWithQualifiedStatus = 0, NULL, PercentTeacherWithQualifiedStatus) AS 'PercentTeacherWithQualifiedStatus'
FROM VW_CensusSchoolPupilsPerStaffRole
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultTotal
GO

CREATE VIEW VW_CensusSchoolDefaultTotal AS
SELECT *
FROM VW_CensusSchoolTotal
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultHeadcountPerFte
GO

CREATE VIEW VW_CensusSchoolDefaultHeadcountPerFte AS
SELECT *
FROM VW_CensusSchoolHeadcountPerFte
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultPercentWorkforce
GO

CREATE VIEW VW_CensusSchoolDefaultPercentWorkforce AS
SELECT *
FROM VW_CensusSchoolPercentWorkforce
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultPupilsPerStaffRole
GO

CREATE VIEW VW_CensusSchoolDefaultPupilsPerStaffRole AS
SELECT *
FROM VW_CensusSchoolPupilsPerStaffRole
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultCurrentTotal
GO

CREATE VIEW VW_CensusSchoolDefaultCurrentTotal AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
       s.LaCode
FROM School s
         LEFT JOIN VW_CensusSchoolDefaultTotal c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultCurrentHeadcountPerFte
GO

CREATE VIEW VW_CensusSchoolDefaultCurrentHeadcountPerFte AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
       s.LaCode
FROM School s
         LEFT JOIN VW_CensusSchoolDefaultHeadcountPerFte c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultCurrentPercentWorkforce
GO

CREATE VIEW VW_CensusSchoolDefaultCurrentPercentWorkforce AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
       s.LaCode
FROM School s
         LEFT JOIN VW_CensusSchoolDefaultPercentWorkforce c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_CensusSchoolDefaultCurrentPupilsPerStaffRole
GO

CREATE VIEW VW_CensusSchoolDefaultCurrentPupilsPerStaffRole AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
       s.LaCode
FROM School s
         LEFT JOIN VW_CensusSchoolDefaultPupilsPerStaffRole c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_CensusSchoolCustomTotal
GO

CREATE VIEW VW_CensusSchoolCustomTotal AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_CensusSchoolTotal c ON c.URN = s.URN
WHERE c.RunType = 'custom'
GO

DROP VIEW IF EXISTS VW_CensusSchoolCustomHeadcountPerFte
GO

CREATE VIEW VW_CensusSchoolCustomHeadcountPerFte AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_CensusSchoolHeadcountPerFte c ON c.URN = s.URN
WHERE c.RunType = 'custom'
GO

DROP VIEW IF EXISTS VW_CensusSchoolCustomPercentWorkforce
GO

CREATE VIEW VW_CensusSchoolCustomPercentWorkforce AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_CensusSchoolPercentWorkforce c ON c.URN = s.URN
WHERE c.RunType = 'custom'
GO

DROP VIEW IF EXISTS VW_CensusSchoolCustomPupilsPerStaffRole
GO

CREATE VIEW VW_CensusSchoolCustomPupilsPerStaffRole AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_CensusSchoolPupilsPerStaffRole c ON c.URN = s.URN
WHERE c.RunType = 'custom'
GO