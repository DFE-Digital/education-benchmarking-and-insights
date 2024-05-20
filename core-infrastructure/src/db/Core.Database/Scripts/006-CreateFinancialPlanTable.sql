IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'FinancialPlan')
    BEGIN
        CREATE TABLE dbo.FinancialPlan
        (
            UKPRN          nvarchar(50)   NOT NULL,
            Year           smallint       NOT NULL,
            Input          nvarchar(max),
            DeploymentPlan nvarchar(max),
            Created        datetimeoffset NOT NULL,
            CreatedBy      nvarchar(255)  NOT NULL,
            UpdatedAt      datetimeoffset NOT NULL,
            UpdatedBy      nvarchar(255)  NOT NULL,
            IsComplete     bit            NOT NULL,
            Version        int            NOT NULL,


            CONSTRAINT PK_FinancialPlan PRIMARY KEY (UKPRN, Year),
        );
    END
