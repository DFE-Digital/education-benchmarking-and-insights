DROP VIEW IF EXISTS VW_LASchoolRisksOverviewDefault
    GO

CREATE VIEW VW_LASchoolRisksOverviewDefault AS
SELECT ri.RunId
    , ri.URN
    , s.SchoolName
    , s.LACode
    , s.LAName
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

DROP VIEW IF EXISTS VW_LASchoolRisksOverviewDefaultCurrent
    GO

CREATE VIEW VW_LASchoolRisksOverviewDefaultCurrent AS
SELECT URN
     , SchoolName
     , LACode
     , LAName
     , EducationalPerformance
     , EducationalPerformanceMax
     , Financial
     , FinancialMax
     , SchoolAndPupil
     , SchoolAndPupilMax
     , Overall
     , OverallMax
     , OverallGrade
FROM VW_LASchoolRisksOverviewDefault
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
    GO
