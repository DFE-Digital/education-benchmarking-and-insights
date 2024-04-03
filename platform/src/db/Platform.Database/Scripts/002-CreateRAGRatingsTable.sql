IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'ComparatorSets')
    BEGIN
        CREATE TABLE dbo.ComparatorSets
        (
            all_comparators_all_Id bigint       NOT NULL,
            trust_academy_Id       bigint       NOT NULL,
            URN                    int          NOT NULL,
            UKPRN_URN1             nvarchar(50) NOT NULL,
            UKPRN_URN2             nvarchar(50) NOT NULL,
            UKPRN_URN_CG           nvarchar(50) NOT NULL,
            PeerGroup              nvarchar(50) NOT NULL,
            CostGroup              nvarchar(50) NOT NULL,
            CompareNum             bit          NOT NULL,
            compare                bit          NOT NULL,
            RANK2                  int          NOT NULL,
            Comparator_code        int          NOT NULL,
            RANK3                  int          NOT NULL,
            ReprocessFlag          int          NOT NULL,
            UseAllCompFlag         bit          NOT NULL,
            Range_flag             bit          NOT NULL,
            DataReleaseId          int          NOT NULL,
            PartYearDataFlag       int          NOT NULL,

            CONSTRAINT PK_ComparatorSets PRIMARY KEY (all_comparators_all_Id)
        );

        CREATE INDEX ComparatorSets_URN_PeerGroup_CostGroup ON ComparatorSets ( URN, PeerGroup, CostGroup)
    END