IF EXISTS(SELECT 1
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'NonFinancial')
    BEGIN
    ALTER TABLE NonFinancial ADD EHCPlan smallint NULL;
    ALTER TABLE NonFinancial ADD SENSupport smallint NULL;
    END;
