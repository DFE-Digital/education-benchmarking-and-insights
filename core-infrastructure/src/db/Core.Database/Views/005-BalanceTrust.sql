DROP VIEW IF EXISTS VW_BalanceTrustDefaultCurrentActual
GO

CREATE VIEW VW_BalanceTrustDefaultCurrentActual AS
SELECT t.CompanyNumber,
       t.TrustName,
       f.InYearBalance,
       f.InYearBalanceCS,
       f.InYearBalanceSchool,
       f.RevenueReserve
FROM Trust t
         LEFT JOIN VW_TrustFinancialDefaultCurrent f on f.CompanyNumber = t.CompanyNumber
GO


DROP VIEW IF EXISTS VW_BalanceTrustDefaultCurrentPercentExpenditure
GO

CREATE VIEW VW_BalanceTrustDefaultCurrentPercentExpenditure AS
SELECT t.CompanyNumber,
       t.TrustName,   
       IIF(f.TotalExpenditure != 0, (f.InYearBalance / f.TotalExpenditure) * 100, NULL) AS 'InYearBalance',
       IIF(f.TotalExpenditureCS != 0, (f.InYearBalanceCS / f.TotalExpenditureCS) * 100, NULL) AS 'InYearBalanceCS',
       IIF(f.TotalExpenditureSchool != 0, (f.InYearBalanceSchool / f.TotalExpenditureSchool) * 100, NULL) AS 'InYearBalanceSchool',
       IIF(f.TotalExpenditure != 0, (f.RevenueReserve / f.TotalExpenditure) * 100, NULL) AS 'RevenueReserve'
FROM Trust t
         LEFT JOIN VW_TrustFinancialDefaultCurrent f on f.CompanyNumber = t.CompanyNumber
GO

DROP VIEW IF EXISTS VW_BalanceTrustDefaultCurrentPercentIncome
GO

CREATE VIEW VW_BalanceTrustDefaultCurrentPercentIncome AS
SELECT t.CompanyNumber,
       t.TrustName,
       IIF(f.TotalIncome != 0, (f.InYearBalance / f.TotalIncome) * 100, NULL) AS 'InYearBalance',
       IIF(f.TotalIncomeCS != 0, (f.InYearBalanceCS / f.TotalIncomeCS) * 100, NULL) AS 'InYearBalanceCS',
       IIF(f.TotalIncomeSchool != 0, (f.InYearBalanceSchool / f.TotalIncomeSchool) * 100, NULL) AS 'InYearBalanceSchool',
       IIF(f.TotalIncome != 0, (f.RevenueReserve / f.TotalExpenditure) * 100, NULL) AS 'RevenueReserve'
FROM Trust t
         LEFT JOIN VW_TrustFinancialDefaultCurrent f on f.CompanyNumber = t.CompanyNumber
GO

DROP VIEW IF EXISTS VW_BalanceTrustDefaultCurrentPerUnit
GO

CREATE VIEW VW_BalanceTrustDefaultCurrentPerUnit AS
SELECT t.CompanyNumber,
       t.TrustName,
       IIF(f.TotalPupils != 0, f.InYearBalance / f.TotalPupils, NULL) AS 'InYearBalance',
       IIF(f.TotalPupils != 0, f.InYearBalanceCS / f.TotalPupils, NULL) AS 'InYearBalanceCS',
       IIF(f.TotalPupils != 0, f.InYearBalanceSchool / f.TotalPupils, NULL) AS 'InYearBalanceSchool',
       IIF(f.TotalPupils != 0, f.RevenueReserve / f.TotalPupils, NULL) AS 'RevenueReserve'
FROM Trust t
         LEFT JOIN VW_TrustFinancialDefaultCurrent f on f.CompanyNumber = t.CompanyNumber
GO

DROP VIEW IF EXISTS VW_BalanceTrustDefaultActual
GO

CREATE VIEW VW_BalanceTrustDefaultActual AS
SELECT CompanyNumber,
       RunId,
       InYearBalance,    
       RevenueReserve
FROM VW_TrustFinancialDefault
GO

DROP VIEW IF EXISTS VW_BalanceTrustDefaultPercentExpenditure
GO

CREATE VIEW VW_BalanceTrustDefaultPercentExpenditure AS
SELECT CompanyNumber,
       RunId,
       IIF(TotalExpenditure != 0, (InYearBalance / TotalExpenditure) * 100, NULL) AS 'InYearBalance',
       IIF(TotalExpenditure != 0, (RevenueReserve / TotalExpenditure) * 100, NULL) AS 'RevenueReserve'
FROM VW_TrustFinancialDefault
GO

DROP VIEW IF EXISTS VW_BalanceTrustDefaultPercentIncome
GO

CREATE VIEW VW_BalanceTrustDefaultPercentIncome AS
SELECT CompanyNumber,
       RunId,
       IIF(TotalIncome != 0, (InYearBalance / TotalIncome) * 100, NULL) AS 'InYearBalance',
       IIF(TotalIncome != 0, (RevenueReserve / TotalExpenditure) * 100, NULL) AS 'RevenueReserve'
FROM VW_TrustFinancialDefault
GO

DROP VIEW IF EXISTS VW_BalanceTrustDefaultPerUnit
GO

CREATE VIEW VW_BalanceTrustDefaultPerUnit AS
SELECT CompanyNumber,
       RunId,
       IIF(TotalPupils != 0, InYearBalance / TotalPupils, NULL) AS 'InYearBalance',
       IIF(TotalPupils != 0, RevenueReserve / TotalPupils, NULL) AS 'RevenueReserve'
FROM VW_TrustFinancialDefault
GO