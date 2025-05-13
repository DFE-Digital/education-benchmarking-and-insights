IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'CommercialResources')
BEGIN
CREATE TABLE [dbo].[CommercialResources]
(
    ResourceID   INT IDENTITY(1,1) PRIMARY KEY,
    Category    nvarchar(50)   NOT NULL,
    SubCategory nvarchar(50)   NOT NULL,
    Title       nvarchar(255)  NOT NULL,
    Url         nvarchar(2000)  NOT NULL,
    ValidFrom   datetimeoffset NOT NULL DEFAULT GETUTCDATE(),
    ValidTo     datetimeoffset NULL
    );
END