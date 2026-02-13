DROP VIEW IF EXISTS VW_IncomeSchoolDefaultComparatorAveActual
GO

CREATE VIEW VW_IncomeSchoolDefaultComparatorAveActual AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
    CROSS APPLY Openjson(Pupil) Comparator
    WHERE RunType = 'default'
)
SELECT s.URN
     , s.RunId
     , c.OverallPhase
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
FROM comparators s
         INNER
             JOIN VW_IncomeSchoolDefaultNormalisedActual c
                  ON (
                      s.ComparatorURN = c.URN
                          AND s.RunId = c.RunId
                      )
GROUP
    BY s.URN
     , s.RunId
     , c.OverallPhase
GO

DROP VIEW IF EXISTS VW_IncomeSchoolDefaultComparatorAvePercentExpenditure
GO

CREATE VIEW VW_IncomeSchoolDefaultComparatorAvePercentExpenditure AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
        CROSS APPLY Openjson(Pupil) Comparator
        WHERE RunType = 'default'
        )
SELECT s.URN
     , s.RunId
     , c.OverallPhase
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
FROM comparators s
         INNER
             JOIN VW_IncomeSchoolDefaultNormalisedPercentExpenditure c
                  ON (
                      s.ComparatorURN = c.URN
                          AND s.RunId = c.RunId
                      )
GROUP
    BY s.URN
     , s.RunId
     , c.OverallPhase
GO

DROP VIEW IF EXISTS VW_IncomeSchoolDefaultComparatorAvePercentIncome
GO

CREATE VIEW VW_IncomeSchoolDefaultComparatorAvePercentIncome AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
        CROSS APPLY Openjson(Pupil) Comparator
        WHERE RunType = 'default'
        )
SELECT s.URN
     , s.RunId
     , c.OverallPhase
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
FROM comparators s
         INNER
             JOIN VW_IncomeSchoolDefaultNormalisedPercentIncome c
                  ON (
                      s.ComparatorURN = c.URN
                          AND s.RunId = c.RunId
                      )
GROUP
    BY s.URN
     , s.RunId
     , c.OverallPhase
GO

DROP VIEW IF EXISTS VW_IncomeSchoolDefaultComparatorAvePerUnit
GO

CREATE VIEW VW_IncomeSchoolDefaultComparatorAvePerUnit AS
WITH comparators AS (
    SELECT RunId
         , URN
         , Comparator.value AS ComparatorURN
    FROM ComparatorSet
        CROSS APPLY Openjson(Pupil) Comparator
        WHERE RunType = 'default'
        )
SELECT s.URN
     , s.RunId
     , c.OverallPhase
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
FROM comparators s
         INNER
             JOIN VW_IncomeSchoolDefaultNormalisedPerUnit c
                  ON (
                      s.ComparatorURN = c.URN
                          AND s.RunId = c.RunId
                      )
GROUP
    BY s.URN
     , s.RunId
     , c.OverallPhase
GO
