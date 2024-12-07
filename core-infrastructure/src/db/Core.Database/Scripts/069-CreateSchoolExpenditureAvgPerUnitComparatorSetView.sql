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
    SELECT pupilComparator.URN
         , pupilComparator.RunId
         , Avg(TotalExpenditure) AS AvgTotalExpenditurePerPupil
      FROM pupilComparator
     INNER
      JOIN SchoolExpenditurePerUnitHistoric
        ON (
                 pupilComparator.PupilComparatorURN = SchoolExpenditurePerUnitHistoric.URN
             AND pupilComparator.RunId = SchoolExpenditurePerUnitHistoric.RunId
           )
     GROUP
        BY pupilComparator.URN
         , pupilComparator.RunId
   ), buildingExpenditureAvgPerMetricComparatorSet AS (
    SELECT buildingComparator.URN
         , buildingComparator.RunId
         , Avg(TotalPremisesStaffServiceCosts) AS AvgTotalPremisesStaffServiceCostsPerArea
      FROM buildingComparator
     INNER
      JOIN SchoolExpenditurePerUnitHistoric
        ON (
                 buildingComparator.BuildingComparatorURN = SchoolExpenditurePerUnitHistoric.URN
             AND buildingComparator.RunId = SchoolExpenditurePerUnitHistoric.RunId
           )
     GROUP
        BY buildingComparator.URN
         , buildingComparator.RunId
    )
  SELECT pupilExpenditureAvgPerMetricComparatorSet.URN
       , pupilExpenditureAvgPerMetricComparatorSet.RunId AS Year
       , AvgTotalExpenditurePerPupil                     AS TotalExpenditure
       , AvgTotalPremisesStaffServiceCostsPerArea        AS TotalPremisesStaffServiceCosts
    FROM pupilExpenditureAvgPerMetricComparatorSet
    FULL
    JOIN buildingExpenditureAvgPerMetricComparatorSet
      ON (
               pupilExpenditureAvgPerMetricComparatorSet.URN = buildingExpenditureAvgPerMetricComparatorSet.URN
           AND pupilExpenditureAvgPerMetricComparatorSet.RunId = buildingExpenditureAvgPerMetricComparatorSet.RunId
         )
GO
