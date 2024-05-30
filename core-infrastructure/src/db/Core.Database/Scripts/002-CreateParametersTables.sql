IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'Parameters')
    BEGIN
        CREATE TABLE dbo.Parameters
        (
            Name  nvarchar(50) NOT NULL,
            Value nvarchar(50) NOT NULL,

            CONSTRAINT PK_Parameters PRIMARY KEY (Name)
        );

        INSERT INTO Parameters VALUES ('CurrentYear', '2022');
        INSERT INTO Parameters VALUES ('LatestCFRYear', '2023');
        INSERT INTO Parameters VALUES ('LatestAARYear', '2022');
    END;     