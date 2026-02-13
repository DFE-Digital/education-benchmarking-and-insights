DROP VIEW IF EXISTS VW_IncomeSchoolDefaultNationalAveActual
    GO

CREATE VIEW VW_IncomeSchoolDefaultNationalAveActual AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg (TotalIncome)                          AS 'TotalIncome'
     , Avg (TotalGrantFunding)                    AS 'TotalGrantFunding'
     , Avg (TotalSelfGeneratedFunding)            AS 'TotalSelfGeneratedFunding'
     , Avg (DirectRevenueFinancing)               AS 'DirectRevenueFinancing'
     , Avg (DirectGrants)                         AS 'DirectGrants'
     , Avg (PrePost16Funding)                     AS 'PrePost16Funding'
     , Avg (OtherDfeGrants)                       AS 'OtherDfeGrants'
     , Avg (OtherIncomeGrants)                    AS 'OtherIncomeGrants'
     , Avg (GovernmentSource)                     AS 'GovernmentSource'
     , Avg (CommunityGrants)                      AS 'CommunityGrants'
     , Avg (Academies)                            AS 'Academies'
     , Avg (IncomeFacilitiesServices)             AS 'IncomeFacilitiesServices'
     , Avg (IncomeCateringServices)               AS 'IncomeCateringServices'
     , Avg (DonationsVoluntaryFunds)              AS 'DonationsVoluntaryFunds'
     , Avg (ReceiptsSupplyTeacherInsuranceClaims) AS 'ReceiptsSupplyTeacherInsuranceClaims'
     , Avg (InvestmentIncome)                     AS 'InvestmentIncome'
     , Avg (OtherSelfGeneratedIncome)             AS 'OtherSelfGeneratedIncome'
FROM VW_IncomeSchoolDefaultNormalisedActual
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO

DROP VIEW IF EXISTS VW_IncomeSchoolDefaultNationalAvePercentExpenditure
    GO

CREATE VIEW VW_IncomeSchoolDefaultNationalAvePercentExpenditure AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg (TotalIncome)                          AS 'TotalIncome'
     , Avg (TotalGrantFunding)                    AS 'TotalGrantFunding'
     , Avg (TotalSelfGeneratedFunding)            AS 'TotalSelfGeneratedFunding'
     , Avg (DirectRevenueFinancing)               AS 'DirectRevenueFinancing'
     , Avg (DirectGrants)                         AS 'DirectGrants'
     , Avg (PrePost16Funding)                     AS 'PrePost16Funding'
     , Avg (OtherDfeGrants)                       AS 'OtherDfeGrants'
     , Avg (OtherIncomeGrants)                    AS 'OtherIncomeGrants'
     , Avg (GovernmentSource)                     AS 'GovernmentSource'
     , Avg (CommunityGrants)                      AS 'CommunityGrants'
     , Avg (Academies)                            AS 'Academies'
     , Avg (IncomeFacilitiesServices)             AS 'IncomeFacilitiesServices'
     , Avg (IncomeCateringServices)               AS 'IncomeCateringServices'
     , Avg (DonationsVoluntaryFunds)              AS 'DonationsVoluntaryFunds'
     , Avg (ReceiptsSupplyTeacherInsuranceClaims) AS 'ReceiptsSupplyTeacherInsuranceClaims'
     , Avg (InvestmentIncome)                     AS 'InvestmentIncome'
     , Avg (OtherSelfGeneratedIncome)             AS 'OtherSelfGeneratedIncome'
FROM VW_IncomeSchoolDefaultNormalisedPercentExpenditure
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO

DROP VIEW IF EXISTS VW_IncomeSchoolDefaultNationalAvePercentIncome
    GO

CREATE VIEW VW_IncomeSchoolDefaultNationalAvePercentIncome AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg (TotalIncome)                          AS 'TotalIncome'
     , Avg (TotalGrantFunding)                    AS 'TotalGrantFunding'
     , Avg (TotalSelfGeneratedFunding)            AS 'TotalSelfGeneratedFunding'
     , Avg (DirectRevenueFinancing)               AS 'DirectRevenueFinancing'
     , Avg (DirectGrants)                         AS 'DirectGrants'
     , Avg (PrePost16Funding)                     AS 'PrePost16Funding'
     , Avg (OtherDfeGrants)                       AS 'OtherDfeGrants'
     , Avg (OtherIncomeGrants)                    AS 'OtherIncomeGrants'
     , Avg (GovernmentSource)                     AS 'GovernmentSource'
     , Avg (CommunityGrants)                      AS 'CommunityGrants'
     , Avg (Academies)                            AS 'Academies'
     , Avg (IncomeFacilitiesServices)             AS 'IncomeFacilitiesServices'
     , Avg (IncomeCateringServices)               AS 'IncomeCateringServices'
     , Avg (DonationsVoluntaryFunds)              AS 'DonationsVoluntaryFunds'
     , Avg (ReceiptsSupplyTeacherInsuranceClaims) AS 'ReceiptsSupplyTeacherInsuranceClaims'
     , Avg (InvestmentIncome)                     AS 'InvestmentIncome'
     , Avg (OtherSelfGeneratedIncome)             AS 'OtherSelfGeneratedIncome'
FROM VW_IncomeSchoolDefaultNormalisedPercentIncome
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO

DROP VIEW IF EXISTS VW_IncomeSchoolDefaultNationalAvePerUnit
    GO

CREATE VIEW VW_IncomeSchoolDefaultNationalAvePerUnit AS
SELECT RunId
     , OverallPhase
     , FinanceType
     , Avg (TotalIncome)                          AS 'TotalIncome'
     , Avg (TotalGrantFunding)                    AS 'TotalGrantFunding'
     , Avg (TotalSelfGeneratedFunding)            AS 'TotalSelfGeneratedFunding'
     , Avg (DirectRevenueFinancing)               AS 'DirectRevenueFinancing'
     , Avg (DirectGrants)                         AS 'DirectGrants'
     , Avg (PrePost16Funding)                     AS 'PrePost16Funding'
     , Avg (OtherDfeGrants)                       AS 'OtherDfeGrants'
     , Avg (OtherIncomeGrants)                    AS 'OtherIncomeGrants'
     , Avg (GovernmentSource)                     AS 'GovernmentSource'
     , Avg (CommunityGrants)                      AS 'CommunityGrants'
     , Avg (Academies)                            AS 'Academies'
     , Avg (IncomeFacilitiesServices)             AS 'IncomeFacilitiesServices'
     , Avg (IncomeCateringServices)               AS 'IncomeCateringServices'
     , Avg (DonationsVoluntaryFunds)              AS 'DonationsVoluntaryFunds'
     , Avg (ReceiptsSupplyTeacherInsuranceClaims) AS 'ReceiptsSupplyTeacherInsuranceClaims'
     , Avg (InvestmentIncome)                     AS 'InvestmentIncome'
     , Avg (OtherSelfGeneratedIncome)             AS 'OtherSelfGeneratedIncome'
FROM VW_IncomeSchoolDefaultNormalisedPerUnit
GROUP
    BY RunId
     , FinanceType
     , OverallPhase
GO