IF NOT EXISTS (SELECT *
               FROM sys.indexes
               WHERE name = 'Financial_RunType_URN'
                 AND object_id = OBJECT_ID('[dbo].[Financial]'))
    BEGIN
        CREATE NONCLUSTERED INDEX Financial_RunType_URN ON [dbo].[Financial] ([RunType], [URN])
    END

IF NOT EXISTS (SELECT *
               FROM sys.indexes
               WHERE name = 'NonFinancial_RunType_URN'
                 AND object_id = OBJECT_ID('[dbo].[NonFinancial]'))
    BEGIN
        CREATE NONCLUSTERED INDEX NonFinancial_RunType_URN ON [dbo].[NonFinancial] ([RunType], [URN])
    END