DECLARE @School AS NVARCHAR(6)='142875'
DECLARE @Year AS NVARCHAR(4)='2023'
DECLARE @Category AS NVARCHAR(100)='Administrative supplies'

SELECT *
FROM MetricRAG
WHERE RunType = 'default' AND RunId = @Year AND URN = @School AND Category = @Category