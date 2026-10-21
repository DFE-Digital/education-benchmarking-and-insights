ALTER VIEW dbo.SchoolCharacteristic
AS
SELECT s.URN,
       s.SchoolName,
       s.AddressTown,
       s.AddressPostcode,
       s.FinanceType,
       s.OverallPhase,
       s.LAName,
       f.PeriodCoveredByReturn,
       CONVERT(float, nf.TotalPupils)                    AS [TotalPupils],
       CONVERT(float, nf.PercentFreeSchoolMeals)         AS [PercentFreeSchoolMeals],
       CONVERT(float, nf.PercentSpecialEducationNeeds)   AS [PercentSpecialEducationNeeds],
       s.LondonWeighting,
       CONVERT(float, nf.BuildingAverageAge)             AS [BuildingAverageAge],
       CONVERT(float, nf.TotalInternalFloorArea)         AS [TotalInternalFloorArea],
       s.OfstedDescription,
       st.SchoolsInTrust,
       s.IsPFISchool,
       CONVERT(float, nf.TotalPupilsSixthForm)           AS [TotalPupilsSixthForm],
       CONVERT(float, nf.KS2Progress)                    AS [KS2Progress],
       CONVERT(float, nf.KS4Progress)                    AS [KS4Progress],
       nf.KS4ProgressBanding,
       CONVERT(float, nf.PercentWithVI)                  AS [PercentWithVI],
       CONVERT(float, nf.PercentWithSPLD)                AS [PercentWithSPLD],
       CONVERT(float, nf.PercentWithSLD)                 AS [PercentWithSLD],
       CONVERT(float, nf.PercentWithSLCN)                AS [PercentWithSLCN],
       CONVERT(float, nf.PercentWithSEMH)                AS [PercentWithSEMH],
       CONVERT(float, nf.PercentWithPMLD)                AS [PercentWithPMLD],
       CONVERT(float, nf.PercentWithPD)                  AS [PercentWithPD],
       CONVERT(float, nf.PercentWithOTH)                 AS [PercentWithOTH],
       CONVERT(float, nf.PercentWithMSI)                 AS [PercentWithMSI],
       CONVERT(float, nf.PercentWithMLD)                 AS [PercentWithMLD],
       CONVERT(float, nf.PercentWithHI)                  AS [PercentWithHI],
       CONVERT(float, nf.PercentWithASD)                 AS [PercentWithASD],
       CONVERT(float, nf.Attainment8)                    AS [Attainment8],
       CONVERT(float, lanf.LocalAuthorityAvgAttainment8) AS [LocalAuthorityAvgAttainment8],
       CONVERT(float, lanf.NationalAvgAttainment8)       AS [NationalAvgAttainment8],
       f.FinancialPosition                               AS [SchoolPosition]
FROM dbo.School s
    LEFT JOIN dbo.SchoolsInTrust st ON st.CompanyNumber = s.TrustCompanyNumber
    LEFT JOIN dbo.CurrentDefaultNonFinancial nf ON nf.URN = s.URN
    LEFT JOIN dbo.CurrentDefaultFinancial f ON f.URN = s.URN
    LEFT JOIN dbo.CurrentDefaultLocalAuthorityNonFinancial lanf ON lanf.LaCode = s.LaCode;
GO
