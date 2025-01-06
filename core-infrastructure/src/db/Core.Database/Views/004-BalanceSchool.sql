DROP VIEW IF EXISTS VW_BalanceSchoolDefaultCurrentActual
GO

CREATE VIEW VW_BalanceSchoolDefaultCurrentActual AS
SELECT s.URN,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       f.PeriodCoveredByReturn,
       f.InYearBalance,
       f.RevenueReserve
FROM School s
         LEFT JOIN VW_FinancialDefaultCurrent f on f.URN = s.URN
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultActual
GO

CREATE VIEW VW_BalanceSchoolDefaultActual AS
SELECT RunId,
       URN,
       InYearBalance,
       RevenueReserve
FROM Financial
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultPercentExpenditure
GO

CREATE VIEW VW_BalanceSchoolDefaultPercentExpenditure AS
SELECT RunId,
       URN,
       IIF(TotalExpenditure != 0, (InYearBalance / TotalExpenditure) * 100, NULL) AS 'InYearBalance',
       IIF(TotalExpenditure != 0, (RevenueReserve / TotalExpenditure) * 100, NULL) AS 'RevenueReserve'
FROM Financial
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultPercentIncome
GO

CREATE VIEW VW_BalanceSchoolDefaultPercentIncome AS
SELECT RunId,
       URN,
       IIF(TotalIncome != 0, (InYearBalance / TotalIncome) * 100, NULL) AS 'InYearBalance',
       IIF(TotalIncome != 0, (RevenueReserve / TotalIncome) * 100, NULL) AS 'RevenueReserve'
FROM Financial
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultPerUnit
GO

CREATE VIEW VW_BalanceSchoolDefaultPerUnit AS
SELECT RunId,
       URN,
       IIF(TotalPupils != 0, InYearBalance / TotalPupils, NULL) AS 'InYearBalance',
       IIF(TotalPupils != 0, RevenueReserve / TotalPupils, NULL) AS 'RevenueReserve'
FROM Financial
WHERE RunType = 'default'
GO