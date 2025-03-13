
IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'LocalAuthorityStatisticalNeighbour')
    BEGIN
        CREATE TABLE dbo.LocalAuthorityStatisticalNeighbour
        (
            RunType              nvarchar(50)  NOT NULL,
            RunId                nvarchar(50)  NOT NULL,
            LaCode               nvarchar(3)   NOT NULL,
            NeighbourPosition    tinyint       NOT NULL,
            NeighbourLaCode      nvarchar(3)   NOT NULL,
            NeighbourDescription nvarchar(100) NOT NULL,

            CONSTRAINT PK_LocalAuthorityStatisticalNeighbour PRIMARY KEY (RunType, RunId, LaCode, NeighbourPosition)
        );
    END;
 

