IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'School')
    BEGIN
        CREATE TABLE dbo.School
        (
            UKPRN               nvarchar(50)  NOT NULL,
            URN                 nvarchar(6)   NOT NULL,
            SchoolName          nvarchar(255) NOT NULL,
            TrustUKPRN          nvarchar(50)  NOT NULL,
            TrustName           nvarchar(255) NOT NULL,
            FederationLeadUKPRN nvarchar(50)  NOT NULL,
            FederationLeadName  nvarchar(255) NOT NULL,
            LACode              nvarchar(3)   NOT NULL,
            LAName              nvarchar(100) NOT NULL,
            LondonWeighting     nvarchar(10)  NOT NULL,
            FinanceType         nvarchar(10)  NOT NULL,
            OverallPhase        nvarchar(20)  NOT NULL,
            SchoolType          nvarchar(20)  NOT NULL,
            HasSixthForm        bit           NOT NULL,
            HasNursery          bit           NOT NULL,
            IsPFISchool         bit           NOT NULL,
            OfstedDate          date          NULL,
            OfstedRating        tinyint       NULL,
            OfstedDescription   nvarchar(20)  NULL,
            Telephone           nvarchar(20)  NULL,
            Website             nvarchar(255) NULL,
            ContactEmail        nvarchar(255) NULL,
            HeadteacherName     nvarchar(255) NOT NULL,
            HeadteacherEmail    nvarchar(255) NOT NULL,

            CONSTRAINT PK_School PRIMARY KEY (UKPRN),
        );

        CREATE INDEX School_URN ON School (URN)
        CREATE INDEX School_TrustUKPRN ON School (TrustUKPRN)
        CREATE INDEX School_FederationLeadUKPRN ON School (FederationLeadUKPRN)
        CREATE INDEX School_LACode ON School (LACode)
    END   