IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'RAGRatings')
    BEGIN
        CREATE TABLE dbo.RAGRatings
        (
            [RAGRatingId] [int] NOT NULL,
	        [academies_data_Id] [bigint] NULL,
            [URN] [varchar](10) NULL,
	        [UKPRN] [char](8) NOT NULL,
	        [UKPRN_URN] [varchar](20) NOT NULL,
	        [RAGAcademyName] [varchar](255) NULL,
	        [Sector] [varchar](50) NOT NULL,
	        [Cost Pool] [varchar](100) NOT NULL,
	        [Cost Pool ID] [int] NOT NULL,
	        [CostGroup] [varchar](5) NULL,
	        [PeerGroup] [varchar](10) NOT NULL,
	        [PerUnit] [decimal](38, 0) NULL,
	        [5thDecile] [decimal](38, 5) NULL,
	        [Decile] [int] NULL,
	        [RAG] [int] NOT NULL,
	        [Rank] [int] NULL,
	        [PerUnitText] [varchar](100) NULL,
	        [%ReductionText] [nvarchar](100) NULL,
	        [RankAcademyLevel] [int] NULL,
	        [RAGletter] [varchar](5) NULL,
	        [RAGWord] [varchar](5) NULL,
	        [RAGOrder] [int] NULL,
	        [OverallAcademyRank] [int] NULL,
	        [OverallCostPoolRank] [int] NULL,
	        [PerUnitRank] [int] NULL,
	        [%Comparators] [varchar](50) NULL,
	        [£DiffText] [nvarchar](100) NULL,
	        [%DiffText] [nvarchar](100) NULL,
	        [HomepageText] [nvarchar](280) NULL,
	        [Key] [varchar](18) NULL,
	        [PerUnit_PreviousYear] [decimal](38, 0) NULL,
	        [YOY%change] [decimal](38, 6) NULL,
	        [YOY%changeText] [varchar](200) NULL,
	        [PerUnit_PreviousYearText] [varchar](200) NULL,
	        [DataReleaseId] [int] NULL,
            
            CONSTRAINT [PK_RAGRating] PRIMARY KEY (RAGRatingId) 
        );

        CREATE INDEX RAGRatings_URN_PeerGroup_CostGroup ON RAGRatings (URN, PeerGroup, CostGroup)
    END