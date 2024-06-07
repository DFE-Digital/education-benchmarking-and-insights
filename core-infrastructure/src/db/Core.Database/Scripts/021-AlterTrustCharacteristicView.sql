IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'TrustCharacteristic')
    BEGIN
        DROP VIEW TrustCharacteristic
    END
GO

CREATE VIEW TrustCharacteristic
AS
SELECT t.CompanyNumber,
       t.TrustName,
       tnf.TotalPupils,
       st.SchoolsInTrust,
       t.OpenDate,
       tnf.PercentFreeSchoolMeals,
       tnf.PercentSpecialEducationNeeds,
       tnf.TotalInternalFloorArea
FROM Trust t
         INNER JOIN SchoolsInTrust st ON st.CompanyNumber = t.CompanyNumber
         INNER JOIN (SELECT TrustCompanyNumber,
                            SUM(TotalPupils)                  'TotalPupils',
                            AVG(PercentFreeSchoolMeals)       'PercentFreeSchoolMeals',
                            AVG(PercentSpecialEducationNeeds) 'PercentSpecialEducationNeeds',
                            SUM(TotalInternalFloorArea)       'TotalInternalFloorArea'
                     FROM CurrentDefaultNonFinancial nf
                              INNER JOIN School s ON s.URN = nf.URN
                     WHERE s.TrustCompanyNumber is not null
                     GROUP BY TrustCompanyNumber) tnf ON tnf.TrustCompanyNumber = t.CompanyNumber

GO