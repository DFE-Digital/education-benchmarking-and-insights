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
               CONVERT(float, nf.PercentWithASD)               'PercentWithASD'
        FROM School s
                 LEFT JOIN SchoolsInTrust st on st.CompanyNumber = s.TrustCompanyNumber
                 LEFT JOIN CurrentDefaultNonFinancial nf on nf.URN = s.URN

GO