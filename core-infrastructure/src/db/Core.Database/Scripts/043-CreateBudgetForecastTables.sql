IF EXISTS (SELECT *
           FROM INFORMATION_SCHEMA.TABLES
           WHERE table_name = 'BudgetForecastReturns')
    BEGIN
        DROP TABLE dbo.BudgetForecastReturns;
    END;

IF EXISTS (SELECT *
           FROM INFORMATION_SCHEMA.TABLES
           WHERE table_name = 'BudgetForecastReturnsMetrics')
    BEGIN
        DROP TABLE dbo.BudgetForecastReturnsMetrics;
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
            Forecast        decimal(16, 2) NULL,
            Actual          decimal(16, 2) NULL,
            TotalPupils     decimal(16, 2) NULL,
            Slope           decimal(16, 2) NULL,
            Variance        decimal(16, 2) NULL,
            PercentVariance decimal(16, 2) NULL,

            CONSTRAINT PK_BudgetForecastReturn PRIMARY KEY (RunType, RunId, CompanyNumber, Year, Category)
        );
    END;

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'BudgetForecastReturnMetric')
    BEGIN
        CREATE TABLE dbo.BudgetForecastReturnMetric
        (
            RunType       nvarchar(50)   NOT NULL,
            RunId         nvarchar(50)   NOT NULL,
            Year          int            NOT NULL,
            CompanyNumber nvarchar(8)    NOT NULL,
            Metric        nvarchar(50)   NOT NULL,
            Value         decimal(16, 2) NULL,
            
            CONSTRAINT PK_BudgetForecastReturnMetric PRIMARY KEY (RunType, RunId, CompanyNumber, Year, Metric)
        );
    END;    