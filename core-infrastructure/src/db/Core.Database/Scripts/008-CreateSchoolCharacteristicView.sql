IF NOT EXISTS(SELECT 1
              FROM sys.views
              WHERE name = 'SchoolCharacteristic')
    BEGIN
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
    END
GO