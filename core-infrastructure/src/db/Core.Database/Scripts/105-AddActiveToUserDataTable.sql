ALTER TABLE
  [dbo].[UserData]
ADD
  [Active] bit NOT NULL DEFAULT 1
GO

-- set Active to false for all but the most recently expired user data entries per type
UPDATE
  [userData]
SET
  [userData].[Active] = 0
FROM
  [dbo].[UserData] [userData]
  JOIN (
    SELECT
      [Type],
      [UserId],
      [OrganisationType],
      [OrganisationId],
      [Expiry]
    FROM
      [dbo].[UserData]
    EXCEPT
    SELECT
      [Type],
      [UserId],
      [OrganisationType],
      [OrganisationId],
      MAX([Expiry]) AS [Expiry]
    FROM
      [dbo].[UserData]
    GROUP BY
      [Type],
      [UserId],
      [OrganisationType],
      [OrganisationId]
  ) [expired] ON [userData].[Type] = [expired].[Type]
  AND [userData].[UserId] = [expired].[UserId]
  AND [userData].[OrganisationType] = [expired].[OrganisationType]
  AND [userData].[OrganisationId] = [expired].[OrganisationId]
  AND [userData].[Expiry] = [expired].[Expiry]