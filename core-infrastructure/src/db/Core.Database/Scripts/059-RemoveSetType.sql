DELETE FROM ComparatorSet WHERE SetType = 'mixed' AND RunType = 'default';

IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'ComparatorSet')
    BEGIN
       ALTER TABLE ComparatorSet DROP CONSTRAINT PK_ComparatorSet;
       ALTER TABLE ComparatorSet DROP COLUMN SetType;
       ALTER TABLE ComparatorSet ADD CONSTRAINT PK_ComparatorSet PRIMARY KEY (RunType, RunId, URN);
    END

DELETE FROM MetricRAG WHERE SetType = 'mixed' AND RunType = 'default';

IF EXISTS(SELECT *
          FROM INFORMATION_SCHEMA.TABLES
          WHERE table_name = 'MetricRAG')
    BEGIN
       ALTER TABLE MetricRAG DROP CONSTRAINT PK_MetricRAG;
       ALTER TABLE MetricRAG DROP COLUMN SetType;
       ALTER TABLE MetricRAG ADD CONSTRAINT PK_MetricRAG PRIMARY KEY (RunType, RunId, URN, Category, SubCategory);
    END
