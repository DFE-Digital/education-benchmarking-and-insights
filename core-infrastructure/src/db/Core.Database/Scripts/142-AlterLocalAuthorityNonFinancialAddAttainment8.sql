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
          AND column_name = 'LocalAuthorityAvgAttainment8'
    )
    BEGIN
    ALTER TABLE dbo.[LocalAuthorityNonFinancial]
        ADD LocalAuthorityAvgAttainment8 DECIMAL(18,2) NULL;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE table_name = 'LocalAuthorityNonFinancial'
          AND column_name = 'NationalAvgAttainment8'
    )
    BEGIN
    ALTER TABLE dbo.[LocalAuthorityNonFinancial]
        ADD NationalAvgAttainment8 DECIMAL(18,2) NULL;
    END;
END;
