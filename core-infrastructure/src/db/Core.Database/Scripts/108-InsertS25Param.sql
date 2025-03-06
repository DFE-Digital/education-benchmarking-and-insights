IF EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'Parameters')
    BEGIN
        INSERT INTO Parameters VALUES ('LatestS251Year', '2024');
    END;