DROP VIEW IF EXISTS VW_FinancialDefaultCurrent
GO

CREATE VIEW VW_FinancialDefaultCurrent AS
SELECT *
FROM Financial
WHERE RunType = 'default'
  AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_TrustFinancialDefault
GO

CREATE VIEW VW_TrustFinancialDefault AS
SELECT RunId,
       CompanyNumber,
       TotalPupils,
       TotalExpenditure,
       TotalExpenditureCS,
       TotalExpenditure - TotalExpenditureCS AS 'TotalExpenditureSchool',
       TotalIncome,
       TotalIncomeCS,
       TotalIncome - TotalExpenditureCS      AS 'TotalIncomeSchool',
       InYearBalance,
       InYearBalanceCS,
       InYearBalance - InYearBalanceCS       AS 'InYearBalanceSchool',
       RevenueReserve,
       TotalGrantFunding,
       TotalSelfGeneratedFunding,
       DirectGrants,
       PrePost16Funding,
       OtherDfeGrants,
       OtherIncomeGrants,
       GovernmentSource,
       CommunityGrants,
       Academies,
       IncomeFacilitiesServices,
       IncomeCateringServices,
       DonationsVoluntaryFunds,
       ReceiptsSupplyTeacherInsuranceClaims,
       InvestmentIncome,
       OtherSelfGeneratedIncome
FROM TrustFinancial
WHERE RunType = 'default'
GO


DROP VIEW IF EXISTS VW_TrustFinancialDefaultCurrent
GO

CREATE VIEW VW_TrustFinancialDefaultCurrent AS
SELECT *
FROM VW_TrustFinancialDefault
WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO