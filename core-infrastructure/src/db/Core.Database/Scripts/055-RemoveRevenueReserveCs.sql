IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'Financial')
    BEGIN
       ALTER TABLE Financial DROP COLUMN RevenueReserveCS;
    END


IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'TrustFinancial')
    BEGIN
       ALTER TABLE TrustFinancial DROP COLUMN RevenueReserveCS;
    END
