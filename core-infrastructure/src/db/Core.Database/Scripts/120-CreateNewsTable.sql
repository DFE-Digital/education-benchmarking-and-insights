IF NOT EXISTS(SELECT *
    FROM INFORMATION_SCHEMA.TABLES
    WHERE table_name = 'News')
BEGIN
    CREATE TABLE [dbo].[News]
    (
        Title       nvarchar(255)   NOT NULL,
        Slug        nvarchar(255)   NOT NULL,
        Body        nvarchar(max)   NOT NULL,
        Created     datetimeoffset  NOT NULL DEFAULT GETUTCDATE(),
        Updated     datetimeoffset  NOT NULL DEFAULT GETUTCDATE(),
        Published   datetimeoffset  NULL,
        Archived    datetimeoffset  NULL,

        CONSTRAINT PK_News PRIMARY KEY (Slug),
    );
END;