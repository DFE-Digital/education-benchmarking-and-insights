IF NOT EXISTS (
  SELECT *
    FROM sys.indexes
   WHERE name = 'ComparatorSet_RunType_URN'
     AND object_id = OBJECT_ID('[dbo].[ComparatorSet]')
)
BEGIN
  CREATE NONCLUSTERED INDEX [ComparatorSet_RunType_URN]  ON [dbo].[ComparatorSet] ([RunType], [URN]) WITH (ONLINE = ON)
END

IF NOT EXISTS (
  SELECT *
    FROM sys.indexes
   WHERE name = 'School_FinanceType_OverallPhase'
     AND object_id = OBJECT_ID('[dbo].[School]')
)
BEGIN
  CREATE NONCLUSTERED INDEX [School_FinanceType_OverallPhase] ON [dbo].[School] ([FinanceType], [OverallPhase]) WITH (ONLINE = ON)
END
