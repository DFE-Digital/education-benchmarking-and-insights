IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'FinancialPlan')
    BEGIN
        ALTER TABLE FinancialPlan DROP CONSTRAINT PK_FinancialPlan;

        ALTER TABLE FinancialPlan ADD IsComplete bit NOT NULL;
        ALTER TABLE FinancialPlan ADD Version int NOT NULL;
        ALTER TABLE FinancialPlan ALTER COLUMN Year INT NOT NULL;

        ALTER TABLE FinancialPlan ADD CONSTRAINT PK_FinancialPlan PRIMARY KEY (URN, Year);
    END
