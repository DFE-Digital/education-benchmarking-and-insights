IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'UserData')
    BEGIN
        CREATE TABLE dbo.UserData
        (
            Id     nvarchar(50)   NOT NULL,
            Type   nvarchar(50)   NOT NULL,
            UserId nvarchar(255)  NOT NULL,
            Status nvarchar(10)   NOT NULL,
            Expiry datetimeoffset NOT NULL,

            CONSTRAINT PK_UserData PRIMARY KEY (Id)
        );

        CREATE INDEX UserData_UserId ON School (UserId)
    END;