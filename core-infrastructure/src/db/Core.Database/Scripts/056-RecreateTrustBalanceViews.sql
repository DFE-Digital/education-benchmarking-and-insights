ALTER VIEW TrustBalanceHistoric
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
       f.InYearBalanceCS
  FROM Trust t
  LEFT JOIN TrustFinancial f
    ON t.CompanyNumber = f.CompanyNumber
 CROSS JOIN (SELECT Value FROM Parameters p WHERE p.Name = 'LatestAARYear') y
 WHERE f.RunType = 'default'
   AND f.RunId BETWEEN y.Value - 5 AND y.Value

GO


ALTER VIEW TrustBalance
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
       InYearBalanceCS
 FROM Trust t
 LEFT JOIN TrustFinancial f
   ON t.CompanyNumber = f.CompanyNumber
WHERE f.RunType = 'default'
  AND f.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')

GO
