IF EXISTS(SELECT 1
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'LocalAuthorityFinancial')
    BEGIN
    ALTER TABLE dbo.LocalAuthorityFinancial
        ADD TotalPupils decimal NULL;
    END;

IF EXISTS(SELECT 1
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'LocalAuthorityNonFinancial')
    BEGIN
    ALTER TABLE dbo.LocalAuthorityNonFinancial
        ADD TotalPupils decimal NULL;
    END;