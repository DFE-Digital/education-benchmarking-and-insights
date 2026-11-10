IF EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'Parameters')
BEGIN
    DELETE FROM Parameters
    WHERE Name = 'LatestAttainment8Year'
END;