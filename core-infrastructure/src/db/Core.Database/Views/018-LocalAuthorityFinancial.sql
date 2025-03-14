DROP VIEW IF EXISTS VW_LocalAuthorityFinancial
GO

CREATE VIEW VW_LocalAuthorityFinancial
AS
    SELECT RunId,
        RunType,
        LaCode,
        Population2To18,
        OutturnTotalHighNeeds,
        OutturnTotalPlaceFunding,
        OutturnTotalTopUpFundingMaintained,
        OutturnTotalTopUpFundingNonMaintained,
        OutturnTotalSenServices,
        OutturnTotalAlternativeProvisionServices,
        OutturnTotalHospitalServices,
        OutturnTotalOtherHealthServices,
        OutturnTopFundingMaintainedEarlyYears,
        OutturnTopFundingMaintainedPrimary,
        OutturnTopFundingMaintainedSecondary,
        OutturnTopFundingMaintainedSpecial,
        OutturnTopFundingMaintainedAlternativeProvision,
        OutturnTopFundingMaintainedPostSchool,
        OutturnTopFundingMaintainedIncome,
        OutturnTopFundingNonMaintainedEarlyYears,
        OutturnTopFundingNonMaintainedPrimary,
        OutturnTopFundingNonMaintainedSecondary,
        OutturnTopFundingNonMaintainedSpecial,
        OutturnTopFundingNonMaintainedAlternativeProvision,
        OutturnTopFundingNonMaintainedPostSchool,
        OutturnTopFundingNonMaintainedIncome,
        OutturnPlaceFundingPrimary,
        OutturnPlaceFundingSecondary,
        OutturnPlaceFundingSpecial,
        OutturnPlaceFundingAlternativeProvision,
        BudgetTotalHighNeeds,
        BudgetTotalPlaceFunding,
        BudgetTotalTopUpFundingMaintained,
        BudgetTotalTopUpFundingNonMaintained,
        BudgetTotalSenServices,
        BudgetTotalAlternativeProvisionServices,
        BudgetTotalHospitalServices,
        BudgetTotalOtherHealthServices,
        BudgetTopFundingMaintainedEarlyYears,
        BudgetTopFundingMaintainedPrimary,
        BudgetTopFundingMaintainedSecondary,
        BudgetTopFundingMaintainedSpecial,
        BudgetTopFundingMaintainedAlternativeProvision,
        BudgetTopFundingMaintainedPostSchool,
        BudgetTopFundingMaintainedIncome,
        BudgetTopFundingNonMaintainedEarlyYears,
        BudgetTopFundingNonMaintainedPrimary,
        BudgetTopFundingNonMaintainedSecondary,
        BudgetTopFundingNonMaintainedSpecial,
        BudgetTopFundingNonMaintainedAlternativeProvision,
        BudgetTopFundingNonMaintainedPostSchool,
        BudgetTopFundingNonMaintainedIncome,
        BudgetPlaceFundingPrimary,
        BudgetPlaceFundingSecondary,
        BudgetPlaceFundingSpecial,
        BudgetPlaceFundingAlternativeProvision
    FROM LocalAuthorityFinancial
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialPerPopulation
GO

