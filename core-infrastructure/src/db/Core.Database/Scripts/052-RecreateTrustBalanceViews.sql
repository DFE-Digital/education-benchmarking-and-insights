IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'TrustBalanceHistoric')
    BEGIN
        DROP VIEW TrustBalanceHistoric
    END
GO


CREATE VIEW TrustBalanceHistoric
AS
SELECT t.CompanyNumber,
       t.TrustName,
       f.RunId 'Year',
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.InYearBalance,
       f.RevenueReserve,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.InYearBalanceCS,
       f.RevenueReserveCS
  FROM Trust t
  LEFT JOIN TrustFinancial f
    ON t.CompanyNumber = f.CompanyNumber
 CROSS JOIN (SELECT Value FROM Parameters p WHERE p.Name = 'LatestAARYear') y
 WHERE f.RunType = 'default'
   AND f.RunId BETWEEN y.Value - 5 AND y.Value

GO


IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'TrustBalance')
    BEGIN
        DROP VIEW TrustBalance
    END
GO

CREATE VIEW TrustBalance
AS
SELECT t.CompanyNumber,
       t.TrustName,
       TotalPupils,
       TotalIncome,
       TotalExpenditure,
       InYearBalance,
       RevenueReserve,
       TotalIncomeCS,
       TotalExpenditureCS,
       InYearBalanceCS,
       RevenueReserveCS
 FROM Trust t
 LEFT JOIN TrustFinancial f
   ON t.CompanyNumber = f.CompanyNumber
WHERE f.RunType = 'default'
  AND f.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')

GO
