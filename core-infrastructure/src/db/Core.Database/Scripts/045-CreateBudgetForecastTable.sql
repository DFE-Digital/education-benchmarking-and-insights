IF EXISTS (SELECT *
           FROM INFORMATION_SCHEMA.TABLES
           WHERE table_name = 'BudgetForecastReturn')
    BEGIN
        DROP TABLE dbo.BudgetForecastReturn;
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'BudgetForecastReturn')
    BEGIN
        CREATE TABLE dbo.BudgetForecastReturn
        (
            RunType         nvarchar(50)   NOT NULL,
            RunId           nvarchar(50)   NOT NULL,
            Year            int            NOT NULL,
            CompanyNumber   nvarchar(8)    NOT NULL,
            Category        nvarchar(50)   NOT NULL,
            Value           decimal(16, 2) NULL,
            TotalPupils     decimal(16, 2) NULL,

            CONSTRAINT PK_BudgetForecastReturn PRIMARY KEY (RunType, RunId, CompanyNumber, Year, Category)
        );
    END;  