ALTER VIEW SchoolBalanceHistoric AS
SELECT s.URN,
       s.Year,
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.InYearBalance,
       f.RevenueReserve,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.InYearBalanceCS
FROM (SELECT *
      FROM School,
           AARHistoryYears) s
         LEFT OUTER JOIN Financial f ON s.Year = f.RunId AND f.URN = s.URN AND f.RunType = 'default'
WHERE s.FinanceType = 'Academy'
UNION
SELECT s.URN,
       s.Year,
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.InYearBalance,
       f.RevenueReserve,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.InYearBalanceCS
FROM (SELECT *
      FROM School,
           CFRHistoryYears) s
         LEFT OUTER JOIN Financial f ON s.Year = f.RunId AND f.URN = s.URN AND f.RunType = 'default'
WHERE s.FinanceType = 'Maintained'

GO


ALTER VIEW SchoolBalance AS
    SELECT s.URN,
           s.SchoolName,
           s.SchoolType,
           s.LAName,
           s.TrustCompanyNumber,
           f.PeriodCoveredByReturn,
           f.TotalPupils,
           f.TotalIncome,
           f.TotalExpenditure,
           f.InYearBalance,
           f.RevenueReserve,
           f.TotalIncomeCS,
           f.TotalExpenditureCS,
           f.InYearBalanceCS
    FROM School s
             LEFT JOIN CurrentDefaultFinancial f on f.URN = s.URN
GO
