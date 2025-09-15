/*Clean up any legacy or redundant views*/

DROP VIEW IF EXISTS VW_TransparencyFilesAar
GO

DROP VIEW IF EXISTS VW_TransparencyFilesCfr
GO

DROP VIEW IF EXISTS SchoolExpenditureHistoric
    GO

DROP VIEW IF EXISTS SchoolExpenditure
    GO

DROP VIEW IF EXISTS TrustExpenditureHistoric
    GO

DROP VIEW IF EXISTS TrustExpenditure
    GO

DROP VIEW IF EXISTS SchoolExpenditureCustom
    GO

DROP VIEW IF EXISTS SchoolExpenditureHistoricWithNulls
    GO

DROP VIEW IF EXISTS SchoolExpenditureAvgHistoric
    GO

DROP VIEW IF EXISTS SchoolExpenditurePerUnitHistoric
    GO

DROP VIEW IF EXISTS SchoolExpenditureAvgPerUnitHistoric
    GO

DROP VIEW IF EXISTS SchoolExpenditureAvgComparatorSet
    GO

DROP VIEW IF EXISTS SchoolExpenditureAvgPerUnitComparatorSet
    GO

DROP VIEW IF EXISTS SchoolExpenditurePercentageOfExpenditureHistoric
    GO

DROP VIEW IF EXISTS SchoolExpenditurePercentageOfIncomeHistoric
    GO

DROP VIEW IF EXISTS SchoolExpenditureAvgPercentageOfExpenditureHistoric
    GO

DROP VIEW IF EXISTS SchoolExpenditureAvgPercentageOfIncomeHistoric
    GO

DROP VIEW IF EXISTS SchoolExpenditureAvgPercentageOfExpenditureComparatorSet
    GO

DROP VIEW IF EXISTS SchoolExpenditureAvgPercentageOfIncomeComparatorSet
    GO
