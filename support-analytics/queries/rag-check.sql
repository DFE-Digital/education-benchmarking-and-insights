SELECT r.URN, r.Category, r.SubCategory, r.Value, r.Mean, r.DiffMean, r.Value - r.Mean 'DiffCalc', ABS(DiffMean - ( r.Value - r.Mean)) 'Variance' FROM MetricRAG r
WHERE RunType = 'default'
  AND RunId = '2023'
  AND ABS(DiffMean - ( r.Value - r.Mean)) > 0.01
ORDER BY Variance DESC


SELECT r.URN, r.Category, r.SubCategory, r.Mean, r.DiffMean, r.PercentDiff,
       IIF(r.Mean = 0, 0, r.DiffMean / r.Mean * 100) 'PercentDiffCalc',
       ABS(r.PercentDiff - (IIF(r.Mean = 0, 0, r.DiffMean / r.Mean * 100))) 'Variance' FROM MetricRAG r
WHERE RunType = 'default'
  AND RunId = '2023'
ORDER BY Variance DESC