IF EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE table_name = 'LocalAuthorityNonFinancial'
)
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE table_name = 'LocalAuthorityNonFinancial'
          AND column_name = 'LocalAuthorityAttainment8'
    )
    BEGIN
    ALTER TABLE dbo.[LocalAuthorityNonFinancial]
        ADD LocalAuthorityAttainment8 DECIMAL(18,2) NULL;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE table_name = 'LocalAuthorityNonFinancial'
          AND column_name = 'NationalAverageAttainment8'
    )
    BEGIN
    ALTER TABLE dbo.[LocalAuthorityNonFinancial]
        ADD NationalAverageAttainment8 DECIMAL(18,2) NULL;
    END;
END;
