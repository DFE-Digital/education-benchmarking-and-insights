IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'DatasetType')
    BEGIN
        CREATE TABLE dbo.DatasetType
        (
              DatasetShortCode varchar(16)   NOT NULL,
              Description      varchar(255)      NULL

              CONSTRAINT PK_DatasetType PRIMARY KEY (DatasetShortCode)
        );
    END
GO

IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'FileIntegrityHistory')
    BEGIN
        CREATE TABLE dbo.FileIntegrityHistory
        (
                  DatasetShortCode varchar(16)   NOT NULL,
                  DatasetYear      smallint      NOT NULL,
                  FileName         varchar(255)  NOT NULL,
                  FileSizeBytes    int           NOT NULL,
                  HashValue        varchar(255)  NOT NULL,
                  HashAlgorithm    varchar(16)   NOT NULL,
                  Description      nvarchar(255)     NULL,
                  CreatedAt        datetime      NOT NULL

                  CONSTRAINT FK_FileIntegrityHistory_DatasetType FOREIGN KEY (DatasetShortCode) REFERENCES DatasetType(DatasetShortCode),
                  CONSTRAINT PK_FileIntegrityHistory             PRIMARY KEY (DatasetShortCode, DatasetYear, CreatedAt)
        );
    END
GO
