DROP VIEW IF EXISTS SchoolExpenditureAvgPercentageOfIncomeHistoric
GO

CREATE VIEW SchoolExpenditureAvgPercentageOfIncomeHistoric AS
  SELECT RunId
       , FinanceType
       , OverallPhase
       , Avg(TotalExpenditure)               AS TotalExpenditure
       , Avg(TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditurePercentageOfIncomeHistoric
   GROUP
      BY RunId
       , FinanceType
       , OverallPhase
GO
