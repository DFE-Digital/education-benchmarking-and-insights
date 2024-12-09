DROP VIEW IF EXISTS SchoolExpenditureAvgHistoric
GO

CREATE VIEW SchoolExpenditureAvgHistoric AS
  SELECT Year
       , FinanceType
       , OverallPhase
       , Avg(TotalExpenditure)               AS TotalExpenditure
       , Avg(TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditureHistoricWithNulls
   GROUP
      BY Year
       , FinanceType
       , OverallPhase
GO
