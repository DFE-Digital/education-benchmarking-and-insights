DECLARE @School AS NVARCHAR(6)='142875'
DECLARE @Year AS NVARCHAR(4)='2023'
DECLARE @SetId As NVARCHAR(50)='e6178d3b-28c8-4ff7-8659-b33894e87d8b'

SELECT *
FROM (
         SELECT Runid,
                URN,
                'financial' 'SetType',
                IIF(ISNULL(TotalPupils,0) = 0, 0, CONVERT(decimal(16,2),(ISNULL(TotalTeachingSupportStaffCosts,0) + ISNULL(TotalTeachingSupportStaffCostsCS,0)) / TotalPupils)) 'Teaching and Teaching support staff',
                IIF(ISNULL(TotalPupils,0) = 0, 0, CONVERT(decimal(16,2),(ISNULL(TotalNonEducationalSupportStaffCosts,0) + ISNULL(TotalNonEducationalSupportStaffCostsCS,0)) / TotalPupils)) 'Non-educational support staff and services',
                IIF(ISNULL(TotalPupils,0) = 0, 0, CONVERT(decimal(16,2),(ISNULL(TotalEducationalSuppliesCosts,0) + ISNULL(TotalEducationalSuppliesCostsCS,0)) / TotalPupils)) 'Educational supplies',
                IIF(ISNULL(TotalPupils,0) = 0, 0, CONVERT(decimal(16,2),(ISNULL(LearningResourcesIctCosts,0) + ISNULL(LearningResourcesIctCostsCS,0)) / TotalPupils)) 'Educational ICT',
                IIF(ISNULL(TotalInternalFloorArea,0) = 0, 0, CONVERT(decimal(16,2),(ISNULL(TotalPremisesStaffServiceCosts,0) + ISNULL(TotalPremisesStaffServiceCostsCS,0)) / TotalInternalFloorArea)) 'Premises staff and services',
                IIF(ISNULL(TotalInternalFloorArea,0) = 0, 0, CONVERT(decimal(16,2),(ISNULL(TotalUtilitiesCosts,0) + ISNULL(TotalUtilitiesCostsCS,0)) / TotalInternalFloorArea)) 'Utilities',
                IIF(ISNULL(TotalPupils,0) = 0, 0, CONVERT(decimal(16,2),(ISNULL(AdministrativeSuppliesNonEducationalCosts,0) + ISNULL(AdministrativeSuppliesNonEducationalCostsCS,0)) / TotalPupils)) 'Administrative supplies',
                IIF(ISNULL(TotalPupils,0) = 0, 0, CONVERT(decimal(16,2),(ISNULL(TotalGrossCateringCosts,0) + ISNULL(TotalGrossCateringCostsCS,0)) / TotalPupils)) 'Catering staff and supplies',
                IIF(ISNULL(TotalPupils,0) = 0, 0, CONVERT(decimal(16,2),(ISNULL(TotalOtherCosts,0) + ISNULL(TotalOtherCostsCS,0)) / TotalPupils)) 'Other costs'
         FROM Financial
         WHERE RunType = 'default' AND URN = @School AND RunId = @Year
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
               WHERE RunType = 'default' AND SubCategory = 'Total' AND URN = @School AND RunId = @SetId) x
                  PIVOT (MAX (Value) FOR Category IN ([Administrative supplies], [Catering staff and supplies],[Educational ICT],[Educational supplies],[Non-educational support staff and services],[Other costs],[Premises staff and services],[Teaching and Teaching support staff],[Utilities])) rag) p
ORDER BY URN, SetType