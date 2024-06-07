ALTER VIEW SchoolPhasesCoveredByTrust AS
    SELECT CompanyNumber,
           CONCAT('[', STRING_AGG(CONCAT('"', OverallPhase, '"'), ','), ']') 'PhasesCovered'
    FROM (SELECT DISTINCT TrustCompanyNumber 'CompanyNumber', OverallPhase
          FROM School
          WHERE TrustCompanyNumber IS NOT NULL) t
    GROUP BY t.CompanyNumber

GO


ALTER VIEW TrustCharacteristic AS
    SELECT t.CompanyNumber,
           t.TrustName,
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
GO
