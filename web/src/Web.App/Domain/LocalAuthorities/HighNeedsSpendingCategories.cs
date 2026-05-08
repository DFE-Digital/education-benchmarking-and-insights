// ReSharper disable ConvertToExtensionBlock
namespace Web.App.Domain.LocalAuthorities;

public static class HighNeedsSpendingCategories
{
    public enum CategoryGroup
    {
        HighNeedsAmount,
        PlaceFunding,
        TopUpFundingMaintained,
        TopUpFundingNonMaintained,
        SenTransport,
        CentralSenServices
    }

    public enum SubCategoryFilter
    {
        TotalPlaceFunding = 0,
        TotalTopUpFundingMaintained = 1,
        TotalTopUpFundingNonMaintained = 2,
        TotalSenServices = 3,
        TotalAlternativeProvisionServices = 4,
        TotalHospitalServices = 5,
        TotalOtherHealthServices = 6,
        PlaceFundingPrimary = 7,
        PlaceFundingSecondary = 8,
        PlaceFundingSpecial = 9,
        PlaceFundingAlternativeProvision = 10,
        TopFundingMaintainedEarlyYears = 11,
        TopFundingMaintainedPrimary = 12,
        TopFundingMaintainedSecondary = 13,
        TopFundingMaintainedSpecial = 14,
        TopFundingMaintainedAlternativeProvision = 15,
        TopFundingMaintainedPostSchool = 16,
        TopFundingMaintainedIncome = 17,
        TopFundingNonMaintainedEarlyYears = 18,
        TopFundingNonMaintainedPrimary = 19,
        TopFundingNonMaintainedSecondary = 20,
        TopFundingNonMaintainedSpecial = 21,
        TopFundingNonMaintainedAlternativeProvision = 22,
        TopFundingNonMaintainedPostSchool = 23,
        TopFundingNonMaintainedIncome = 24,
        HometoSchoolTransportPre16 = 25,
        HometoSchoolTransport1618 = 26,
        HometoSchoolTransport1925 = 27,
        SenTransportDsg = 28,
        EdPsychologyService = 29,
        SenAdmin = 30
    }

    public static readonly SubCategoryFilter[] All =
    [
        SubCategoryFilter.TotalPlaceFunding,
        SubCategoryFilter.TotalTopUpFundingMaintained,
        SubCategoryFilter.TotalTopUpFundingNonMaintained,
        SubCategoryFilter.TotalSenServices,
        SubCategoryFilter.TotalAlternativeProvisionServices,
        SubCategoryFilter.TotalHospitalServices,
        SubCategoryFilter.TotalOtherHealthServices,
        SubCategoryFilter.PlaceFundingPrimary,
        SubCategoryFilter.PlaceFundingSecondary,
        SubCategoryFilter.PlaceFundingSpecial,
        SubCategoryFilter.PlaceFundingAlternativeProvision,
        SubCategoryFilter.TopFundingMaintainedEarlyYears,
        SubCategoryFilter.TopFundingMaintainedPrimary,
        SubCategoryFilter.TopFundingMaintainedSecondary,
        SubCategoryFilter.TopFundingMaintainedSpecial,
        SubCategoryFilter.TopFundingMaintainedAlternativeProvision,
        SubCategoryFilter.TopFundingMaintainedPostSchool,
        SubCategoryFilter.TopFundingMaintainedIncome,
        SubCategoryFilter.TopFundingNonMaintainedEarlyYears,
        SubCategoryFilter.TopFundingNonMaintainedPrimary,
        SubCategoryFilter.TopFundingNonMaintainedSecondary,
        SubCategoryFilter.TopFundingNonMaintainedSpecial,
        SubCategoryFilter.TopFundingNonMaintainedAlternativeProvision,
        SubCategoryFilter.TopFundingNonMaintainedPostSchool,
        SubCategoryFilter.TopFundingNonMaintainedIncome,
        SubCategoryFilter.HometoSchoolTransportPre16,
        SubCategoryFilter.HometoSchoolTransport1618,
        SubCategoryFilter.HometoSchoolTransport1925,
        SubCategoryFilter.SenTransportDsg,
        SubCategoryFilter.EdPsychologyService,
        SubCategoryFilter.SenAdmin
    ];

