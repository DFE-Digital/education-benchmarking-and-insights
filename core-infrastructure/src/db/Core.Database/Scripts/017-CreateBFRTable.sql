IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'BudgetForecastReturns')
    BEGIN
        CREATE TABLE dbo.BudgetForecastReturns
        (
            RunType         nvarchar(50)  NOT NULL,
            RunId           nvarchar(50)  NOT NULL,
            TrustUPIN       nvarchar(6)   NOT NULL,

            CONSTRAINT PK_BudgetForecastReturns PRIMARY KEY (RunType, RunId, TrustUPIN)
        );
    END;
