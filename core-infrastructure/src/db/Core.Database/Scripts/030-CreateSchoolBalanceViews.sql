IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolBalanceHistoric')
    BEGIN
        DROP VIEW SchoolBalanceHistoric
    END
GO

CREATE VIEW SchoolBalanceHistoric
AS
SELECT s.URN,
       f.RunId 'Year',
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.InYearBalance,
       f.RevenueReserve,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.InYearBalanceCS,
       f.RevenueReserveCS
FROM School s,
     (SELECT Value FROM Parameters p WHERE p.Name = 'LatestAARYear') y,
     Financial f
WHERE s.URN = f.URN
  AND f.RunType = 'default'
  AND s.FinanceType = 'Academy'
  AND f.RunId BETWEEN y.Value - 5 AND y.Value
UNION
SELECT s.URN,
       f.RunId 'Year',
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.InYearBalance,
       f.RevenueReserve,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.InYearBalanceCS,
       f.RevenueReserveCS
FROM School s,
     (SELECT Value FROM Parameters p WHERE p.Name = 'LatestCFRYear') y,
     Financial f
WHERE s.URN = f.URN
  AND f.RunType = 'default'
  AND s.FinanceType = 'Maintained'
  AND f.RunId BETWEEN y.Value - 5 AND y.Value

GO


IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolBalance')
    BEGIN
        DROP VIEW SchoolBalance
    END
GO

CREATE VIEW SchoolBalance
AS
SELECT s.URN,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.InYearBalance,
       f.RevenueReserve,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.InYearBalanceCS,
       f.RevenueReserveCS
FROM School s
         LEFT JOIN CurrentDefaultFinancial f on f.URN = s.URN

GO