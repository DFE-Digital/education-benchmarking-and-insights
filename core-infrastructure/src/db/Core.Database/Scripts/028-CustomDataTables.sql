IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'CustomDataSchool')
    BEGIN
        CREATE TABLE dbo.CustomDataSchool
        (
            Id   nvarchar(50)  NOT NULL,
            URN  nvarchar(6)   NOT NULL,
            Data nvarchar(max) NULL,

            CONSTRAINT PK_CustomDataSchool PRIMARY KEY (Id)
        );
    END;   