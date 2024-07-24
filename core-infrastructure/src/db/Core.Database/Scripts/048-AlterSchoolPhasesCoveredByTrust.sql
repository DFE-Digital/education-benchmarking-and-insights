ALTER VIEW SchoolPhasesCoveredByTrust AS
SELECT
    CompanyNumber,
    CONCAT(
        '[',
        STRING_AGG(
            CONCAT(
                '{"phase":"',
                OverallPhase,
                '","count":',
                Count,
                '}'
            ),
            ','
        ),
        ']'
    ) 'PhasesCovered'
FROM
    (
        SELECT
            DISTINCT TrustCompanyNumber 'CompanyNumber',
            OverallPhase,
            COUNT(OverallPhase) 'Count'
        FROM
            School
        WHERE
            TrustCompanyNumber IS NOT NULL
        GROUP BY
            TrustCompanyNumber,
            OverallPhase
    ) t
GROUP BY
    t.CompanyNumber
GO