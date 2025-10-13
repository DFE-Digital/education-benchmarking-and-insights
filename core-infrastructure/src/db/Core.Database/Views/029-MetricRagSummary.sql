DROP VIEW IF EXISTS VW_MetricRagSummaryExcludingOther;
GO

CREATE VIEW VW_MetricRagSummaryExcludingOther AS
SELECT
    mr.URN,
    mr.RunType,
    mr.RunId,
    s.OverallPhase,
    s.SchoolName,
    s.TrustCompanyNumber,
    s.LACode,
    s.FinanceType,
    SUM(CASE WHEN mr.RAG = 'red' THEN 1 ELSE 0 END) AS RedCount,
    SUM(CASE WHEN mr.RAG = 'amber' THEN 1 ELSE 0 END) AS AmberCount,
    SUM(CASE WHEN mr.RAG = 'green' THEN 1 ELSE 0 END) AS GreenCount
FROM MetricRAG mr
INNER JOIN School s ON mr.URN = s.URN
WHERE SubCategory = 'Total'
    AND Category <> 'Other costs'
GROUP BY
    mr.URN,
    mr.RunType,
    mr.RunId,
    s.OverallPhase,
    s.SchoolName,
    s.TrustCompanyNumber,
    s.LACode,
    s.FinanceType;
GO

DROP VIEW IF EXISTS VW_MetricRagSummaryExcludingOtherDefaultCurrent;
GO

CREATE VIEW VW_MetricRagSummaryExcludingOtherDefaultCurrent AS
SELECT
    URN,
    OverallPhase,
    SchoolName,
    TrustCompanyNumber,
    LACode,
    FinanceType,
    RedCount,
    AmberCount,
    GreenCount
FROM VW_MetricRagSummaryExcludingOther
WHERE RunType = 'default'
    AND RunId = (
        SELECT Value
        FROM Parameters
        WHERE Name = 'CurrentYear'
    );
GO
