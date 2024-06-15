ALTER VIEW TrustCharacteristic AS
    SELECT t.CompanyNumber,
           t.TrustName,
           tf.TotalIncome,
           tnf.TotalPupils,
           st.SchoolsInTrust,
           t.OpenDate,
           tnf.PercentFreeSchoolMeals,
           tnf.PercentSpecialEducationNeeds,
           tnf.TotalInternalFloorArea,
           sp.PhasesCovered
    FROM Trust t
             INNER JOIN SchoolsInTrust st ON st.CompanyNumber = t.CompanyNumber
             INNER JOIN SchoolPhasesCoveredByTrust sp ON sp.CompanyNumber = t.CompanyNumber
             INNER JOIN (SELECT TrustCompanyNumber,
                                CONVERT(float, SUM(TotalPupils))                  'TotalPupils',
                                CONVERT(float, AVG(PercentFreeSchoolMeals))       'PercentFreeSchoolMeals',
                                CONVERT(float, AVG(PercentSpecialEducationNeeds)) 'PercentSpecialEducationNeeds',
                                CONVERT(float, SUM(TotalInternalFloorArea))       'TotalInternalFloorArea'
                         FROM CurrentDefaultNonFinancial nf
                                  INNER JOIN School s ON s.URN = nf.URN
                         WHERE s.TrustCompanyNumber is not null
                         GROUP BY TrustCompanyNumber) tnf ON tnf.TrustCompanyNumber = t.CompanyNumber
             INNER JOIN (SELECT TrustCompanyNumber,
                                CONVERT(float, SUM(TotalIncome + TotalIncomeCS)) 'TotalIncome'
                         FROM CurrentDefaultFinancial f
                                  INNER JOIN School s ON s.URN = f.URN
                         WHERE s.TrustCompanyNumber is not null
                         GROUP BY TrustCompanyNumber) tf ON tf.TrustCompanyNumber = t.CompanyNumber

GO

ALTER VIEW SchoolCharacteristic
    AS
        SELECT s.URN,
               s.SchoolName,
               s.AddressTown,
               s.AddressPostcode,
               s.FinanceType,
               s.OverallPhase,
               s.LAName,
               CONVERT(float, nf.TotalPupils)                  'TotalPupils',
               CONVERT(float, nf.PercentFreeSchoolMeals)       'PercentFreeSchoolMeals',
               CONVERT(float, nf.PercentSpecialEducationNeeds) 'PercentSpecialEducationNeeds',
               s.LondonWeighting,
               CONVERT(float, nf.BuildingAverageAge)           'BuildingAverageAge',
               CONVERT(float, nf.TotalInternalFloorArea)       'TotalInternalFloorArea',
               s.OfstedDescription,
               st.SchoolsInTrust,
               s.IsPFISchool,
               CONVERT(float, nf.TotalPupilsSixthForm)         'TotalPupilsSixthForm',
               CONVERT(float, nf.KS2Progress)                  'KS2Progress',
               CONVERT(float, nf.KS4Progress)                  'KS4Progress',
               CONVERT(float, nf.PercentWithVI)                'PercentWithVI',
               CONVERT(float, nf.PercentWithSPLD)              'PercentWithSPLD',
               CONVERT(float, nf.PercentWithSLD)               'PercentWithSLD',
               CONVERT(float, nf.PercentWithSLCN)              'PercentWithSLCN',
               CONVERT(float, nf.PercentWithSEMH)              'PercentWithSEMH',
               CONVERT(float, nf.PercentWithPMLD)              'PercentWithPMLD',
               CONVERT(float, nf.PercentWithPD)                'PercentWithPD',
               CONVERT(float, nf.PercentWithOTH)               'PercentWithOTH',
               CONVERT(float, nf.PercentWithMSI)               'PercentWithMSI',
               CONVERT(float, nf.PercentWithMLD)               'PercentWithMLD',
               CONVERT(float, nf.PercentWithHI)                'PercentWithHI',
               CONVERT(float, nf.PercentWithASD)               'PercentWithASD',
               f.FinancialPosition 'SchoolPosition'
        FROM School s
                 LEFT JOIN SchoolsInTrust st on st.CompanyNumber = s.TrustCompanyNumber
                 LEFT JOIN CurrentDefaultNonFinancial nf on nf.URN = s.URN
                 LEFT JOIN CurrentDefaultFinancial f on f.URN = s.URN
GO