DROP VIEW IF EXISTS VW_LASchoolRisksDefault
    GO

CREATE VIEW VW_LASchoolRisksDefault AS
SELECT ri.RunId
    , ri.URN
    , s.SchoolName
    , s.LACode
    , s.LAName
    , s.OverallPhase
    , ri.EducationalPerformance
    , ri.EducationalPerformanceMax
    , ri.Financial
    , ri.FinancialMax
    , ri.SchoolAndPupil
    , ri.SchoolAndPupilMax
    , ri.Overall
    , ri.OverallMax
    , ri.OverallGrade
FROM LASchoolRiskIndicatorsHeaders ri
    LEFT JOIN School s ON s.URN = ri.URN
GO

DROP VIEW IF EXISTS VW_LASchoolRisksDefaultCurrent
    GO

CREATE VIEW VW_LASchoolRisksDefaultCurrent AS
SELECT URN
     , SchoolName
     , LACode
     , LAName
     , OverallPhase
     , EducationalPerformance
     , EducationalPerformanceMax
     , Financial
     , FinancialMax
     , SchoolAndPupil
     , SchoolAndPupilMax
     , Overall
     , OverallMax
     , OverallGrade
FROM VW_LASchoolRisksDefault
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO
