DROP VIEW IF EXISTS VW_LocalAuthorityFinancial
GO

CREATE VIEW VW_LocalAuthorityFinancial
AS
    SELECT RunId,
        RunType,
        LaCode,
        Population2To18,
        TotalPupils,
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
        OutturnSENTransportDSG,
        OutturnHometoSchoolTransportPre16,
        OutturnHometoSchoolTransport1618,
        OutturnHometoSchoolTransport1925,
        OutturnEdPsychologyService,
        OutturnSENAdmin,
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
        BudgetPlaceFundingAlternativeProvision,
        BudgetSENTransportDSG,
        BudgetHometoSchoolTransportPre16,
        BudgetHometoSchoolTransport1618,
        BudgetHometoSchoolTransport1925,
        BudgetEdPsychologyService,
        BudgetSENAdmin
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
        TotalPupils,
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
        IIF(Population2To18 > 0.0, OutturnSENTransportDSG / Population2To18, NULL) AS OutturnSENTransportDSG,
        IIF(Population2To18 > 0.0, OutturnHometoSchoolTransportPre16 / Population2To18, NULL) AS OutturnHometoSchoolTransportPre16,
        IIF(Population2To18 > 0.0, OutturnHometoSchoolTransport1618 / Population2To18, NULL) AS OutturnHometoSchoolTransport1618,
        IIF(Population2To18 > 0.0, OutturnHometoSchoolTransport1925 / Population2To18, NULL) AS OutturnHometoSchoolTransport1925,
        IIF(Population2To18 > 0.0, OutturnEdPsychologyService / Population2To18, NULL) AS OutturnEdPsychologyService,
        IIF(Population2To18 > 0.0, OutturnSENAdmin / Population2To18, NULL) AS OutturnSENAdmin,
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
        IIF(Population2To18 > 0.0, BudgetPlaceFundingAlternativeProvision / Population2To18, NULL) AS BudgetPlaceFundingAlternativeProvision,
        IIF(Population2To18 > 0.0, BudgetSENTransportDSG / Population2To18, NULL) AS BudgetSENTransportDSG,
        IIF(Population2To18 > 0.0, BudgetHometoSchoolTransportPre16 / Population2To18, NULL) AS BudgetHometoSchoolTransportPre16,
        IIF(Population2To18 > 0.0, BudgetHometoSchoolTransport1618 / Population2To18, NULL) AS BudgetHometoSchoolTransport1618,
        IIF(Population2To18 > 0.0, BudgetHometoSchoolTransport1925 / Population2To18, NULL) AS BudgetHometoSchoolTransport1925,
        IIF(Population2To18 > 0.0, BudgetEdPsychologyService / Population2To18, NULL) AS BudgetEdPsychologyService,
        IIF(Population2To18 > 0.0, BudgetSENAdmin / Population2To18, NULL) AS BudgetSENAdmin
    FROM LocalAuthorityFinancial
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialPerPupil
    GO

CREATE VIEW VW_LocalAuthorityFinancialPerPupil
AS
SELECT RunId,
       RunType,
       LaCode,
       Population2To18,
       TotalPupils,
       IIF(TotalPupils > 0.0, OutturnTotalHighNeeds / TotalPupils, NULL) AS OutturnTotalHighNeeds,
       IIF(TotalPupils > 0.0, OutturnTotalPlaceFunding / TotalPupils, NULL) AS OutturnTotalPlaceFunding,
       IIF(TotalPupils > 0.0, OutturnTotalTopUpFundingMaintained / TotalPupils, NULL) AS OutturnTotalTopUpFundingMaintained,
       IIF(TotalPupils > 0.0, OutturnTotalTopUpFundingNonMaintained / TotalPupils, NULL) AS OutturnTotalTopUpFundingNonMaintained,
       IIF(TotalPupils > 0.0, OutturnTotalSenServices / TotalPupils, NULL) AS OutturnTotalSenServices,
       IIF(TotalPupils > 0.0, OutturnTotalAlternativeProvisionServices / TotalPupils, NULL) AS OutturnTotalAlternativeProvisionServices,
       IIF(TotalPupils > 0.0, OutturnTotalHospitalServices / TotalPupils, NULL) AS OutturnTotalHospitalServices,
       IIF(TotalPupils > 0.0, OutturnTotalOtherHealthServices / TotalPupils, NULL) AS OutturnTotalOtherHealthServices,
       IIF(TotalPupils > 0.0, OutturnTopFundingMaintainedEarlyYears / TotalPupils, NULL) AS OutturnTopFundingMaintainedEarlyYears,
       IIF(TotalPupils > 0.0, OutturnTopFundingMaintainedPrimary / TotalPupils, NULL) AS OutturnTopFundingMaintainedPrimary,
       IIF(TotalPupils > 0.0, OutturnTopFundingMaintainedSecondary / TotalPupils, NULL) AS OutturnTopFundingMaintainedSecondary,
       IIF(TotalPupils > 0.0, OutturnTopFundingMaintainedSpecial / TotalPupils, NULL) AS OutturnTopFundingMaintainedSpecial,
       IIF(TotalPupils > 0.0, OutturnTopFundingMaintainedAlternativeProvision / TotalPupils, NULL) AS OutturnTopFundingMaintainedAlternativeProvision,
       IIF(TotalPupils > 0.0, OutturnTopFundingMaintainedPostSchool / TotalPupils, NULL) AS OutturnTopFundingMaintainedPostSchool,
       IIF(TotalPupils > 0.0, OutturnTopFundingMaintainedIncome / TotalPupils, NULL) AS OutturnTopFundingMaintainedIncome,
       IIF(TotalPupils > 0.0, OutturnTopFundingNonMaintainedEarlyYears / TotalPupils, NULL) AS OutturnTopFundingNonMaintainedEarlyYears,
       IIF(TotalPupils > 0.0, OutturnTopFundingNonMaintainedPrimary / TotalPupils, NULL) AS OutturnTopFundingNonMaintainedPrimary,
       IIF(TotalPupils > 0.0, OutturnTopFundingNonMaintainedSecondary / TotalPupils, NULL) AS OutturnTopFundingNonMaintainedSecondary,
       IIF(TotalPupils > 0.0, OutturnTopFundingNonMaintainedSpecial / TotalPupils, NULL) AS OutturnTopFundingNonMaintainedSpecial,
       IIF(TotalPupils > 0.0, OutturnTopFundingNonMaintainedAlternativeProvision / TotalPupils, NULL) AS OutturnTopFundingNonMaintainedAlternativeProvision,
       IIF(TotalPupils > 0.0, OutturnTopFundingNonMaintainedPostSchool / TotalPupils, NULL) AS OutturnTopFundingNonMaintainedPostSchool,
       IIF(TotalPupils > 0.0, OutturnTopFundingNonMaintainedIncome / TotalPupils, NULL) AS OutturnTopFundingNonMaintainedIncome,
       IIF(TotalPupils > 0.0, OutturnPlaceFundingPrimary / TotalPupils, NULL) AS OutturnPlaceFundingPrimary,
       IIF(TotalPupils > 0.0, OutturnPlaceFundingSecondary / TotalPupils, NULL) AS OutturnPlaceFundingSecondary,
       IIF(TotalPupils > 0.0, OutturnPlaceFundingSpecial / TotalPupils, NULL) AS OutturnPlaceFundingSpecial,
       IIF(TotalPupils > 0.0, OutturnPlaceFundingAlternativeProvision / TotalPupils, NULL) AS OutturnPlaceFundingAlternativeProvision,
       IIF(TotalPupils > 0.0, OutturnSENTransportDSG / TotalPupils, NULL) AS OutturnSENTransportDSG,
       IIF(TotalPupils > 0.0, OutturnHometoSchoolTransportPre16 / TotalPupils, NULL) AS OutturnHometoSchoolTransportPre16,
       IIF(TotalPupils > 0.0, OutturnHometoSchoolTransport1618 / TotalPupils, NULL) AS OutturnHometoSchoolTransport1618,
       IIF(TotalPupils > 0.0, OutturnHometoSchoolTransport1925 / TotalPupils, NULL) AS OutturnHometoSchoolTransport1925,
       IIF(TotalPupils > 0.0, OutturnEdPsychologyService / TotalPupils, NULL) AS OutturnEdPsychologyService,
       IIF(TotalPupils > 0.0, OutturnSENAdmin / TotalPupils, NULL) AS OutturnSENAdmin,
       IIF(TotalPupils > 0.0, BudgetTotalHighNeeds / TotalPupils, NULL) AS BudgetTotalHighNeeds,
       IIF(TotalPupils > 0.0, BudgetTotalPlaceFunding / TotalPupils, NULL) AS BudgetTotalPlaceFunding,
       IIF(TotalPupils > 0.0, BudgetTotalTopUpFundingMaintained / TotalPupils, NULL) AS BudgetTotalTopUpFundingMaintained,
       IIF(TotalPupils > 0.0, BudgetTotalTopUpFundingNonMaintained / TotalPupils, NULL) AS BudgetTotalTopUpFundingNonMaintained,
       IIF(TotalPupils > 0.0, BudgetTotalSenServices / TotalPupils, NULL) AS BudgetTotalSenServices,
       IIF(TotalPupils > 0.0, BudgetTotalAlternativeProvisionServices / TotalPupils, NULL) AS BudgetTotalAlternativeProvisionServices,
       IIF(TotalPupils > 0.0, BudgetTotalHospitalServices / TotalPupils, NULL) AS BudgetTotalHospitalServices,
       IIF(TotalPupils > 0.0, BudgetTotalOtherHealthServices / TotalPupils, NULL) AS BudgetTotalOtherHealthServices,
       IIF(TotalPupils > 0.0, BudgetTopFundingMaintainedEarlyYears / TotalPupils, NULL) AS BudgetTopFundingMaintainedEarlyYears,
       IIF(TotalPupils > 0.0, BudgetTopFundingMaintainedPrimary / TotalPupils, NULL) AS BudgetTopFundingMaintainedPrimary,
       IIF(TotalPupils > 0.0, BudgetTopFundingMaintainedSecondary / TotalPupils, NULL) AS BudgetTopFundingMaintainedSecondary,
       IIF(TotalPupils > 0.0, BudgetTopFundingMaintainedSpecial / TotalPupils, NULL) AS BudgetTopFundingMaintainedSpecial,
       IIF(TotalPupils > 0.0, BudgetTopFundingMaintainedAlternativeProvision / TotalPupils, NULL) AS BudgetTopFundingMaintainedAlternativeProvision,
       IIF(TotalPupils > 0.0, BudgetTopFundingMaintainedPostSchool / TotalPupils, NULL) AS BudgetTopFundingMaintainedPostSchool,
       IIF(TotalPupils > 0.0, BudgetTopFundingMaintainedIncome / TotalPupils, NULL) AS BudgetTopFundingMaintainedIncome,
       IIF(TotalPupils > 0.0, BudgetTopFundingNonMaintainedEarlyYears / TotalPupils, NULL) AS BudgetTopFundingNonMaintainedEarlyYears,
       IIF(TotalPupils > 0.0, BudgetTopFundingNonMaintainedPrimary / TotalPupils, NULL) AS BudgetTopFundingNonMaintainedPrimary,
       IIF(TotalPupils > 0.0, BudgetTopFundingNonMaintainedSecondary / TotalPupils, NULL) AS BudgetTopFundingNonMaintainedSecondary,
       IIF(TotalPupils > 0.0, BudgetTopFundingNonMaintainedSpecial / TotalPupils, NULL) AS BudgetTopFundingNonMaintainedSpecial,
       IIF(TotalPupils > 0.0, BudgetTopFundingNonMaintainedAlternativeProvision / TotalPupils, NULL) AS BudgetTopFundingNonMaintainedAlternativeProvision,
       IIF(TotalPupils > 0.0, BudgetTopFundingNonMaintainedPostSchool / TotalPupils, NULL) AS BudgetTopFundingNonMaintainedPostSchool,
       IIF(TotalPupils > 0.0, BudgetTopFundingNonMaintainedIncome / TotalPupils, NULL) AS BudgetTopFundingNonMaintainedIncome,
       IIF(TotalPupils > 0.0, BudgetPlaceFundingPrimary / TotalPupils, NULL) AS BudgetPlaceFundingPrimary,
       IIF(TotalPupils > 0.0, BudgetPlaceFundingSecondary / TotalPupils, NULL) AS BudgetPlaceFundingSecondary,
       IIF(TotalPupils > 0.0, BudgetPlaceFundingSpecial / TotalPupils, NULL) AS BudgetPlaceFundingSpecial,
       IIF(TotalPupils > 0.0, BudgetPlaceFundingAlternativeProvision / TotalPupils, NULL) AS BudgetPlaceFundingAlternativeProvision,
       IIF(TotalPupils > 0.0, BudgetSENTransportDSG / TotalPupils, NULL) AS BudgetSENTransportDSG,
       IIF(TotalPupils > 0.0, BudgetHometoSchoolTransportPre16 / TotalPupils, NULL) AS BudgetHometoSchoolTransportPre16,
       IIF(TotalPupils > 0.0, BudgetHometoSchoolTransport1618 / TotalPupils, NULL) AS BudgetHometoSchoolTransport1618,
       IIF(TotalPupils > 0.0, BudgetHometoSchoolTransport1925 / TotalPupils, NULL) AS BudgetHometoSchoolTransport1925,
       IIF(TotalPupils > 0.0, BudgetEdPsychologyService / TotalPupils, NULL) AS BudgetEdPsychologyService,
       IIF(TotalPupils > 0.0, BudgetSENAdmin / TotalPupils, NULL) AS BudgetSENAdmin
    FROM LocalAuthorityFinancial
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialPerEHCP
    GO

CREATE VIEW VW_LocalAuthorityFinancialPerEHCP
AS
SELECT f.RunId,
       f.RunType,
       f.LaCode,
       f.Population2To18,
       f.TotalPupils,
       IIF(n.EHCPTotal > 0.0, f.OutturnTotalHighNeeds / n.EHCPTotal, NULL) AS OutturnTotalHighNeeds,
       IIF(n.EHCPTotal > 0.0, f.OutturnTotalPlaceFunding / n.EHCPTotal, NULL) AS OutturnTotalPlaceFunding,
       IIF(n.EHCPTotal > 0.0, f.OutturnTotalTopUpFundingMaintained / n.EHCPTotal, NULL) AS OutturnTotalTopUpFundingMaintained,
       IIF(n.EHCPTotal > 0.0, f.OutturnTotalTopUpFundingNonMaintained / n.EHCPTotal, NULL) AS OutturnTotalTopUpFundingNonMaintained,
       IIF(n.EHCPTotal > 0.0, f.OutturnTotalSenServices / n.EHCPTotal, NULL) AS OutturnTotalSenServices,
       IIF(n.EHCPTotal > 0.0, f.OutturnTotalAlternativeProvisionServices / n.EHCPTotal, NULL) AS OutturnTotalAlternativeProvisionServices,
       IIF(n.EHCPTotal > 0.0, f.OutturnTotalHospitalServices / n.EHCPTotal, NULL) AS OutturnTotalHospitalServices,
       IIF(n.EHCPTotal > 0.0, f.OutturnTotalOtherHealthServices / n.EHCPTotal, NULL) AS OutturnTotalOtherHealthServices,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingMaintainedEarlyYears / n.EHCPTotal, NULL) AS OutturnTopFundingMaintainedEarlyYears,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingMaintainedPrimary / n.EHCPTotal, NULL) AS OutturnTopFundingMaintainedPrimary,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingMaintainedSecondary / n.EHCPTotal, NULL) AS OutturnTopFundingMaintainedSecondary,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingMaintainedSpecial / n.EHCPTotal, NULL) AS OutturnTopFundingMaintainedSpecial,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingMaintainedAlternativeProvision / n.EHCPTotal, NULL) AS OutturnTopFundingMaintainedAlternativeProvision,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingMaintainedPostSchool / n.EHCPTotal, NULL) AS OutturnTopFundingMaintainedPostSchool,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingMaintainedIncome / n.EHCPTotal, NULL) AS OutturnTopFundingMaintainedIncome,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingNonMaintainedEarlyYears / n.EHCPTotal, NULL) AS OutturnTopFundingNonMaintainedEarlyYears,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingNonMaintainedPrimary / n.EHCPTotal, NULL) AS OutturnTopFundingNonMaintainedPrimary,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingNonMaintainedSecondary / n.EHCPTotal, NULL) AS OutturnTopFundingNonMaintainedSecondary,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingNonMaintainedSpecial / n.EHCPTotal, NULL) AS OutturnTopFundingNonMaintainedSpecial,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingNonMaintainedAlternativeProvision / n.EHCPTotal, NULL) AS OutturnTopFundingNonMaintainedAlternativeProvision,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingNonMaintainedPostSchool / n.EHCPTotal, NULL) AS OutturnTopFundingNonMaintainedPostSchool,
       IIF(n.EHCPTotal > 0.0, f.OutturnTopFundingNonMaintainedIncome / n.EHCPTotal, NULL) AS OutturnTopFundingNonMaintainedIncome,
       IIF(n.EHCPTotal > 0.0, f.OutturnPlaceFundingPrimary / n.EHCPTotal, NULL) AS OutturnPlaceFundingPrimary,
       IIF(n.EHCPTotal > 0.0, f.OutturnPlaceFundingSecondary / n.EHCPTotal, NULL) AS OutturnPlaceFundingSecondary,
       IIF(n.EHCPTotal > 0.0, f.OutturnPlaceFundingSpecial / n.EHCPTotal, NULL) AS OutturnPlaceFundingSpecial,
       IIF(n.EHCPTotal > 0.0, f.OutturnPlaceFundingAlternativeProvision / n.EHCPTotal, NULL) AS OutturnPlaceFundingAlternativeProvision,
       IIF(n.EHCPTotal > 0.0, f.OutturnSENTransportDSG / n.EHCPTotal, NULL) AS OutturnSENTransportDSG,
       IIF(n.EHCPTotal > 0.0, f.OutturnHometoSchoolTransportPre16 / n.EHCPTotal, NULL) AS OutturnHometoSchoolTransportPre16,
       IIF(n.EHCPTotal > 0.0, f.OutturnHometoSchoolTransport1618 / n.EHCPTotal, NULL) AS OutturnHometoSchoolTransport1618,
       IIF(n.EHCPTotal > 0.0, f.OutturnHometoSchoolTransport1925 / n.EHCPTotal, NULL) AS OutturnHometoSchoolTransport1925,
       IIF(n.EHCPTotal > 0.0, f.OutturnEdPsychologyService / n.EHCPTotal, NULL) AS OutturnEdPsychologyService,
       IIF(n.EHCPTotal > 0.0, f.OutturnSENAdmin / n.EHCPTotal, NULL) AS OutturnSENAdmin,
       IIF(n.EHCPTotal > 0.0, f.BudgetTotalHighNeeds / n.EHCPTotal, NULL) AS BudgetTotalHighNeeds,
       IIF(n.EHCPTotal > 0.0, f.BudgetTotalPlaceFunding / n.EHCPTotal, NULL) AS BudgetTotalPlaceFunding,
       IIF(n.EHCPTotal > 0.0, f.BudgetTotalTopUpFundingMaintained / n.EHCPTotal, NULL) AS BudgetTotalTopUpFundingMaintained,
       IIF(n.EHCPTotal > 0.0, f.BudgetTotalTopUpFundingNonMaintained / n.EHCPTotal, NULL) AS BudgetTotalTopUpFundingNonMaintained,
       IIF(n.EHCPTotal > 0.0, f.BudgetTotalSenServices / n.EHCPTotal, NULL) AS BudgetTotalSenServices,
       IIF(n.EHCPTotal > 0.0, f.BudgetTotalAlternativeProvisionServices / n.EHCPTotal, NULL) AS BudgetTotalAlternativeProvisionServices,
       IIF(n.EHCPTotal > 0.0, f.BudgetTotalHospitalServices / n.EHCPTotal, NULL) AS BudgetTotalHospitalServices,
       IIF(n.EHCPTotal > 0.0, f.BudgetTotalOtherHealthServices / n.EHCPTotal, NULL) AS BudgetTotalOtherHealthServices,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingMaintainedEarlyYears / n.EHCPTotal, NULL) AS BudgetTopFundingMaintainedEarlyYears,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingMaintainedPrimary / n.EHCPTotal, NULL) AS BudgetTopFundingMaintainedPrimary,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingMaintainedSecondary / n.EHCPTotal, NULL) AS BudgetTopFundingMaintainedSecondary,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingMaintainedSpecial / n.EHCPTotal, NULL) AS BudgetTopFundingMaintainedSpecial,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingMaintainedAlternativeProvision / n.EHCPTotal, NULL) AS BudgetTopFundingMaintainedAlternativeProvision,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingMaintainedPostSchool / n.EHCPTotal, NULL) AS BudgetTopFundingMaintainedPostSchool,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingMaintainedIncome / n.EHCPTotal, NULL) AS BudgetTopFundingMaintainedIncome,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingNonMaintainedEarlyYears / n.EHCPTotal, NULL) AS BudgetTopFundingNonMaintainedEarlyYears,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingNonMaintainedPrimary / n.EHCPTotal, NULL) AS BudgetTopFundingNonMaintainedPrimary,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingNonMaintainedSecondary / n.EHCPTotal, NULL) AS BudgetTopFundingNonMaintainedSecondary,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingNonMaintainedSpecial / n.EHCPTotal, NULL) AS BudgetTopFundingNonMaintainedSpecial,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingNonMaintainedAlternativeProvision / n.EHCPTotal, NULL) AS BudgetTopFundingNonMaintainedAlternativeProvision,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingNonMaintainedPostSchool / n.EHCPTotal, NULL) AS BudgetTopFundingNonMaintainedPostSchool,
       IIF(n.EHCPTotal > 0.0, f.BudgetTopFundingNonMaintainedIncome / n.EHCPTotal, NULL) AS BudgetTopFundingNonMaintainedIncome,
       IIF(n.EHCPTotal > 0.0, f.BudgetPlaceFundingPrimary / n.EHCPTotal, NULL) AS BudgetPlaceFundingPrimary,
       IIF(n.EHCPTotal > 0.0, f.BudgetPlaceFundingSecondary / n.EHCPTotal, NULL) AS BudgetPlaceFundingSecondary,
       IIF(n.EHCPTotal > 0.0, f.BudgetPlaceFundingSpecial / n.EHCPTotal, NULL) AS BudgetPlaceFundingSpecial,
       IIF(n.EHCPTotal > 0.0, f.BudgetPlaceFundingAlternativeProvision / n.EHCPTotal, NULL) AS BudgetPlaceFundingAlternativeProvision,
       IIF(n.EHCPTotal > 0.0, f.BudgetSENTransportDSG / n.EHCPTotal, NULL) AS BudgetSENTransportDSG,
       IIF(n.EHCPTotal > 0.0, f.BudgetHometoSchoolTransportPre16 / n.EHCPTotal, NULL) AS BudgetHometoSchoolTransportPre16,
       IIF(n.EHCPTotal > 0.0, f.BudgetHometoSchoolTransport1618 / n.EHCPTotal, NULL) AS BudgetHometoSchoolTransport1618,
       IIF(n.EHCPTotal > 0.0, f.BudgetHometoSchoolTransport1925 / n.EHCPTotal, NULL) AS BudgetHometoSchoolTransport1925,
       IIF(n.EHCPTotal > 0.0, f.BudgetEdPsychologyService / n.EHCPTotal, NULL) AS BudgetEdPsychologyService,
       IIF(n.EHCPTotal > 0.0, f.BudgetSENAdmin / n.EHCPTotal, NULL) AS BudgetSENAdmin
FROM LocalAuthorityFinancial f
    LEFT JOIN LocalAuthorityNonFinancial n ON n.LaCode = f.LaCode
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialPerSENSupport
    GO

CREATE VIEW VW_LocalAuthorityFinancialPerSENSupport
AS
SELECT f.RunId,
       f.RunType,
       f.LaCode,
       f.Population2To18,
       f.TotalPupils,
       IIF(n.SENSupport > 0.0, f.OutturnTotalHighNeeds / n.SENSupport, NULL) AS OutturnTotalHighNeeds,
       IIF(n.SENSupport > 0.0, f.OutturnTotalPlaceFunding / n.SENSupport, NULL) AS OutturnTotalPlaceFunding,
       IIF(n.SENSupport > 0.0, f.OutturnTotalTopUpFundingMaintained / n.SENSupport, NULL) AS OutturnTotalTopUpFundingMaintained,
       IIF(n.SENSupport > 0.0, f.OutturnTotalTopUpFundingNonMaintained / n.SENSupport, NULL) AS OutturnTotalTopUpFundingNonMaintained,
       IIF(n.SENSupport > 0.0, f.OutturnTotalSenServices / n.SENSupport, NULL) AS OutturnTotalSenServices,
       IIF(n.SENSupport > 0.0, f.OutturnTotalAlternativeProvisionServices / n.SENSupport, NULL) AS OutturnTotalAlternativeProvisionServices,
       IIF(n.SENSupport > 0.0, f.OutturnTotalHospitalServices / n.SENSupport, NULL) AS OutturnTotalHospitalServices,
       IIF(n.SENSupport > 0.0, f.OutturnTotalOtherHealthServices / n.SENSupport, NULL) AS OutturnTotalOtherHealthServices,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingMaintainedEarlyYears / n.SENSupport, NULL) AS OutturnTopFundingMaintainedEarlyYears,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingMaintainedPrimary / n.SENSupport, NULL) AS OutturnTopFundingMaintainedPrimary,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingMaintainedSecondary / n.SENSupport, NULL) AS OutturnTopFundingMaintainedSecondary,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingMaintainedSpecial / n.SENSupport, NULL) AS OutturnTopFundingMaintainedSpecial,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingMaintainedAlternativeProvision / n.SENSupport, NULL) AS OutturnTopFundingMaintainedAlternativeProvision,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingMaintainedPostSchool / n.SENSupport, NULL) AS OutturnTopFundingMaintainedPostSchool,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingMaintainedIncome / n.SENSupport, NULL) AS OutturnTopFundingMaintainedIncome,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingNonMaintainedEarlyYears / n.SENSupport, NULL) AS OutturnTopFundingNonMaintainedEarlyYears,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingNonMaintainedPrimary / n.SENSupport, NULL) AS OutturnTopFundingNonMaintainedPrimary,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingNonMaintainedSecondary / n.SENSupport, NULL) AS OutturnTopFundingNonMaintainedSecondary,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingNonMaintainedSpecial / n.SENSupport, NULL) AS OutturnTopFundingNonMaintainedSpecial,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingNonMaintainedAlternativeProvision / n.SENSupport, NULL) AS OutturnTopFundingNonMaintainedAlternativeProvision,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingNonMaintainedPostSchool / n.SENSupport, NULL) AS OutturnTopFundingNonMaintainedPostSchool,
       IIF(n.SENSupport > 0.0, f.OutturnTopFundingNonMaintainedIncome / n.SENSupport, NULL) AS OutturnTopFundingNonMaintainedIncome,
       IIF(n.SENSupport > 0.0, f.OutturnPlaceFundingPrimary / n.SENSupport, NULL) AS OutturnPlaceFundingPrimary,
       IIF(n.SENSupport > 0.0, f.OutturnPlaceFundingSecondary / n.SENSupport, NULL) AS OutturnPlaceFundingSecondary,
       IIF(n.SENSupport > 0.0, f.OutturnPlaceFundingSpecial / n.SENSupport, NULL) AS OutturnPlaceFundingSpecial,
       IIF(n.SENSupport > 0.0, f.OutturnPlaceFundingAlternativeProvision / n.SENSupport, NULL) AS OutturnPlaceFundingAlternativeProvision,
       IIF(n.SENSupport > 0.0, f.OutturnSENTransportDSG / n.SENSupport, NULL) AS OutturnSENTransportDSG,
       IIF(n.SENSupport > 0.0, f.OutturnHometoSchoolTransportPre16 / n.SENSupport, NULL) AS OutturnHometoSchoolTransportPre16,
       IIF(n.SENSupport > 0.0, f.OutturnHometoSchoolTransport1618 / n.SENSupport, NULL) AS OutturnHometoSchoolTransport1618,
       IIF(n.SENSupport > 0.0, f.OutturnHometoSchoolTransport1925 / n.SENSupport, NULL) AS OutturnHometoSchoolTransport1925,
       IIF(n.SENSupport > 0.0, f.OutturnEdPsychologyService / n.SENSupport, NULL) AS OutturnEdPsychologyService,
       IIF(n.SENSupport > 0.0, f.OutturnSENAdmin / n.SENSupport, NULL) AS OutturnSENAdmin,
       IIF(n.SENSupport > 0.0, f.BudgetTotalHighNeeds / n.SENSupport, NULL) AS BudgetTotalHighNeeds,
       IIF(n.SENSupport > 0.0, f.BudgetTotalPlaceFunding / n.SENSupport, NULL) AS BudgetTotalPlaceFunding,
       IIF(n.SENSupport > 0.0, f.BudgetTotalTopUpFundingMaintained / n.SENSupport, NULL) AS BudgetTotalTopUpFundingMaintained,
       IIF(n.SENSupport > 0.0, f.BudgetTotalTopUpFundingNonMaintained / n.SENSupport, NULL) AS BudgetTotalTopUpFundingNonMaintained,
       IIF(n.SENSupport > 0.0, f.BudgetTotalSenServices / n.SENSupport, NULL) AS BudgetTotalSenServices,
       IIF(n.SENSupport > 0.0, f.BudgetTotalAlternativeProvisionServices / n.SENSupport, NULL) AS BudgetTotalAlternativeProvisionServices,
       IIF(n.SENSupport > 0.0, f.BudgetTotalHospitalServices / n.SENSupport, NULL) AS BudgetTotalHospitalServices,
       IIF(n.SENSupport > 0.0, f.BudgetTotalOtherHealthServices / n.SENSupport, NULL) AS BudgetTotalOtherHealthServices,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingMaintainedEarlyYears / n.SENSupport, NULL) AS BudgetTopFundingMaintainedEarlyYears,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingMaintainedPrimary / n.SENSupport, NULL) AS BudgetTopFundingMaintainedPrimary,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingMaintainedSecondary / n.SENSupport, NULL) AS BudgetTopFundingMaintainedSecondary,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingMaintainedSpecial / n.SENSupport, NULL) AS BudgetTopFundingMaintainedSpecial,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingMaintainedAlternativeProvision / n.SENSupport, NULL) AS BudgetTopFundingMaintainedAlternativeProvision,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingMaintainedPostSchool / n.SENSupport, NULL) AS BudgetTopFundingMaintainedPostSchool,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingMaintainedIncome / n.SENSupport, NULL) AS BudgetTopFundingMaintainedIncome,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingNonMaintainedEarlyYears / n.SENSupport, NULL) AS BudgetTopFundingNonMaintainedEarlyYears,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingNonMaintainedPrimary / n.SENSupport, NULL) AS BudgetTopFundingNonMaintainedPrimary,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingNonMaintainedSecondary / n.SENSupport, NULL) AS BudgetTopFundingNonMaintainedSecondary,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingNonMaintainedSpecial / n.SENSupport, NULL) AS BudgetTopFundingNonMaintainedSpecial,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingNonMaintainedAlternativeProvision / n.SENSupport, NULL) AS BudgetTopFundingNonMaintainedAlternativeProvision,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingNonMaintainedPostSchool / n.SENSupport, NULL) AS BudgetTopFundingNonMaintainedPostSchool,
       IIF(n.SENSupport > 0.0, f.BudgetTopFundingNonMaintainedIncome / n.SENSupport, NULL) AS BudgetTopFundingNonMaintainedIncome,
       IIF(n.SENSupport > 0.0, f.BudgetPlaceFundingPrimary / n.SENSupport, NULL) AS BudgetPlaceFundingPrimary,
       IIF(n.SENSupport > 0.0, f.BudgetPlaceFundingSecondary / n.SENSupport, NULL) AS BudgetPlaceFundingSecondary,
       IIF(n.SENSupport > 0.0, f.BudgetPlaceFundingSpecial / n.SENSupport, NULL) AS BudgetPlaceFundingSpecial,
       IIF(n.SENSupport > 0.0, f.BudgetPlaceFundingAlternativeProvision / n.SENSupport, NULL) AS BudgetPlaceFundingAlternativeProvision,
       IIF(n.SENSupport > 0.0, f.BudgetSENTransportDSG / n.SENSupport, NULL) AS BudgetSENTransportDSG,
       IIF(n.SENSupport > 0.0, f.BudgetHometoSchoolTransportPre16 / n.SENSupport, NULL) AS BudgetHometoSchoolTransportPre16,
       IIF(n.SENSupport > 0.0, f.BudgetHometoSchoolTransport1618 / n.SENSupport, NULL) AS BudgetHometoSchoolTransport1618,
       IIF(n.SENSupport > 0.0, f.BudgetHometoSchoolTransport1925 / n.SENSupport, NULL) AS BudgetHometoSchoolTransport1925,
       IIF(n.SENSupport > 0.0, f.BudgetEdPsychologyService / n.SENSupport, NULL) AS BudgetEdPsychologyService,
       IIF(n.SENSupport > 0.0, f.BudgetSENAdmin / n.SENSupport, NULL) AS BudgetSENAdmin
FROM LocalAuthorityFinancial f
     LEFT JOIN LocalAuthorityNonFinancial n ON n.LaCode = f.LaCode
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialPerSENSupportAndEHCP
    GO
GO

CREATE VIEW VW_LocalAuthorityFinancialPerSENSupportAndEHCP
AS
SELECT f.RunId,
       f.RunType,
       f.LaCode,
       f.Population2To18,
       f.TotalPupils,
       IIF(x.TotalSupport > 0.0, f.OutturnTotalHighNeeds / x.TotalSupport, NULL) AS OutturnTotalHighNeeds,
       IIF(x.TotalSupport > 0.0, f.OutturnTotalPlaceFunding / x.TotalSupport, NULL) AS OutturnTotalPlaceFunding,
       IIF(x.TotalSupport > 0.0, f.OutturnTotalTopUpFundingMaintained / x.TotalSupport, NULL) AS OutturnTotalTopUpFundingMaintained,
       IIF(x.TotalSupport > 0.0, f.OutturnTotalTopUpFundingNonMaintained / x.TotalSupport, NULL) AS OutturnTotalTopUpFundingNonMaintained,
       IIF(x.TotalSupport > 0.0, f.OutturnTotalSenServices / x.TotalSupport, NULL) AS OutturnTotalSenServices,
       IIF(x.TotalSupport > 0.0, f.OutturnTotalAlternativeProvisionServices / x.TotalSupport, NULL) AS OutturnTotalAlternativeProvisionServices,
       IIF(x.TotalSupport > 0.0, f.OutturnTotalHospitalServices / x.TotalSupport, NULL) AS OutturnTotalHospitalServices,
       IIF(x.TotalSupport > 0.0, f.OutturnTotalOtherHealthServices / x.TotalSupport, NULL) AS OutturnTotalOtherHealthServices,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingMaintainedEarlyYears / x.TotalSupport, NULL) AS OutturnTopFundingMaintainedEarlyYears,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingMaintainedPrimary / x.TotalSupport, NULL) AS OutturnTopFundingMaintainedPrimary,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingMaintainedSecondary / x.TotalSupport, NULL) AS OutturnTopFundingMaintainedSecondary,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingMaintainedSpecial / x.TotalSupport, NULL) AS OutturnTopFundingMaintainedSpecial,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingMaintainedAlternativeProvision / x.TotalSupport, NULL) AS OutturnTopFundingMaintainedAlternativeProvision,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingMaintainedPostSchool / x.TotalSupport, NULL) AS OutturnTopFundingMaintainedPostSchool,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingMaintainedIncome / x.TotalSupport, NULL) AS OutturnTopFundingMaintainedIncome,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingNonMaintainedEarlyYears / x.TotalSupport, NULL) AS OutturnTopFundingNonMaintainedEarlyYears,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingNonMaintainedPrimary / x.TotalSupport, NULL) AS OutturnTopFundingNonMaintainedPrimary,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingNonMaintainedSecondary / x.TotalSupport, NULL) AS OutturnTopFundingNonMaintainedSecondary,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingNonMaintainedSpecial / x.TotalSupport, NULL) AS OutturnTopFundingNonMaintainedSpecial,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingNonMaintainedAlternativeProvision / x.TotalSupport, NULL) AS OutturnTopFundingNonMaintainedAlternativeProvision,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingNonMaintainedPostSchool / x.TotalSupport, NULL) AS OutturnTopFundingNonMaintainedPostSchool,
       IIF(x.TotalSupport > 0.0, f.OutturnTopFundingNonMaintainedIncome / x.TotalSupport, NULL) AS OutturnTopFundingNonMaintainedIncome,
       IIF(x.TotalSupport > 0.0, f.OutturnPlaceFundingPrimary / x.TotalSupport, NULL) AS OutturnPlaceFundingPrimary,
       IIF(x.TotalSupport > 0.0, f.OutturnPlaceFundingSecondary / x.TotalSupport, NULL) AS OutturnPlaceFundingSecondary,
       IIF(x.TotalSupport > 0.0, f.OutturnPlaceFundingSpecial / x.TotalSupport, NULL) AS OutturnPlaceFundingSpecial,
       IIF(x.TotalSupport > 0.0, f.OutturnPlaceFundingAlternativeProvision / x.TotalSupport, NULL) AS OutturnPlaceFundingAlternativeProvision,
       IIF(x.TotalSupport > 0.0, f.OutturnSENTransportDSG / x.TotalSupport, NULL) AS OutturnSENTransportDSG,
       IIF(x.TotalSupport > 0.0, f.OutturnHometoSchoolTransportPre16 / x.TotalSupport, NULL) AS OutturnHometoSchoolTransportPre16,
       IIF(x.TotalSupport > 0.0, f.OutturnHometoSchoolTransport1618 / x.TotalSupport, NULL) AS OutturnHometoSchoolTransport1618,
       IIF(x.TotalSupport > 0.0, f.OutturnHometoSchoolTransport1925 / x.TotalSupport, NULL) AS OutturnHometoSchoolTransport1925,
       IIF(x.TotalSupport > 0.0, f.OutturnEdPsychologyService / x.TotalSupport, NULL) AS OutturnEdPsychologyService,
       IIF(x.TotalSupport > 0.0, f.OutturnSENAdmin / x.TotalSupport, NULL) AS OutturnSENAdmin,
       IIF(x.TotalSupport > 0.0, f.BudgetTotalHighNeeds / x.TotalSupport, NULL) AS BudgetTotalHighNeeds,
       IIF(x.TotalSupport > 0.0, f.BudgetTotalPlaceFunding / x.TotalSupport, NULL) AS BudgetTotalPlaceFunding,
       IIF(x.TotalSupport > 0.0, f.BudgetTotalTopUpFundingMaintained / x.TotalSupport, NULL) AS BudgetTotalTopUpFundingMaintained,
       IIF(x.TotalSupport > 0.0, f.BudgetTotalTopUpFundingNonMaintained / x.TotalSupport, NULL) AS BudgetTotalTopUpFundingNonMaintained,
       IIF(x.TotalSupport > 0.0, f.BudgetTotalSenServices / x.TotalSupport, NULL) AS BudgetTotalSenServices,
       IIF(x.TotalSupport > 0.0, f.BudgetTotalAlternativeProvisionServices / x.TotalSupport, NULL) AS BudgetTotalAlternativeProvisionServices,
       IIF(x.TotalSupport > 0.0, f.BudgetTotalHospitalServices / x.TotalSupport, NULL) AS BudgetTotalHospitalServices,
       IIF(x.TotalSupport > 0.0, f.BudgetTotalOtherHealthServices / x.TotalSupport, NULL) AS BudgetTotalOtherHealthServices,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingMaintainedEarlyYears / x.TotalSupport, NULL) AS BudgetTopFundingMaintainedEarlyYears,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingMaintainedPrimary / x.TotalSupport, NULL) AS BudgetTopFundingMaintainedPrimary,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingMaintainedSecondary / x.TotalSupport, NULL) AS BudgetTopFundingMaintainedSecondary,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingMaintainedSpecial / x.TotalSupport, NULL) AS BudgetTopFundingMaintainedSpecial,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingMaintainedAlternativeProvision / x.TotalSupport, NULL) AS BudgetTopFundingMaintainedAlternativeProvision,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingMaintainedPostSchool / x.TotalSupport, NULL) AS BudgetTopFundingMaintainedPostSchool,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingMaintainedIncome / x.TotalSupport, NULL) AS BudgetTopFundingMaintainedIncome,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingNonMaintainedEarlyYears / x.TotalSupport, NULL) AS BudgetTopFundingNonMaintainedEarlyYears,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingNonMaintainedPrimary / x.TotalSupport, NULL) AS BudgetTopFundingNonMaintainedPrimary,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingNonMaintainedSecondary / x.TotalSupport, NULL) AS BudgetTopFundingNonMaintainedSecondary,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingNonMaintainedSpecial / x.TotalSupport, NULL) AS BudgetTopFundingNonMaintainedSpecial,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingNonMaintainedAlternativeProvision / x.TotalSupport, NULL) AS BudgetTopFundingNonMaintainedAlternativeProvision,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingNonMaintainedPostSchool / x.TotalSupport, NULL) AS BudgetTopFundingNonMaintainedPostSchool,
       IIF(x.TotalSupport > 0.0, f.BudgetTopFundingNonMaintainedIncome / x.TotalSupport, NULL) AS BudgetTopFundingNonMaintainedIncome,
       IIF(x.TotalSupport > 0.0, f.BudgetPlaceFundingPrimary / x.TotalSupport, NULL) AS BudgetPlaceFundingPrimary,
       IIF(x.TotalSupport > 0.0, f.BudgetPlaceFundingSecondary / x.TotalSupport, NULL) AS BudgetPlaceFundingSecondary,
       IIF(x.TotalSupport > 0.0, f.BudgetPlaceFundingSpecial / x.TotalSupport, NULL) AS BudgetPlaceFundingSpecial,
       IIF(x.TotalSupport > 0.0, f.BudgetPlaceFundingAlternativeProvision / x.TotalSupport, NULL) AS BudgetPlaceFundingAlternativeProvision,
       IIF(x.TotalSupport > 0.0, f.BudgetSENTransportDSG / x.TotalSupport, NULL) AS BudgetSENTransportDSG,
       IIF(x.TotalSupport > 0.0, f.BudgetHometoSchoolTransportPre16 / x.TotalSupport, NULL) AS BudgetHometoSchoolTransportPre16,
       IIF(x.TotalSupport > 0.0, f.BudgetHometoSchoolTransport1618 / x.TotalSupport, NULL) AS BudgetHometoSchoolTransport1618,
       IIF(x.TotalSupport > 0.0, f.BudgetHometoSchoolTransport1925 / x.TotalSupport, NULL) AS BudgetHometoSchoolTransport1925,
       IIF(x.TotalSupport > 0.0, f.BudgetEdPsychologyService / x.TotalSupport, NULL) AS BudgetEdPsychologyService,
       IIF(x.TotalSupport > 0.0, f.BudgetSENAdmin / x.TotalSupport, NULL) AS BudgetSENAdmin
FROM LocalAuthorityFinancial f
    LEFT JOIN LocalAuthorityNonFinancial n ON n.LaCode = f.LaCode
    CROSS APPLY (
        SELECT n.EHCPTotal + n.SENSupport AS TotalSupport
) x
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

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialDefaultPerPupil
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultPerPupil
AS
    SELECT *
    FROM VW_LocalAuthorityFinancialPerPupil
    WHERE RunType = 'default'
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultPerEHCP
AS
    SELECT *
    FROM VW_LocalAuthorityFinancialPerEHCP
    WHERE RunType = 'default'
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultPerSENSupport
AS
    SELECT *
    FROM VW_LocalAuthorityFinancialPerSENSupport
    WHERE RunType = 'default'
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultPerSENSupportAndEHCP
AS
    SELECT *
    FROM VW_LocalAuthorityFinancialPerSENSupportAndEHCP
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

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialDefaultCurrentPerPupil
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultCurrentPerPupil
AS
    SELECT c.*,
        l.Name
    FROM LocalAuthority l
        LEFT JOIN VW_LocalAuthorityFinancialDefaultPerPupil c ON c.LaCode = l.Code
    WHERE c.RunId = (SELECT Value
    FROM Parameters
    WHERE Name = 'CurrentYear')
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultCurrentPerEHCP
AS
    SELECT c.*,
        l.Name
    FROM LocalAuthority l
        LEFT JOIN VW_LocalAuthorityFinancialDefaultPerEHCP c ON c.LaCode = l.Code
    WHERE c.RunId = (SELECT Value
    FROM Parameters
    WHERE Name = 'CurrentYear')
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultCurrentPerSENSupport
AS
    SELECT c.*,
        l.Name
    FROM LocalAuthority l
        LEFT JOIN VW_LocalAuthorityFinancialDefaultPerSENSupport c ON c.LaCode = l.Code
    WHERE c.RunId = (SELECT Value
    FROM Parameters
    WHERE Name = 'CurrentYear')
