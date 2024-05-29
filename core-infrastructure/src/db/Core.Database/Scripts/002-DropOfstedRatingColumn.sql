IF EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.COLUMNS
              WHERE TABLE_NAME = 'School'
              AND COLUMN_NAME = 'OfstedRating')
    BEGIN
        ALTER TABLE School DROP COLUMN OfstedRating
    END