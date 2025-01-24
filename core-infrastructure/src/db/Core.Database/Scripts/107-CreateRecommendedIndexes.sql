IF NOT EXISTS (
  SELECT *
    FROM sys.indexes
   WHERE name = 'UserData_Organisation_Status'
     AND object_id = OBJECT_ID('[dbo].[UserData]')
)
BEGIN
    CREATE NONCLUSTERED INDEX [UserData_Organisation_Status] ON [dbo].[UserData] ([OrganisationId], [OrganisationType], [Status]) INCLUDE ([UserId]) WITH (ONLINE = ON)
END
