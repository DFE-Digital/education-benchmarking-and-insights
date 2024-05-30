IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.COLUMNS
          WHERE TABLE_NAME = 'School'
            AND COLUMN_NAME = 'OfstedRating')
    BEGIN
        ALTER TABLE MetricRAG ADD SetType nvarchar(50) NOT NULL
    END