IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'CommercialResources')
BEGIN
CREATE TABLE [dbo].[CommercialResources]
(
    Id          integer identity (1,1) NOT NULL,
    Category    nvarchar(50)   NOT NULL,
    SubCategory nvarchar(50)   NOT NULL,
    Title       nvarchar(255)  NOT NULL,
    Url         nvarchar(2000)  NOT NULL,
    ValidFrom   datetimeoffset NOT NULL DEFAULT GETUTCDATE(),
    ValidTo     datetimeoffset NULL

    CONSTRAINT PK_CommercialResources PRIMARY KEY (Id),
    );
END