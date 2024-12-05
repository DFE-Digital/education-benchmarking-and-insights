DROP VIEW IF EXISTS SchoolExpenditureAvgHistoric
GO

CREATE VIEW SchoolExpenditureAvgHistoric AS
  SELECT RunId
       , FinanceType
       , OverallPhase
       , Avg(TotalExpenditure)               AS TotalExpenditure
       , Avg(TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditureHistoricWithNulls
   INNER
    JOIN School
      ON (School.URN = SchoolExpenditureHistoricWithNulls.URN)
   GROUP
      BY RunId
       , FinanceType
       , OverallPhase
GO
