IF EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE table_name = 'LASchoolRiskIndicators'
)
BEGIN
    IF EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE table_name = 'LASchoolRiskIndicators'
          AND column_name = 'RiskIndicatorValue'
          AND data_type = 'decimal'
    )
    BEGIN
        ALTER TABLE dbo.LASchoolRiskIndicators
        ALTER COLUMN RiskIndicatorValue nvarchar(100) NULL;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE table_name = 'LASchoolRiskIndicators'
          AND column_name = 'RiskIndicatorValueFormatting'
    )
    BEGIN
        ALTER TABLE dbo.LASchoolRiskIndicators
        ADD RiskIndicatorValueFormatting nvarchar(50) NOT NULL CONSTRAINT DF_LASchoolRiskIndicators_Formatting DEFAULT 'Decimal';
    END;
END;