    public static readonly Dictionary<CategoryGroup, SubCategoryFilter[]> Groups = new()
    {
        [CategoryGroup.HighNeedsAmount] =
        [
            SubCategoryFilter.TotalPlaceFunding,
            SubCategoryFilter.TotalTopUpFundingMaintained,
            SubCategoryFilter.TotalTopUpFundingNonMaintained,
            SubCategoryFilter.TotalSenServices,
            SubCategoryFilter.TotalAlternativeProvisionServices,
            SubCategoryFilter.TotalHospitalServices,
            SubCategoryFilter.TotalOtherHealthServices
        ],
        [CategoryGroup.PlaceFunding] =
        [
            SubCategoryFilter.PlaceFundingPrimary,
            SubCategoryFilter.PlaceFundingSecondary,
            SubCategoryFilter.PlaceFundingSpecial,
            SubCategoryFilter.PlaceFundingAlternativeProvision,
        ],
        [CategoryGroup.TopUpFundingMaintained] =
        [
            SubCategoryFilter.TopFundingMaintainedEarlyYears,
            SubCategoryFilter.TopFundingMaintainedPrimary,
            SubCategoryFilter.TopFundingMaintainedSecondary,
            SubCategoryFilter.TopFundingMaintainedSpecial,
            SubCategoryFilter.TopFundingMaintainedAlternativeProvision,
            SubCategoryFilter.TopFundingMaintainedPostSchool,
            SubCategoryFilter.TopFundingMaintainedIncome,
        ],
        [CategoryGroup.TopUpFundingNonMaintained] =
        [
            SubCategoryFilter.TopFundingNonMaintainedEarlyYears,
            SubCategoryFilter.TopFundingNonMaintainedPrimary,
            SubCategoryFilter.TopFundingNonMaintainedSecondary,
            SubCategoryFilter.TopFundingNonMaintainedSpecial,
            SubCategoryFilter.TopFundingNonMaintainedAlternativeProvision,
            SubCategoryFilter.TopFundingNonMaintainedPostSchool,
            SubCategoryFilter.TopFundingNonMaintainedIncome,
        ],
        [CategoryGroup.SenTransport] =
        [
            SubCategoryFilter.HometoSchoolTransportPre16,
            SubCategoryFilter.HometoSchoolTransport1618,
            SubCategoryFilter.HometoSchoolTransport1925,
            SubCategoryFilter.SenTransportDsg,
        ],
        [CategoryGroup.CentralSenServices] =
        [
            SubCategoryFilter.EdPsychologyService,
            SubCategoryFilter.SenAdmin
        ]
    };

    public static string GetCategoryGroupDescription(this CategoryGroup group) => group switch
    {
        CategoryGroup.HighNeedsAmount => "High needs amount",
        CategoryGroup.PlaceFunding => "Place funding",
        CategoryGroup.TopUpFundingMaintained => "Top up funding (maintained)",
        CategoryGroup.TopUpFundingNonMaintained => "Top up funding (non-maintained)",
        CategoryGroup.SenTransport => "SEN transport",
        CategoryGroup.CentralSenServices => "Central SEN services",
        _ => ""
    };

    public static string GetSubCategoryFilterDescription(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.TotalPlaceFunding => "Total place funding for special schools and AP/PRUs",
        SubCategoryFilter.TotalTopUpFundingMaintained => "Top up funding (maintained schools, academies, free schools and colleges)",
        SubCategoryFilter.TotalTopUpFundingNonMaintained => "Top up funding (non-maintained and independent schools and colleges)",
        SubCategoryFilter.TotalSenServices => "SEN support and inclusion services",
        SubCategoryFilter.TotalAlternativeProvisionServices => "Alternative provision services",
        SubCategoryFilter.TotalHospitalServices => "Hospital education services",
        SubCategoryFilter.TotalOtherHealthServices => "Therapies and other health related services",
        SubCategoryFilter.PlaceFundingPrimary => "Primary",
        SubCategoryFilter.PlaceFundingSecondary => "Secondary",
        SubCategoryFilter.PlaceFundingSpecial => "Special",
        SubCategoryFilter.PlaceFundingAlternativeProvision => "PRU/AP",
        SubCategoryFilter.TopFundingMaintainedEarlyYears => "Early Years",
        SubCategoryFilter.TopFundingMaintainedPrimary => "Primary",
        SubCategoryFilter.TopFundingMaintainedSecondary => "Secondary",
        SubCategoryFilter.TopFundingMaintainedSpecial => "Special",
        SubCategoryFilter.TopFundingMaintainedAlternativeProvision => "Alternative provision",
        SubCategoryFilter.TopFundingMaintainedPostSchool => "Post-school",
        SubCategoryFilter.TopFundingMaintainedIncome => "Income",
        SubCategoryFilter.TopFundingNonMaintainedEarlyYears => "Early Years",
        SubCategoryFilter.TopFundingNonMaintainedPrimary => "Primary",
        SubCategoryFilter.TopFundingNonMaintainedSecondary => "Secondary",
        SubCategoryFilter.TopFundingNonMaintainedSpecial => "Special",
        SubCategoryFilter.TopFundingNonMaintainedAlternativeProvision => "Alternative provision",
        SubCategoryFilter.TopFundingNonMaintainedPostSchool => "Post-school",
        SubCategoryFilter.TopFundingNonMaintainedIncome => "Income",
        SubCategoryFilter.HometoSchoolTransportPre16 => "Home to school transport (pre-16)",
        SubCategoryFilter.HometoSchoolTransport1618 => "Home to school transport (16-18)",
        SubCategoryFilter.HometoSchoolTransport1925 => "Home to school transport (16-18)",
        SubCategoryFilter.SenTransportDsg => "SEN transport (LA-managed)",
        SubCategoryFilter.EdPsychologyService => "Educational psychology service",
        SubCategoryFilter.SenAdmin => "SEN admin",
        _ => ""
    };

    public static string GetHeading(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.TotalPlaceFunding => "Total place funding for special schools and AP/PRUs",
        SubCategoryFilter.TotalTopUpFundingMaintained => "Top up funding (maintained schools, academies, free schools and colleges)",
        SubCategoryFilter.TotalTopUpFundingNonMaintained => "Top up funding (non-maintained schools, academies, free schools and colleges)",
        SubCategoryFilter.TotalSenServices => "SEN support and inclusion services",
        SubCategoryFilter.TotalAlternativeProvisionServices => "Alternative provision services",
        SubCategoryFilter.TotalHospitalServices => "Hospital education services",
        SubCategoryFilter.TotalOtherHealthServices => "Therapies and other health related services",
        SubCategoryFilter.PlaceFundingPrimary => "Place funding",
        SubCategoryFilter.PlaceFundingSecondary => "Secondary place funding",
        SubCategoryFilter.PlaceFundingSpecial => "Special place funding",
        SubCategoryFilter.PlaceFundingAlternativeProvision => "PRU and alternative provision place funding",
        SubCategoryFilter.TopFundingMaintainedEarlyYears => "Early years top up funding (maintained)",
        SubCategoryFilter.TopFundingMaintainedPrimary => "Primary top up funding (maintained)",
        SubCategoryFilter.TopFundingMaintainedSecondary => "Secondary top up funding (maintained)",
        SubCategoryFilter.TopFundingMaintainedSpecial => "Special top up funding (maintained)",
        SubCategoryFilter.TopFundingMaintainedAlternativeProvision => "Alternative provision top up funding (maintained)",
        SubCategoryFilter.TopFundingMaintainedPostSchool => "Post-school top up funding (maintained)",
        SubCategoryFilter.TopFundingMaintainedIncome => "Top up funding income (maintained)",
        SubCategoryFilter.TopFundingNonMaintainedEarlyYears => "Early years top up funding (non-maintained)",
        SubCategoryFilter.TopFundingNonMaintainedPrimary => "Primary top up funding (non-maintained)",
        SubCategoryFilter.TopFundingNonMaintainedSecondary => "Secondary top up funding (non-maintained)",
        SubCategoryFilter.TopFundingNonMaintainedSpecial => "Special top up funding (non-maintained)",
        SubCategoryFilter.TopFundingNonMaintainedAlternativeProvision => "Alternative provision top up funding (non-maintained)",
        SubCategoryFilter.TopFundingNonMaintainedPostSchool => "Post-school top up funding (non-maintained)",
        SubCategoryFilter.TopFundingNonMaintainedIncome => "Top up funding income (non-maintained)",
        SubCategoryFilter.HometoSchoolTransportPre16 => "Home to school transport (pre-16)",
        SubCategoryFilter.HometoSchoolTransport1618 => "Home to post-16 provision (16-18)",
        SubCategoryFilter.HometoSchoolTransport1925 => "Home to post-16 provision (19-25)",
        SubCategoryFilter.SenTransportDsg => "SEN transport (LA-managed)",
        SubCategoryFilter.EdPsychologyService => "Educational psychology service",
        SubCategoryFilter.SenAdmin => "SEN admin",
        _ => ""
    };

    public static decimal? GetValue(this SubCategoryFilter filter, HighNeedsSpending s) =>
        filter switch
        {
            SubCategoryFilter.TotalPlaceFunding => s.TotalPlaceFunding,
            SubCategoryFilter.TotalTopUpFundingMaintained => s.TotalTopUpFundingMaintained,
            SubCategoryFilter.TotalTopUpFundingNonMaintained => s.TotalTopUpFundingNonMaintained,
            SubCategoryFilter.TotalSenServices => s.TotalSenServices,
            SubCategoryFilter.TotalAlternativeProvisionServices => s.TotalAlternativeProvisionServices,
            SubCategoryFilter.TotalHospitalServices => s.TotalHospitalServices,
            SubCategoryFilter.TotalOtherHealthServices => s.TotalOtherHealthServices,
            SubCategoryFilter.PlaceFundingPrimary => s.PlaceFundingPrimary,
            SubCategoryFilter.PlaceFundingSecondary => s.PlaceFundingSecondary,
            SubCategoryFilter.PlaceFundingSpecial => s.PlaceFundingSpecial,
            SubCategoryFilter.TopFundingMaintainedEarlyYears => s.TopFundingMaintainedEarlyYears,
            SubCategoryFilter.TopFundingMaintainedPrimary => s.TopFundingMaintainedPrimary,
            SubCategoryFilter.TopFundingMaintainedSecondary => s.TopFundingMaintainedSecondary,
            SubCategoryFilter.TopFundingMaintainedSpecial => s.TopFundingMaintainedSpecial,
            SubCategoryFilter.TopFundingMaintainedAlternativeProvision => s.TopFundingMaintainedAlternativeProvision,
            SubCategoryFilter.TopFundingMaintainedPostSchool => s.TopFundingMaintainedPostSchool,
            SubCategoryFilter.TopFundingMaintainedIncome => s.TopFundingMaintainedIncome,
            SubCategoryFilter.TopFundingNonMaintainedEarlyYears => s.TopFundingNonMaintainedEarlyYears,
            SubCategoryFilter.TopFundingNonMaintainedPrimary => s.TopFundingNonMaintainedPrimary,
            SubCategoryFilter.TopFundingNonMaintainedSecondary => s.TopFundingNonMaintainedSecondary,
            SubCategoryFilter.TopFundingNonMaintainedSpecial => s.TopFundingNonMaintainedSpecial,
            SubCategoryFilter.TopFundingNonMaintainedAlternativeProvision => s.TopFundingNonMaintainedAlternativeProvision,
            SubCategoryFilter.TopFundingNonMaintainedPostSchool => s.TopFundingNonMaintainedPostSchool,
            SubCategoryFilter.TopFundingNonMaintainedIncome => s.TopFundingNonMaintainedIncome,
            SubCategoryFilter.PlaceFundingAlternativeProvision => s.PlaceFundingAlternativeProvision,
            SubCategoryFilter.HometoSchoolTransportPre16 => s.HometoSchoolTransportPre16,
            SubCategoryFilter.HometoSchoolTransport1618 => s.HometoSchoolTransport1618,
            SubCategoryFilter.HometoSchoolTransport1925 => s.HometoSchoolTransport1925,
            SubCategoryFilter.SenTransportDsg => s.SenTransportDsg,
            SubCategoryFilter.EdPsychologyService => s.EdPsychologyService,
            SubCategoryFilter.SenAdmin => s.SenAdmin,
            _ => null
        };

    public static string[] GetLineCodes(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.TotalPlaceFunding => ["1.0.2"],
        SubCategoryFilter.TotalTopUpFundingMaintained => ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        SubCategoryFilter.TotalTopUpFundingNonMaintained => ["1.2.3"],
        SubCategoryFilter.TotalSenServices => ["1.2.5", "1.2.8", "1.2.9"],
        SubCategoryFilter.TotalAlternativeProvisionServices => ["1.2.7"],
        SubCategoryFilter.TotalHospitalServices => ["1.2.6"],
        SubCategoryFilter.TotalOtherHealthServices => ["1.2.13"],
        SubCategoryFilter.PlaceFundingPrimary => ["1.0.2"],
        SubCategoryFilter.PlaceFundingSecondary => ["1.0.2"],
        SubCategoryFilter.PlaceFundingSpecial => ["1.0.2"],
        SubCategoryFilter.PlaceFundingAlternativeProvision => ["1.0.2"],
        SubCategoryFilter.TopFundingMaintainedEarlyYears => ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        SubCategoryFilter.TopFundingMaintainedPrimary => ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        SubCategoryFilter.TopFundingMaintainedSecondary => ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        SubCategoryFilter.TopFundingMaintainedSpecial => ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        SubCategoryFilter.TopFundingMaintainedAlternativeProvision => ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        SubCategoryFilter.TopFundingMaintainedPostSchool => ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        SubCategoryFilter.TopFundingMaintainedIncome => ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        SubCategoryFilter.TopFundingNonMaintainedEarlyYears => ["1.2.3"],
        SubCategoryFilter.TopFundingNonMaintainedPrimary => ["1.2.3"],
        SubCategoryFilter.TopFundingNonMaintainedSecondary => ["1.2.3"],
        SubCategoryFilter.TopFundingNonMaintainedSpecial => ["1.2.3"],
        SubCategoryFilter.TopFundingNonMaintainedAlternativeProvision => ["1.2.3"],
        SubCategoryFilter.TopFundingNonMaintainedPostSchool => ["1.2.3"],
        SubCategoryFilter.TopFundingNonMaintainedIncome => ["1.2.3"],
        SubCategoryFilter.HometoSchoolTransportPre16 => ["2.1.4"],
        SubCategoryFilter.HometoSchoolTransport1618 => ["2.1.6"],
        SubCategoryFilter.HometoSchoolTransport1925 => ["2.1.7"],
        SubCategoryFilter.SenTransportDsg => ["1.4.11"],
        SubCategoryFilter.EdPsychologyService => ["2.1.1"],
        SubCategoryFilter.SenAdmin => ["2.1.2"],
        _ => []
    };

    public static HighNeedsSpendingDataSourceInfoType GetAdditionalInfo(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.TotalPlaceFunding => HighNeedsSpendingDataSourceInfoType.Glossary,
        SubCategoryFilter.TotalHospitalServices => HighNeedsSpendingDataSourceInfoType.Hospital,
        SubCategoryFilter.PlaceFundingPrimary => HighNeedsSpendingDataSourceInfoType.Glossary,
        SubCategoryFilter.PlaceFundingSecondary => HighNeedsSpendingDataSourceInfoType.Glossary,
        SubCategoryFilter.PlaceFundingSpecial => HighNeedsSpendingDataSourceInfoType.Glossary,
        SubCategoryFilter.PlaceFundingAlternativeProvision => HighNeedsSpendingDataSourceInfoType.Glossary,
        _ => HighNeedsSpendingDataSourceInfoType.None
    };

    public enum HighNeedsSpendingDataSourceInfoType
    {
        Glossary,
        Hospital,
        None
    }
}