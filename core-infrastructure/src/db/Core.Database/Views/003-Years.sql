DROP VIEW IF EXISTS VW_SchoolYears
GO

CREATE VIEW VW_SchoolYears AS
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

DROP VIEW IF EXISTS VW_TrustYears
GO

CREATE VIEW VW_TrustYears AS
SELECT t.CompanyNumber,
       y.Value     AS 'EndYear',
       y.Value - 5 AS 'StartYear'
FROM Trust t,
     (SELECT Value FROM Parameters WHERE Name = 'LatestAARYear') y
GO