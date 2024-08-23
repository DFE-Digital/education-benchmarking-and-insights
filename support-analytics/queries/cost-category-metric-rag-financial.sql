DECLARE @School AS NVARCHAR(6)='134224'
DECLARE @Year AS   NVARCHAR(4)='2023'

--MATS : 142636, 144562, 144903, 134003, 134224, 134993, 135317, 135744, 136204
--SATS : 137011, 137498, 142894, 144766, 141004, 142604, 143430, 142887, 142903
--Maintained : 109610, 109451

SELECT *
FROM (SELECT Runid,
             URN,
             'CSOnly'                                                                                           'SetType',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalTeachingSupportStaffCostsCS, 0) / TotalPupils))            'Teaching and Teaching support staff',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalNonEducationalSupportStaffCostsCS, 0) / TotalPupils))      'Non-educational support staff and services',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalEducationalSuppliesCostsCS, 0) / TotalPupils))             'Educational supplies',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(LearningResourcesIctCostsCS, 0) / TotalPupils))                 'Educational ICT',
             IIF(ISNULL(TotalInternalFloorArea, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalPremisesStaffServiceCostsCS, 0) / TotalInternalFloorArea)) 'Premises staff and services',
             IIF(ISNULL(TotalInternalFloorArea, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalUtilitiesCostsCS, 0) / TotalInternalFloorArea))            'Utilities',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(AdministrativeSuppliesNonEducationalCostsCS, 0) / TotalPupils)) 'Administrative supplies',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalGrossCateringCostsCS, 0) / TotalPupils))                   'Catering staff and supplies',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalOtherCostsCS, 0) / TotalPupils))                           'Other costs'
      FROM Financial
      WHERE RunType = 'default'
        AND URN = @School
        AND RunId = @Year
      UNION ALL
      SELECT Runid,
             URN,
             'financial'                                                                                      'SetType',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalTeachingSupportStaffCosts, 0) / TotalPupils))            'Teaching and Teaching support staff',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalNonEducationalSupportStaffCosts, 0) / TotalPupils))      'Non-educational support staff and services',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalEducationalSuppliesCosts, 0) / TotalPupils))             'Educational supplies',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(LearningResourcesIctCosts, 0) / TotalPupils))                 'Educational ICT',
             IIF(ISNULL(TotalInternalFloorArea, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalPremisesStaffServiceCosts, 0) / TotalInternalFloorArea)) 'Premises staff and services',
             IIF(ISNULL(TotalInternalFloorArea, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalUtilitiesCosts, 0) / TotalInternalFloorArea))            'Utilities',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(AdministrativeSuppliesNonEducationalCosts, 0) / TotalPupils)) 'Administrative supplies',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalGrossCateringCosts, 0) / TotalPupils))                   'Catering staff and supplies',
             IIF(ISNULL(TotalPupils, 0) = 0, 0,
                 CONVERT(decimal(16, 2), ISNULL(TotalOtherCosts, 0) / TotalPupils))                           'Other costs'
      FROM Financial
      WHERE RunType = 'default'
        AND URN = @School
        AND RunId = @Year
      UNION ALL
      SELECT Runid,
             URN,
             SetType,
             [Teaching and Teaching support staff],
             [Non-educational support staff and services],
             [Educational supplies],
             [Educational ICT],
             [Premises staff and services],
             [Utilities],
             [Administrative supplies],
             [Catering staff and supplies],
             [Other costs]
      FROM (SELECT Runid,
                   SetType,
                   URN,
                   Value,
                   Category
            FROM MetricRAG
            WHERE RunType = 'default'
              AND SubCategory = 'Total'
              AND URN = @School
              AND RunId = @Year) x
               PIVOT (MAX(Value) FOR Category IN ([Administrative supplies], [Catering staff and supplies],[Educational ICT],[Educational supplies],[Non-educational support staff and services],[Other costs],[Premises staff and services],[Teaching and Teaching support staff],[Utilities])) rag) p
ORDER BY URN, SetType