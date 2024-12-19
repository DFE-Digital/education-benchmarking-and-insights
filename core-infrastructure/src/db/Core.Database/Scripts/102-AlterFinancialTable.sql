ALTER TABLE Financial
  ADD FinanceType  nvarchar(10) NULL
    , OverallPhase nvarchar(50) NULL
GO

UPDATE Financial
   SET FinanceType = School.FinanceType
     , OverallPhase = School.OverallPhase
  FROM Financial
 INNER
  JOIN School
    ON Financial.URN = School.URN
;

ALTER TABLE  Financial
ALTER COLUMN FinanceType nvarchar(10) NOT NULL
;

ALTER TABLE  Financial
ALTER COLUMN OverallPhase nvarchar(50) NOT NULL
;

IF NOT EXISTS (
  SELECT *
    FROM sys.indexes
   WHERE name = 'Financial_FinanceType_OverallPhase'
     AND object_id = OBJECT_ID('[dbo].[Financial]')
)
BEGIN
  CREATE NONCLUSTERED INDEX [Financial_FinanceType_OverallPhase]  ON [dbo].[Financial] ([FinanceType], [OverallPhase]) WITH (ONLINE = ON)
END
