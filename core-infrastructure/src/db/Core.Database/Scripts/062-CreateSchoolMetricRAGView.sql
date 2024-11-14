IF EXISTS(
   SELECT
      1
   FROM
      sys.views
   WHERE
      name = 'SchoolMetricRAG'
) BEGIN DROP VIEW SchoolMetricRAG
END
GO
   CREATE VIEW [dbo].[SchoolMetricRAG] AS
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
   r.Mean,
   r.DiffMean,
   r.PercentDiff,
   r.Percentile,
   r.Decile,
   r.RAG
FROM
   School s
   LEFT JOIN MetricRAG r ON r.URN = s.URN
GO