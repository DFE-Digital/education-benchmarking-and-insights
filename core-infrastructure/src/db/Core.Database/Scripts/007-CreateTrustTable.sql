IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'Trust')
    BEGIN
        CREATE TABLE dbo.Trust
        (
            UKPRN         nvarchar(50)  NOT NULL,
            CompanyNumber nvarchar(8)   NOT NULL,
            TrustName     nvarchar(255) NOT NULL,
            CFOName       nvarchar(255) NOT NULL,
            CFOEmail      nvarchar(255) NOT NULL,
            OpenDate      date          NOT NULL,
            UID           nvarchar(50)  NOT NULL,
            
            CONSTRAINT PK_Trust PRIMARY KEY (UKPRN),
        );

        CREATE INDEX Trust_CompanyNumber ON Trust (CompanyNumber)
    END   