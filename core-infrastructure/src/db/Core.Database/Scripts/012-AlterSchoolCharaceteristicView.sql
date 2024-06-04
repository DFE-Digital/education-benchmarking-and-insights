ALTER VIEW SchoolCharacteristic
    AS
        SELECT s.URN,
               s.SchoolName,
               s.AddressTown,
               s.AddressPostcode,
               s.FinanceType,
               s.OverallPhase,
               s.LAName,
               nf.TotalPupils,
               nf.PercentFreeSchoolMeals,
               nf.PercentSpecialEducationNeeds,
               s.LondonWeighting,
               nf.BuildingAverageAge,
               nf.TotalInternalFloorArea,
               s.OfstedDescription,
               st.SchoolsInTrust,
               s.IsPFISchool,
               nf.TotalPupilsSixthForm,
               nf.KS2Progress,
               nf.KS4Progress,
               nf.PercentWithVI,
               nf.PercentWithSPLD,
               nf.PercentWithSLD ,
               nf.PercentWithSLCN,
               nf.PercentWithSEMH,
               nf.PercentWithPMLD,
               nf.PercentWithPD,
               nf.PercentWithOTH,
               nf.PercentWithMSI,
               nf.PercentWithMLD,
               nf.PercentWithHI,
               nf.PercentWithASD
        FROM School s
                 LEFT JOIN SchoolsInTrust st on st.CompanyNumber = s.TrustCompanyNumber
                 LEFT JOIN CurrentDefaultNonFinancial nf on nf.URN = s.URN

GO