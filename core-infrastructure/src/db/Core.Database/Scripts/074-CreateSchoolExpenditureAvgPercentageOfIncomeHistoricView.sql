DROP VIEW IF EXISTS SchoolExpenditureAvgPercentageOfIncomeHistoric
GO

CREATE VIEW SchoolExpenditureAvgPercentageOfIncomeHistoric AS
  SELECT Year
       , FinanceType
       , OverallPhase
       , Avg(TotalExpenditure)               AS TotalExpenditure
       , Avg(TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditurePercentageOfIncomeHistoric
   GROUP
      BY Year
       , FinanceType
       , OverallPhase
GO
