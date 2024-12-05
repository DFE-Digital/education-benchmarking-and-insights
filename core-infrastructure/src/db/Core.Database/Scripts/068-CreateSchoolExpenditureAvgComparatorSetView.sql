DROP VIEW IF EXISTS SchoolExpenditureAvgComparatorSet
GO

CREATE VIEW SchoolExpenditureAvgComparatorSet AS
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
  ), pupilExpenditureAvgComparatorSet AS (
    SELECT SchoolExpenditureHistoricWithNulls.URN
         , SchoolExpenditureHistoricWithNulls.RunId
         , Avg(TotalExpenditure) AS AvgTotalExpenditure
      FROM SchoolExpenditureHistoricWithNulls
     INNER
      JOIN pupilComparator
        ON (
                 SchoolExpenditureHistoricWithNulls.URN = pupilComparator.URN
             AND pupilComparator.RunId = SchoolExpenditureHistoricWithNulls.RunId
           )
     GROUP
        BY SchoolExpenditureHistoricWithNulls.URN
         , SchoolExpenditureHistoricWithNulls.RunId
   ), buildingExpenditureAvgComparatorSet AS (
    SELECT SchoolExpenditureHistoricWithNulls.URN
         , SchoolExpenditureHistoricWithNulls.RunId
         , Avg(TotalPremisesStaffServiceCosts) AS AvgTotalPremisesStaffServiceCosts
      FROM SchoolExpenditureHistoricWithNulls
     INNER
      JOIN buildingComparator
        ON (
                 SchoolExpenditureHistoricWithNulls.URN = buildingComparator.URN
             AND buildingComparator.RunId = SchoolExpenditureHistoricWithNulls.RunId
           )
     GROUP
        BY SchoolExpenditureHistoricWithNulls.URN
         , SchoolExpenditureHistoricWithNulls.RunId
    )
  SELECT pupilExpenditureAvgComparatorSet.URN
       , pupilExpenditureAvgComparatorSet.RunId AS Year
       , AvgTotalExpenditure                    AS TotalExpenditure
       , AvgTotalPremisesStaffServiceCosts      AS TotalPremisesStaffServiceCosts
    FROM pupilExpenditureAvgComparatorSet
    FULL
    JOIN buildingExpenditureAvgComparatorSet
      ON (
               pupilExpenditureAvgComparatorSet.URN = buildingExpenditureAvgComparatorSet.URN
           AND pupilExpenditureAvgComparatorSet.RunId = buildingExpenditureAvgComparatorSet.RunId
         )     
GO
