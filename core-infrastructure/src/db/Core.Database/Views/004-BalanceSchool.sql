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
         LEFT JOIN Financial f on f.URN = s.URN
WHERE RunType = 'default'
  AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultActual
GO

CREATE VIEW VW_BalanceSchoolDefaultActual AS
SELECT RunId,
       URN,
       OverallPhase,
       FinanceType,
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
       OverallPhase,
       FinanceType,
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
       OverallPhase,
       FinanceType,
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
       OverallPhase,
       FinanceType,
       IIF(TotalPupils != 0, InYearBalance / TotalPupils, NULL) AS 'InYearBalance',
       IIF(TotalPupils != 0, RevenueReserve / TotalPupils, NULL) AS 'RevenueReserve'
FROM Financial
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultNormalisedActual
GO

CREATE VIEW VW_BalanceSchoolDefaultNormalisedActual AS
SELECT RunId,
       URN,
       OverallPhase,
       FinanceType,
       IIF (InYearBalance IS NULL OR InYearBalance <= 0, NULL, InYearBalance) AS 'InYearBalance',
       IIF (RevenueReserve IS NULL OR RevenueReserve <= 0, NULL, RevenueReserve) AS 'RevenueReserve'
FROM VW_BalanceSchoolDefaultActual
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultNormalisedPercentExpenditure
GO

CREATE VIEW VW_BalanceSchoolDefaultNormalisedPercentExpenditure AS
SELECT RunId,
       URN,
       OverallPhase,
       FinanceType,
       IIF (InYearBalance IS NULL OR InYearBalance <= 0, NULL, InYearBalance) AS 'InYearBalance',
       IIF (RevenueReserve IS NULL OR RevenueReserve <= 0, NULL, RevenueReserve) AS 'RevenueReserve'
FROM VW_BalanceSchoolDefaultPercentExpenditure
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultNormalisedPercentIncome
GO

CREATE VIEW VW_BalanceSchoolDefaultNormalisedPercentIncome AS
SELECT RunId,
       URN,
       OverallPhase,
       FinanceType,
       IIF (InYearBalance IS NULL OR InYearBalance <= 0, NULL, InYearBalance) AS 'InYearBalance',
       IIF (RevenueReserve IS NULL OR RevenueReserve <= 0, NULL, RevenueReserve) AS 'RevenueReserve'
FROM VW_BalanceSchoolDefaultPercentIncome
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultNormalisedPerUnit
GO

CREATE VIEW VW_BalanceSchoolDefaultNormalisedPerUnit AS
SELECT RunId,
       URN,
       OverallPhase,
       FinanceType,
       IIF (InYearBalance IS NULL OR InYearBalance <= 0, NULL, InYearBalance) AS 'InYearBalance',
       IIF (RevenueReserve IS NULL OR RevenueReserve <= 0, NULL, RevenueReserve) AS 'RevenueReserve'
FROM VW_BalanceSchoolDefaultPerUnit
GO