CREATE VIEW VW_LocalAuthorityFinancialPerPopulation
AS
    SELECT RunId,
        RunType,
        LaCode,
        Population2To18,
        IIF(Population2To18 > 0.0, OutturnTotalHighNeeds / Population2To18, NULL) AS OutturnTotalHighNeeds,
        IIF(Population2To18 > 0.0, OutturnTotalPlaceFunding / Population2To18, NULL) AS OutturnTotalPlaceFunding,
        IIF(Population2To18 > 0.0, OutturnTotalTopUpFundingMaintained / Population2To18, NULL) AS OutturnTotalTopUpFundingMaintained,
        IIF(Population2To18 > 0.0, OutturnTotalTopUpFundingNonMaintained / Population2To18, NULL) AS OutturnTotalTopUpFundingNonMaintained,
        IIF(Population2To18 > 0.0, OutturnTotalSenServices / Population2To18, NULL) AS OutturnTotalSenServices,
        IIF(Population2To18 > 0.0, OutturnTotalAlternativeProvisionServices / Population2To18, NULL) AS OutturnTotalAlternativeProvisionServices,
        IIF(Population2To18 > 0.0, OutturnTotalHospitalServices / Population2To18, NULL) AS OutturnTotalHospitalServices,
        IIF(Population2To18 > 0.0, OutturnTotalOtherHealthServices / Population2To18, NULL) AS OutturnTotalOtherHealthServices,
        IIF(Population2To18 > 0.0, OutturnTopFundingMaintainedEarlyYears / Population2To18, NULL) AS OutturnTopFundingMaintainedEarlyYears,
        IIF(Population2To18 > 0.0, OutturnTopFundingMaintainedPrimary / Population2To18, NULL) AS OutturnTopFundingMaintainedPrimary,
        IIF(Population2To18 > 0.0, OutturnTopFundingMaintainedSecondary / Population2To18, NULL) AS OutturnTopFundingMaintainedSecondary,
        IIF(Population2To18 > 0.0, OutturnTopFundingMaintainedSpecial / Population2To18, NULL) AS OutturnTopFundingMaintainedSpecial,
        IIF(Population2To18 > 0.0, OutturnTopFundingMaintainedAlternativeProvision / Population2To18, NULL) AS OutturnTopFundingMaintainedAlternativeProvision,
        IIF(Population2To18 > 0.0, OutturnTopFundingMaintainedPostSchool / Population2To18, NULL) AS OutturnTopFundingMaintainedPostSchool,
        IIF(Population2To18 > 0.0, OutturnTopFundingMaintainedIncome / Population2To18, NULL) AS OutturnTopFundingMaintainedIncome,
        IIF(Population2To18 > 0.0, OutturnTopFundingNonMaintainedEarlyYears / Population2To18, NULL) AS OutturnTopFundingNonMaintainedEarlyYears,
        IIF(Population2To18 > 0.0, OutturnTopFundingNonMaintainedPrimary / Population2To18, NULL) AS OutturnTopFundingNonMaintainedPrimary,
        IIF(Population2To18 > 0.0, OutturnTopFundingNonMaintainedSecondary / Population2To18, NULL) AS OutturnTopFundingNonMaintainedSecondary,
        IIF(Population2To18 > 0.0, OutturnTopFundingNonMaintainedSpecial / Population2To18, NULL) AS OutturnTopFundingNonMaintainedSpecial,
        IIF(Population2To18 > 0.0, OutturnTopFundingNonMaintainedAlternativeProvision / Population2To18, NULL) AS OutturnTopFundingNonMaintainedAlternativeProvision,
        IIF(Population2To18 > 0.0, OutturnTopFundingNonMaintainedPostSchool / Population2To18, NULL) AS OutturnTopFundingNonMaintainedPostSchool,
        IIF(Population2To18 > 0.0, OutturnTopFundingNonMaintainedIncome / Population2To18, NULL) AS OutturnTopFundingNonMaintainedIncome,
        IIF(Population2To18 > 0.0, OutturnPlaceFundingPrimary / Population2To18, NULL) AS OutturnPlaceFundingPrimary,
        IIF(Population2To18 > 0.0, OutturnPlaceFundingSecondary / Population2To18, NULL) AS OutturnPlaceFundingSecondary,
        IIF(Population2To18 > 0.0, OutturnPlaceFundingSpecial / Population2To18, NULL) AS OutturnPlaceFundingSpecial,
        IIF(Population2To18 > 0.0, OutturnPlaceFundingAlternativeProvision / Population2To18, NULL) AS OutturnPlaceFundingAlternativeProvision,
        IIF(Population2To18 > 0.0, BudgetTotalHighNeeds / Population2To18, NULL) AS BudgetTotalHighNeeds,
        IIF(Population2To18 > 0.0, BudgetTotalPlaceFunding / Population2To18, NULL) AS BudgetTotalPlaceFunding,
        IIF(Population2To18 > 0.0, BudgetTotalTopUpFundingMaintained / Population2To18, NULL) AS BudgetTotalTopUpFundingMaintained,
        IIF(Population2To18 > 0.0, BudgetTotalTopUpFundingNonMaintained / Population2To18, NULL) AS BudgetTotalTopUpFundingNonMaintained,
        IIF(Population2To18 > 0.0, BudgetTotalSenServices / Population2To18, NULL) AS BudgetTotalSenServices,
        IIF(Population2To18 > 0.0, BudgetTotalAlternativeProvisionServices / Population2To18, NULL) AS BudgetTotalAlternativeProvisionServices,
        IIF(Population2To18 > 0.0, BudgetTotalHospitalServices / Population2To18, NULL) AS BudgetTotalHospitalServices,
        IIF(Population2To18 > 0.0, BudgetTotalOtherHealthServices / Population2To18, NULL) AS BudgetTotalOtherHealthServices,
        IIF(Population2To18 > 0.0, BudgetTopFundingMaintainedEarlyYears / Population2To18, NULL) AS BudgetTopFundingMaintainedEarlyYears,
        IIF(Population2To18 > 0.0, BudgetTopFundingMaintainedPrimary / Population2To18, NULL) AS BudgetTopFundingMaintainedPrimary,
        IIF(Population2To18 > 0.0, BudgetTopFundingMaintainedSecondary / Population2To18, NULL) AS BudgetTopFundingMaintainedSecondary,
        IIF(Population2To18 > 0.0, BudgetTopFundingMaintainedSpecial / Population2To18, NULL) AS BudgetTopFundingMaintainedSpecial,
        IIF(Population2To18 > 0.0, BudgetTopFundingMaintainedAlternativeProvision / Population2To18, NULL) AS BudgetTopFundingMaintainedAlternativeProvision,
        IIF(Population2To18 > 0.0, BudgetTopFundingMaintainedPostSchool / Population2To18, NULL) AS BudgetTopFundingMaintainedPostSchool,
        IIF(Population2To18 > 0.0, BudgetTopFundingMaintainedIncome / Population2To18, NULL) AS BudgetTopFundingMaintainedIncome,
        IIF(Population2To18 > 0.0, BudgetTopFundingNonMaintainedEarlyYears / Population2To18, NULL) AS BudgetTopFundingNonMaintainedEarlyYears,
        IIF(Population2To18 > 0.0, BudgetTopFundingNonMaintainedPrimary / Population2To18, NULL) AS BudgetTopFundingNonMaintainedPrimary,
        IIF(Population2To18 > 0.0, BudgetTopFundingNonMaintainedSecondary / Population2To18, NULL) AS BudgetTopFundingNonMaintainedSecondary,
        IIF(Population2To18 > 0.0, BudgetTopFundingNonMaintainedSpecial / Population2To18, NULL) AS BudgetTopFundingNonMaintainedSpecial,
        IIF(Population2To18 > 0.0, BudgetTopFundingNonMaintainedAlternativeProvision / Population2To18, NULL) AS BudgetTopFundingNonMaintainedAlternativeProvision,
        IIF(Population2To18 > 0.0, BudgetTopFundingNonMaintainedPostSchool / Population2To18, NULL) AS BudgetTopFundingNonMaintainedPostSchool,
        IIF(Population2To18 > 0.0, BudgetTopFundingNonMaintainedIncome / Population2To18, NULL) AS BudgetTopFundingNonMaintainedIncome,
        IIF(Population2To18 > 0.0, BudgetPlaceFundingPrimary / Population2To18, NULL) AS BudgetPlaceFundingPrimary,
        IIF(Population2To18 > 0.0, BudgetPlaceFundingSecondary / Population2To18, NULL) AS BudgetPlaceFundingSecondary,
        IIF(Population2To18 > 0.0, BudgetPlaceFundingSpecial / Population2To18, NULL) AS BudgetPlaceFundingSpecial,
        IIF(Population2To18 > 0.0, BudgetPlaceFundingAlternativeProvision / Population2To18, NULL) AS BudgetPlaceFundingAlternativeProvision
    FROM LocalAuthorityFinancial
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialDefaultActual
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultActual
AS
    SELECT *
    FROM VW_LocalAuthorityFinancial
    WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialDefaultPerPopulation
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultPerPopulation
AS
    SELECT *
    FROM VW_LocalAuthorityFinancialPerPopulation
    WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialDefaultCurrentActual
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultCurrentActual
AS
    SELECT c.*,
        l.Name
    FROM LocalAuthority l
        LEFT JOIN VW_LocalAuthorityFinancialDefaultActual c ON c.LaCode = l.Code
    WHERE c.RunId = (SELECT Value
    FROM Parameters
    WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialDefaultCurrentPerPopulation
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultCurrentPerPopulation
AS
    SELECT c.*,
        l.Name
    FROM LocalAuthority l
        LEFT JOIN VW_LocalAuthorityFinancialDefaultPerPopulation c ON c.LaCode = l.Code
    WHERE c.RunId = (SELECT Value
    FROM Parameters
    WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialDefaultCurrentSpendAsPercentageOfBudget 
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultCurrentSpendAsPercentageOfBudget
AS
    SELECT l.[Code],
        l.[Name],
        c.[OutturnTotalHighNeeds] / c.[BudgetTotalHighNeeds] * 100 AS [Value],
        RANK() OVER (ORDER BY c.[OutturnTotalHighNeeds] / c.[BudgetTotalHighNeeds] DESC) AS [Rank]
    FROM [LocalAuthority] l
        LEFT JOIN [VW_LocalAuthorityFinancialDefaultActual] c ON c.[LaCode] = l.[Code]
    WHERE c.[RunId] = (SELECT [Value]
    FROM [Parameters]
    WHERE [Name] = 'CurrentYear')
GO