GO

CREATE VIEW VW_LocalAuthorityFinancialDefaultCurrentPerSENSupportAndEHCP
AS
    SELECT c.*,
        l.Name
    FROM LocalAuthority l
        LEFT JOIN VW_LocalAuthorityFinancialDefaultPerSENSupportAndEHCP c ON c.LaCode = l.Code
    WHERE c.RunId = (SELECT Value
    FROM Parameters
    WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialDefaultCurrentSpendAsPercentageOfBudget 
GO

DROP VIEW IF EXISTS VW_LocalAuthorityFinancialHeadlineStatistics
GO

CREATE VIEW VW_LocalAuthorityFinancialHeadlineStatistics
AS
    WITH ctePreviousPeriod
    AS (
        SELECT LaCode,
            OutturnDSGCarriedForward AS OutturnDSGCarriedForwardPreviousPeriod
        FROM LocalAuthorityFinancial
        WHERE RunId = (SELECT (Value - 1)
            FROM Parameters
            WHERE Name = 'LatestS251Year')
        )
    SELECT laf.LaCode,
        laf.DSGHighNeedsAllocation,
        laf.OutturnTotalHighNeeds,
        laf.OutturnDSGCarriedForward,
        cte.OutturnDSGCarriedForwardPreviousPeriod
    FROM LocalAuthorityFinancial AS laf
    LEFT JOIN ctePreviousPeriod AS cte ON laf.LaCode = cte.LaCode
    WHERE laf.RunId = (SELECT Value
                   FROM Parameters
                   WHERE Name = 'LatestS251Year')
GO