IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'MetricRAG')
    BEGIN
        ALTER TABLE MetricRAG
            ALTER COLUMN Value decimal(16, 2);
        ALTER TABLE MetricRAG
            ALTER COLUMN Mean decimal(16, 2);
        ALTER TABLE MetricRAG
            ALTER COLUMN DiffMean decimal(16, 2);
        ALTER TABLE MetricRAG
            ALTER COLUMN PercentDiff decimal(16, 2);
        ALTER TABLE MetricRAG
            ALTER COLUMN Percentile decimal(16, 2);
        ALTER TABLE MetricRAG
            ALTER COLUMN Decile decimal(16, 2);
    END