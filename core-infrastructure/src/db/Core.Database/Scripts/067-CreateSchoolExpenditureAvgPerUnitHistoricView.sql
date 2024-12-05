DROP VIEW IF EXISTS SchoolExpenditurePerUnitHistoric
GO

CREATE VIEW SchoolExpenditurePerUnitHistoric AS
  SELECT SchoolExpenditureHistoricWithNulls.URN
       , RunId
       , FinanceType
       , OverallPhase
       , TotalExpenditure / TotalPupils                          AS TotalExpenditure
       , TotalPremisesStaffServiceCosts / TotalInternalFloorArea AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditureHistoricWithNulls
   INNER
    JOIN School
      ON (School.URN = SchoolExpenditureHistoricWithNulls.URN)
GO

DROP VIEW IF EXISTS SchoolExpenditureAvgPerUnitHistoric
GO

CREATE VIEW SchoolExpenditureAvgPerUnitHistoric AS
  SELECT RunId                               AS Year
       , FinanceType
       , OverallPhase
       , Avg(TotalExpenditure)               AS TotalExpenditure
       , Avg(TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
    FROM SchoolExpenditurePerUnitHistoric
   GROUP
      BY RunId
       , FinanceType
       , OverallPhase
GO
