IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'FinancialPlan')
    BEGIN
        CREATE TABLE dbo.FinancialPlan
        (
            URN            nvarchar(6)    NOT NULL,
            Year           nvarchar(4)    NOT NULL,
            Input          nvarchar(max),
            DeploymentPlan nvarchar(max),
            Created        datetimeoffset NOT NULL,
            CreatedBy      nvarchar(255)  NOT NULL,
            UpdatedAt      datetimeoffset NOT NULL,
            UpdatedBy      nvarchar(255)  NOT NULL,

            CONSTRAINT PK_FinancialPlan PRIMARY KEY (URN, Year),
        );
    END
