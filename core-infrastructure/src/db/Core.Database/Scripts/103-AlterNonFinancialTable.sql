ALTER TABLE NonFinancial
  ADD FinanceType  nvarchar(10) NULL
    , OverallPhase nvarchar(50) NULL
GO

UPDATE NonFinancial
   SET FinanceType = School.FinanceType
     , OverallPhase = School.OverallPhase
  FROM NonFinancial
 INNER
  JOIN School
    ON NonFinancial.URN = School.URN
;

ALTER TABLE  NonFinancial
ALTER COLUMN FinanceType nvarchar(10) NOT NULL
;

ALTER TABLE  NonFinancial
ALTER COLUMN OverallPhase nvarchar(50) NOT NULL
;

IF NOT EXISTS (
  SELECT *
    FROM sys.indexes
   WHERE name = 'NonFinancial_FinanceType_OverallPhase'
     AND object_id = OBJECT_ID('[dbo].[NonFinancial]')
)
BEGIN
  CREATE NONCLUSTERED INDEX [NonFinancial_FinanceType_OverallPhase]  ON [dbo].[NonFinancial] ([FinanceType], [OverallPhase]) WITH (ONLINE = ON)
END
