IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'LocalAuthority')
    BEGIN
        CREATE TABLE dbo.LocalAuthority
        (
            Code nvarchar(3)   NOT NULL,
            Name nvarchar(100) NOT NULL,

            CONSTRAINT PK_LocalAuthority PRIMARY KEY (Code)
        );
    END   