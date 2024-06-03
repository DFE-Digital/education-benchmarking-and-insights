IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolCharacteristic')
    BEGIN
        DROP VIEW SchoolCharacteristic
    END
GO

CREATE VIEW SchoolCharacteristic
AS
SELECT URN,
       SchoolName,
       AddressTown,
       AddressPostcode,
       OverallPhase,
       LAName,
       OfstedDescription,
       LondonWeighting,
       FinanceType
FROM dbo.School
GO