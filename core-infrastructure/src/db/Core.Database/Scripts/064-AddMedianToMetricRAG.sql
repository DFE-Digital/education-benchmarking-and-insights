IF EXISTS (
SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE table_name = 'MetricRAG'
) 
BEGIN
    IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE Name = N'Median' AND Object_ID = Object_ID(N'dbo.MetricRAG')
    ) 
    BEGIN
        ALTER TABLE MetricRAG ADD Median decimal(16, 2) NULL;
    END

    IF NOT EXISTS(
    SELECT 1
    FROM sys.columns
    WHERE Name = N'DiffMedian' AND Object_ID = Object_ID(N'dbo.MetricRAG')
    ) 
    BEGIN
        ALTER TABLE MetricRAG ADD DiffMedian decimal(16, 2) NULL;
    END
END