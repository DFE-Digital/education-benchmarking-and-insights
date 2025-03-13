DROP VIEW IF EXISTS VW_LocalAuthorityStatisticalNeighbours
GO
    CREATE VIEW VW_LocalAuthorityStatisticalNeighbours AS
SELECT
    [neighbour].[LaCode],
    [source].[Name] AS [LaName],
    [neighbour].[NeighbourPosition],
    [neighbour].[NeighbourLaCode],
    [target].[Name] AS [NeighbourLaName]
FROM
    [dbo].[LocalAuthorityStatisticalNeighbour] [neighbour]
    LEFT OUTER JOIN [dbo].[LocalAuthority] [source] ON [source].[Code] = [neighbour].[LaCode]
    LEFT OUTER JOIN [dbo].[LocalAuthority] [target] ON [target].[Code] = [neighbour].[NeighbourLaCode]
WHERE
    [neighbour].[RunType] = 'default'
    AND [neighbour].[RunId] = (
        SELECT
            [Value]
        FROM
            [Parameters]
        WHERE
            [Name] = 'LatestS251Year'
    )
GO