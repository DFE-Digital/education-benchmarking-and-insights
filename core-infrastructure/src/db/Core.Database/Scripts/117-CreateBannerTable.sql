IF NOT EXISTS(SELECT *
    FROM INFORMATION_SCHEMA.TABLES
    WHERE table_name = 'Banner')
BEGIN
    CREATE TABLE [dbo].[Banner]
    (
        Id          integer identity (1,1)  NOT NULL,
        Title       nvarchar(255)           NOT NULL,
        Body        nvarchar(max)           NOT NULL,
        Target      nvarchar(255)           NOT NULL,
        ValidFrom   datetimeoffset          NOT NULL DEFAULT GETUTCDATE(),
        ValidTo     datetimeoffset          NULL,

        CONSTRAINT PK_Banner PRIMARY KEY (Id),
    );
END;