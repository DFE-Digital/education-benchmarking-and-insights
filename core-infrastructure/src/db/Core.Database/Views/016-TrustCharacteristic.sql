DROP VIEW IF EXISTS VW_TrustCharacteristics
GO

CREATE VIEW VW_TrustCharacteristics AS
WITH currentSchools AS (SELECT URN
                        FROM Financial
                        WHERE RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
                          AND RunType = 'default'),
    schools AS (SELECT TrustCompanyNumber AS 'CompanyNumber', 
                        COUNT(URN) AS 'SchoolsInTrust' 
                 FROM School
                 WHERE TrustCompanyNumber IS NOT NULL
                 AND URN IN (SELECT URN FROM currentSchools)
                 GROUP BY TrustCompanyNumber),
    phasesCount AS (SELECT DISTINCT TrustCompanyNumber AS 'CompanyNumber', 
                                    OverallPhase, 
                                    COUNT(OverallPhase) AS 'Count'
                    FROM School
                    WHERE TrustCompanyNumber IS NOT NULL
                      AND URN IN (SELECT URN FROM currentSchools)
                    GROUP BY TrustCompanyNumber, OverallPhase),
    phases AS (SELECT CompanyNumber,
                      CONCAT( '[', STRING_AGG( CONCAT( '{"phase":"', OverallPhase, '","count":', Count, '}' ), ',' ), ']' ) AS 'PhasesCovered'
               FROM phasesCount
               GROUP BY CompanyNumber),
    census AS (SELECT TrustCompanyNumber AS 'CompanyNumber',
                            CONVERT(float, SUM(TotalPupils)) AS 'TotalPupils',
                            CONVERT(float, AVG(PercentFreeSchoolMeals)) AS 'PercentFreeSchoolMeals',
                            CONVERT(float, AVG(PercentSpecialEducationNeeds)) AS 'PercentSpecialEducationNeeds',
                            CONVERT(float, SUM(TotalInternalFloorArea)) AS 'TotalInternalFloorArea'
                     FROM NonFinancial nf
                         INNER JOIN School s ON s.URN = nf.URN
                     WHERE s.TrustCompanyNumber is not null
                       AND nf.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
                       AND nf.RunType = 'default'
                     GROUP BY s.TrustCompanyNumber),
     income AS (SELECT CompanyNumber,
                       CONVERT(float, TotalIncome) AS 'TotalIncome'
                FROM TrustFinancial f
                WHERE f.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
                  AND f.RunType = 'default')
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
             LEFT OUTER JOIN schools st ON st.CompanyNumber = t.CompanyNumber
             LEFT OUTER JOIN phases sp ON sp.CompanyNumber = t.CompanyNumber
             LEFT OUTER JOIN census tnf ON tnf.CompanyNumber = t.CompanyNumber
             LEFT OUTER JOIN income tf ON tf.CompanyNumber = t.CompanyNumber
GO

