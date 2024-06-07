IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'TrustCharacteristic')
    BEGIN
        DROP VIEW TrustCharacteristic
    END
GO

CREATE VIEW TrustCharacteristic
AS
SELECT CompanyNumber,
       TrustName
FROM dbo.Trust
GO