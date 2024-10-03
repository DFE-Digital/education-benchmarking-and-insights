IF EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'Parameters')
    BEGIN
        INSERT INTO Parameters VALUES ('LatestBFRYear', '2023');
    END;