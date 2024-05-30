IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.COLUMNS
          WHERE TABLE_NAME = 'MetricRAG'
            AND COLUMN_NAME = 'SetType')
    BEGIN
        ALTER TABLE MetricRAG ADD SetType nvarchar(50) NOT NULL
    END