DROP VIEW IF EXISTS VW_BalanceSchoolDefaultNationalAveActual
GO

CREATE VIEW VW_BalanceSchoolDefaultNationalAveActual AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg (InYearBalance)  AS 'InYearBalance'
     , Avg (RevenueReserve) AS 'RevenueReserve'
FROM VW_BalanceSchoolDefaultNormalisedActual
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultNationalAvePercentExpenditure
GO

CREATE VIEW VW_BalanceSchoolDefaultNationalAvePercentExpenditure AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg (InYearBalance)  AS 'InYearBalance'
     , Avg (RevenueReserve) AS 'RevenueReserve'
FROM VW_BalanceSchoolDefaultNormalisedPercentExpenditure
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO

DROP VIEW IF EXISTS VW_BalanceSchoolDefaultNationalAvePercentIncome
GO

CREATE VIEW VW_BalanceSchoolDefaultNationalAvePercentIncome AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg (InYearBalance)  AS 'InYearBalance'
     , Avg (RevenueReserve) AS 'RevenueReserve'
FROM VW_BalanceSchoolDefaultNormalisedPercentIncome
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO


DROP VIEW IF EXISTS VW_BalanceSchoolDefaultNationalAvePerUnit
GO

CREATE VIEW VW_BalanceSchoolDefaultNationalAvePerUnit AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg (InYearBalance)  AS 'InYearBalance'
     , Avg (RevenueReserve) AS 'RevenueReserve'
FROM VW_BalanceSchoolDefaultNormalisedPerUnit
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO