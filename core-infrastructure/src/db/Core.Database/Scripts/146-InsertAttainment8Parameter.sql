IF EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'Parameters')
BEGIN
INSERT INTO Parameters VALUES ('LatestAttainment8Year', '2025');
END;
