IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE table_name = 'MetricRAG'
) 
BEGIN
    IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE Name = N'Mean' AND Object_ID = Object_ID(N'dbo.MetricRAG')
    ) 
    BEGIN
       EXEC sp_rename 'dbo.MetricRAG.Mean', 'Median', 'COLUMN';
    END

    IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE Name = N'DiffMean' AND Object_ID = Object_ID(N'dbo.MetricRAG')
    ) 
    BEGIN
       EXEC sp_rename 'dbo.MetricRAG.DiffMean', 'DiffMedian', 'COLUMN';
    END
END
GO

ALTER VIEW [dbo].[SchoolMetricRAG] AS
SELECT
   s.URN,
   s.OverallPhase,
   s.LACode,
   s.TrustCompanyNumber,
   r.RunType,
   r.RunId,
   r.Category,
   r.SubCategory,
   r.Value,
   r.Median,
   r.DiffMedian,
   r.PercentDiff,
   r.Percentile,
   r.Decile,
   r.RAG
FROM
   School s
   LEFT JOIN MetricRAG r ON r.URN = s.URN
GO