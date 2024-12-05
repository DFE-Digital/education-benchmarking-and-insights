DROP VIEW IF EXISTS SchoolExpenditureAvgPerUnitComparatorSet
GO

CREATE VIEW SchoolExpenditureAvgPerUnitComparatorSet AS
  WITH pupilComparator AS (
    SELECT RunId
         , URN
         , Comparator.value AS PupilComparatorURN
      FROM ComparatorSet
     CROSS APPLY Openjson(Pupil) Comparator
     WHERE RunType = 'default'
  ), buildingComparator AS (
    SELECT RunId
         , URN
         , Comparator.value AS BuildingComparatorURN
      FROM ComparatorSet
     CROSS APPLY Openjson(Building) Comparator
     WHERE RunType = 'default'
  ), pupilExpenditureAvgPerMetricComparatorSet AS (
    SELECT SchoolExpenditurePerUnitHistoric.URN
         , SchoolExpenditurePerUnitHistoric.RunId
         , Avg(TotalExpenditure) AS AvgTotalExpenditurePerPupil
      FROM SchoolExpenditurePerUnitHistoric
     INNER
      JOIN pupilComparator
        ON (
                 SchoolExpenditurePerUnitHistoric.URN = pupilComparator.URN
             AND pupilComparator.RunId = SchoolExpenditurePerUnitHistoric.RunId
           )
     GROUP
        BY SchoolExpenditurePerUnitHistoric.URN
         , SchoolExpenditurePerUnitHistoric.RunId
   ), buildingExpenditureAvgPerMetricComparatorSet AS (
    SELECT SchoolExpenditurePerUnitHistoric.URN
         , SchoolExpenditurePerUnitHistoric.RunId
         , Avg(TotalPremisesStaffServiceCosts) AS AvgTotalPremisesStaffServiceCostsPerArea
      FROM SchoolExpenditurePerUnitHistoric
     INNER
      JOIN buildingComparator
        ON (
                 SchoolExpenditurePerUnitHistoric.URN = buildingComparator.URN
             AND buildingComparator.RunId = SchoolExpenditurePerUnitHistoric.RunId
           )
     GROUP
        BY SchoolExpenditurePerUnitHistoric.URN
         , SchoolExpenditurePerUnitHistoric.RunId
    )
  SELECT pupilExpenditureAvgPerMetricComparatorSet.URN
       , pupilExpenditureAvgPerMetricComparatorSet.RunId
       , AvgTotalExpenditurePerPupil              AS TotalExpenditure
       , AvgTotalPremisesStaffServiceCostsPerArea AS TotalPremisesStaffServiceCosts
    FROM pupilExpenditureAvgPerMetricComparatorSet
    FULL
    JOIN buildingExpenditureAvgPerMetricComparatorSet
      ON (
               pupilExpenditureAvgPerMetricComparatorSet.URN = buildingExpenditureAvgPerMetricComparatorSet.URN
           AND pupilExpenditureAvgPerMetricComparatorSet.RunId = buildingExpenditureAvgPerMetricComparatorSet.RunId
         )
GO
