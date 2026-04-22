IF EXISTS(SELECT 1
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'LocalAuthorityNonFinancial')
    BEGIN
    ALTER TABLE dbo.LocalAuthorityNonFinancial
        ADD SENSupport DECIMAL(16,2) NULL;
    END;