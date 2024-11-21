SELECT r.URN, r.Category, r.SubCategory, r.Value, r.Median, r.DiffMedian, r.Value - r.Median 'DiffCalc', ABS(DiffMedian - (r.Value - r.Median)) 'Variance'
FROM MetricRAG r
WHERE RunType = 'default'
  AND RunId = '2023'
  AND ABS(DiffMedian - ( r.Value - r.Median)) > 0.01
ORDER BY Variance DESC


SELECT r.URN, r.Category, r.SubCategory, r.Median, r.DiffMedian, r.PercentDiff,
  IIF(r.Median = 0, 0, r.DiffMedian / r.Median * 100) 'PercentDiffCalc',
  ABS(r.PercentDiff - (IIF(r.Median = 0, 0, r.DiffMedian / r.Median * 100))) 'Variance'
FROM MetricRAG r
WHERE RunType = 'default'
  AND RunId = '2023'
ORDER BY Variance DESC