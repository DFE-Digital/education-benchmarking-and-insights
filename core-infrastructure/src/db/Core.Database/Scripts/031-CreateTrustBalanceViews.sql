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
       f.Year,
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
         LEFT JOIN     (SELECT TrustCompanyNumber 'CompanyNumber',
                               Year,
                               SUM(TotalPupils) 'TotalPupils',
                               SUM(TotalIncome) 'TotalIncome',
                               SUM(TotalExpenditure) 'TotalExpenditure',
                               SUM(InYearBalance) 'InYearBalance',
                               SUM(RevenueReserve) 'RevenueReserve',
                               SUM(TotalIncomeCS) 'TotalIncomeCS',
                               SUM(TotalExpenditureCS) 'TotalExpenditureCS',
                               SUM(InYearBalanceCS) 'InYearBalanceCS',
                               SUM(RevenueReserveCS) 'RevenueReserveCS'
                        FROM SchoolBalanceHistoric
                        WHERE TrustCompanyNumber IS NOT NULL
                        GROUP BY TrustCompanyNumber, Year) f ON f.CompanyNumber = t.CompanyNumber

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
         LEFT JOIN     (SELECT TrustCompanyNumber 'CompanyNumber',
                               SUM(TotalPupils) 'TotalPupils',
                               SUM(TotalIncome) 'TotalIncome',
                               SUM(TotalExpenditure) 'TotalExpenditure',
                               SUM(InYearBalance) 'InYearBalance',
                               SUM(RevenueReserve) 'RevenueReserve',
                               SUM(TotalIncomeCS) 'TotalIncomeCS',
                               SUM(TotalExpenditureCS) 'TotalExpenditureCS',
                               SUM(InYearBalanceCS) 'InYearBalanceCS',
                               SUM(RevenueReserveCS) 'RevenueReserveCS'
                        FROM SchoolBalance
                        WHERE TrustCompanyNumber IS NOT NULL
                        GROUP BY TrustCompanyNumber) f ON f.CompanyNumber = t.CompanyNumber

GO