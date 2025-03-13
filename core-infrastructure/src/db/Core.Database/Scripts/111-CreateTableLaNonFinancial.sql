
IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'LocalAuthorityNonFinancial')
    BEGIN
        CREATE TABLE dbo.LocalAuthorityNonFinancial
        (
            RunType         nvarchar(50) NOT NULL,
            RunId           nvarchar(50) NOT NULL,
            LaCode          nvarchar(3)  NOT NULL,
            Population2To18 decimal      NULL,
            EHCPTotal       decimal      NULL,
            EHCPMainstream  decimal      NULL,
            EHCPResourced   decimal      NULL,
            EHCPSpecial     decimal      NULL,
            EHCPIndependent decimal      NULL,
            EHCPHospital    decimal      NULL,
            EHCPPost16      decimal      NULL,
            EHCPOther       decimal      NULL,

            CONSTRAINT PK_LocalAuthorityNonFinancial PRIMARY KEY (RunType, RunId, LaCode)
        );
    END;
