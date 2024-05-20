IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'TrustHistory')
    BEGIN
        CREATE TABLE dbo.TrustHistory
        (
            Id           integer identity (1,1) NOT NULL,
            TrustUKPRN   nvarchar(50)           NOT NULL,
            EventDate    date                   NOT NULL,
            EventName    nvarchar(100)          NOT NULL,
            AcademicYear smallint               NOT NULL,
            SchoolURN    nvarchar(6)            NOT NULL,
            SchoolName   nvarchar(255)          NOT NULL,

            CONSTRAINT PK_TrustHistory PRIMARY KEY (Id),
        );
    END   