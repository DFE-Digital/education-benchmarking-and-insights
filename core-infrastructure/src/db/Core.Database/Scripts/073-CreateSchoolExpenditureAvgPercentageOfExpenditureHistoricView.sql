DROP VIEW IF EXISTS SchoolExpenditureAvgPercentageOfExpenditureHistoric
GO

CREATE VIEW SchoolExpenditureAvgPercentageOfExpenditureHistoric AS
  SELECT Year
       , FinanceType
       , OverallPhase
       , Avg(TotalExpenditure)               AS TotalExpenditure
       , Avg(TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditurePercentageOfExpenditureHistoric
   GROUP
      BY RunId
       , FinanceType
       , OverallPhase
GO
