IF EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE table_name = 'NonFinancial'
)
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE table_name = 'NonFinancial'
          AND column_name = 'Attainment8'
    )
    BEGIN
        ALTER TABLE dbo.[NonFinancial]
            ADD Attainment8 DECIMAL(18,2) NULL;
    END;
END;
