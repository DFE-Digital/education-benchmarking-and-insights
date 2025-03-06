DROP VIEW IF EXISTS VW_YearsSchool
GO

CREATE VIEW VW_YearsSchool AS
SELECT s.URN,
       y.Value     AS 'EndYear',
       y.Value - 5 AS 'StartYear'
FROM School s,
     (SELECT Value FROM Parameters WHERE Name = 'LatestAARYear') y
WHERE s.FinanceType = 'Academy'
UNION ALL
SELECT s.URN,
       y.Value     AS 'EndYear',
       y.Value - 5 AS 'StartYear'
FROM School s,
     (SELECT Value FROM Parameters WHERE Name = 'LatestCFRYear') y
WHERE s.FinanceType = 'Maintained'
GO

DROP VIEW IF EXISTS VW_YearsTrust
GO

CREATE VIEW VW_YearsTrust AS
SELECT t.CompanyNumber,
       y.Value     AS 'EndYear',
       y.Value - 5 AS 'StartYear'
FROM Trust t,
     (SELECT Value FROM Parameters WHERE Name = 'LatestAARYear') y
GO

CREATE VIEW VW_YearsLocalAuthority AS
SELECT l.Code,
       y.Value     AS 'EndYear',
       y.Value - 5 AS 'StartYear'
FROM LocalAuthority l,
     (SELECT Value FROM Parameters WHERE Name = 'LatestS251Year') y
GO

DROP VIEW IF EXISTS VW_YearsOverallPhase
GO

CREATE VIEW VW_YearsOverallPhase AS
SELECT s.OverallPhase,
       s.FinanceType,
       y.Value     AS 'EndYear',
       y.Value - 5 AS 'StartYear'
FROM (SELECT OverallPhase, FinanceType
      FROM School
      WHERE FinanceType = 'Academy'
      GROUP BY OverallPhase, FinanceType) s,
     (SELECT Value FROM Parameters WHERE Name = 'LatestAARYear') y
UNION ALL
SELECT s.OverallPhase,
       s.FinanceType,
       y.Value     AS 'EndYear',
       y.Value - 5 AS 'StartYear'
FROM (SELECT OverallPhase, FinanceType
      FROM School
      WHERE FinanceType = 'Maintained'
      GROUP BY OverallPhase, FinanceType) s,
     (SELECT Value FROM Parameters WHERE Name = 'LatestCFRYear') y
GO