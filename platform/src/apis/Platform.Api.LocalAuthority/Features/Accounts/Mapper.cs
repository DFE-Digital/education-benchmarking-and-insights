using Platform.Api.LocalAuthority.Features.Accounts.Models;

namespace Platform.Api.LocalAuthority.Features.Accounts;

public static class Mapper
{
    public static LocalAuthority<HighNeeds> MultiMapToHighNeeds(object[] objects)
    {
        var localAuthority = objects[0] as LocalAuthorityBase;
        var outturn = objects[1] as HighNeedsBase;
        var outturnHighNeedsAmount = objects[2] as HighNeedsAmount;
        var outturnTopFundingMaintained = objects[3] as TopFunding;
        var outturnTopFundingNonMaintained = objects[4] as TopFunding;
        var outturnPlaceFunding = objects[5] as PlaceFunding;
        var budget = objects[6] as HighNeedsBase;
        var budgetHighNeedsAmount = objects[7] as HighNeedsAmount;
        var budgetTopFundingMaintained = objects[8] as TopFunding;
        var budgetTopFundingNonMaintained = objects[9] as TopFunding;
        var budgetPlaceFunding = objects[10] as PlaceFunding;

        return new LocalAuthority<HighNeeds>
        {
            Code = localAuthority?.Code,
            Name = localAuthority?.Name,
            Population2To18 = localAuthority?.Population2To18,
            TotalPupils = localAuthority?.TotalPupils,
            Outturn = new HighNeeds
            {
                Total = outturn?.Total,
                HighNeedsAmount = outturnHighNeedsAmount,
                Maintained = outturnTopFundingMaintained,
                NonMaintained = outturnTopFundingNonMaintained,
                PlaceFunding = outturnPlaceFunding
            },
            Budget = new HighNeeds
            {
                Total = budget?.Total,
                HighNeedsAmount = budgetHighNeedsAmount,
                Maintained = budgetTopFundingMaintained,
                NonMaintained = budgetTopFundingNonMaintained,
                PlaceFunding = budgetPlaceFunding
            }
        };
    }

    public static (HighNeedsYear outturn, HighNeedsYear budget) MultiMapToHighNeedsYear(object[] objects)
    {
        var highNeedsYear = objects[0] as HighNeedsYearBase;
        var outturn = objects[1] as HighNeedsBase;
        var outturnHighNeedsAmount = objects[2] as HighNeedsAmount;
        var outturnTopFundingMaintained = objects[3] as TopFunding;
        var outturnTopFundingNonMaintained = objects[4] as TopFunding;
        var outturnPlaceFunding = objects[5] as PlaceFunding;
        var budget = objects[6] as HighNeedsBase;
        var budgetHighNeedsAmount = objects[7] as HighNeedsAmount;
        var budgetTopFundingMaintained = objects[8] as TopFunding;
        var budgetTopFundingNonMaintained = objects[9] as TopFunding;
        var budgetPlaceFunding = objects[10] as PlaceFunding;

        int? year = null;
        if (int.TryParse(highNeedsYear?.RunId, out var parsed))
        {
            year = parsed;
        }

        return (
            new HighNeedsYear
            {
                Code = highNeedsYear?.Code,
                Year = year,
                Total = outturn?.Total,
                HighNeedsAmount = outturnHighNeedsAmount,
                Maintained = outturnTopFundingMaintained,
                NonMaintained = outturnTopFundingNonMaintained,
                PlaceFunding = outturnPlaceFunding
            },
            new HighNeedsYear
            {
                Code = highNeedsYear?.Code,
                Year = year,
                Total = budget?.Total,
                HighNeedsAmount = budgetHighNeedsAmount,
                Maintained = budgetTopFundingMaintained,
                NonMaintained = budgetTopFundingNonMaintained,
                PlaceFunding = budgetPlaceFunding
            });
    }

    public static HighNeedsResponse MapBudget(BudgetDto dto) => new()
    {
        Code = dto.LaCode,
        TotalPupils = dto.TotalPupils,
        TotalHighNeeds = dto.BudgetTotalHighNeeds,
        TotalPlaceFunding = dto.BudgetTotalPlaceFunding,
        TotalTopUpFundingMaintained = dto.BudgetTotalTopUpFundingMaintained,
        TotalTopUpFundingNonMaintained = dto.BudgetTotalTopUpFundingNonMaintained,
        TotalSenServices = dto.BudgetTotalSenServices,
        TotalAlternativeProvisionServices = dto.BudgetTotalAlternativeProvisionServices,
        TotalHospitalServices = dto.BudgetTotalHospitalServices,
        TotalOtherHealthServices = dto.BudgetTotalOtherHealthServices,
        TopFundingMaintainedEarlyYears = dto.BudgetTopFundingMaintainedEarlyYears,
        TopFundingMaintainedPrimary = dto.BudgetTopFundingMaintainedPrimary,
        TopFundingMaintainedSecondary = dto.BudgetTopFundingMaintainedSecondary,
        TopFundingMaintainedSpecial = dto.BudgetTopFundingMaintainedSpecial,
        TopFundingMaintainedAlternativeProvision = dto.BudgetTopFundingMaintainedAlternativeProvision,
        TopFundingMaintainedPostSchool = dto.BudgetTopFundingMaintainedPostSchool,
        TopFundingMaintainedIncome = dto.BudgetTopFundingMaintainedIncome,
        TopFundingNonMaintainedEarlyYears = dto.BudgetTopFundingNonMaintainedEarlyYears,
        TopFundingNonMaintainedPrimary = dto.BudgetTopFundingNonMaintainedPrimary,
        TopFundingNonMaintainedSecondary = dto.BudgetTopFundingNonMaintainedSecondary,
        TopFundingNonMaintainedSpecial = dto.BudgetTopFundingNonMaintainedSpecial,
        TopFundingNonMaintainedAlternativeProvision = dto.BudgetTopFundingNonMaintainedAlternativeProvision,
        TopFundingNonMaintainedPostSchool = dto.BudgetTopFundingNonMaintainedPostSchool,
        TopFundingNonMaintainedIncome = dto.BudgetTopFundingNonMaintainedIncome,
        PlaceFundingPrimary = dto.BudgetPlaceFundingPrimary,
        PlaceFundingSecondary = dto.BudgetPlaceFundingSecondary,
        PlaceFundingSpecial = dto.BudgetPlaceFundingSpecial,
        PlaceFundingAlternativeProvision = dto.BudgetPlaceFundingAlternativeProvision,
        SenTransport = dto.BudgetSENTransport,
        HometoSchoolTransportPre16 = dto.BudgetHometoSchoolTransportPre16,
        HometoSchoolTransport1618 = dto.BudgetHometoSchoolTransport1618,
        HometoSchoolTransport1925 = dto.BudgetHometoSchoolTransport1925,
        EdPsychologyService = dto.BudgetEdPsychologyService,
        SenAdmin = dto.BudgetSENAdmin
    };

    public static HighNeedsResponse MapOutturn(OutturnDto dto) => new()
    {
        Code = dto.LaCode,
        TotalPupils = dto.TotalPupils,
        TotalHighNeeds = dto.OutturnTotalHighNeeds,
        TotalPlaceFunding = dto.OutturnTotalPlaceFunding,
        TotalTopUpFundingMaintained = dto.OutturnTotalTopUpFundingMaintained,
        TotalTopUpFundingNonMaintained = dto.OutturnTotalTopUpFundingNonMaintained,
        TotalSenServices = dto.OutturnTotalSenServices,
        TotalAlternativeProvisionServices = dto.OutturnTotalAlternativeProvisionServices,
        TotalHospitalServices = dto.OutturnTotalHospitalServices,
        TotalOtherHealthServices = dto.OutturnTotalOtherHealthServices,
        TopFundingMaintainedEarlyYears = dto.OutturnTopFundingMaintainedEarlyYears,
        TopFundingMaintainedPrimary = dto.OutturnTopFundingMaintainedPrimary,
        TopFundingMaintainedSecondary = dto.OutturnTopFundingMaintainedSecondary,
        TopFundingMaintainedSpecial = dto.OutturnTopFundingMaintainedSpecial,
        TopFundingMaintainedAlternativeProvision = dto.OutturnTopFundingMaintainedAlternativeProvision,
        TopFundingMaintainedPostSchool = dto.OutturnTopFundingMaintainedPostSchool,
        TopFundingMaintainedIncome = dto.OutturnTopFundingMaintainedIncome,
        TopFundingNonMaintainedEarlyYears = dto.OutturnTopFundingNonMaintainedEarlyYears,
        TopFundingNonMaintainedPrimary = dto.OutturnTopFundingNonMaintainedPrimary,
        TopFundingNonMaintainedSecondary = dto.OutturnTopFundingNonMaintainedSecondary,
        TopFundingNonMaintainedSpecial = dto.OutturnTopFundingNonMaintainedSpecial,
        TopFundingNonMaintainedAlternativeProvision = dto.OutturnTopFundingNonMaintainedAlternativeProvision,
        TopFundingNonMaintainedPostSchool = dto.OutturnTopFundingNonMaintainedPostSchool,
        TopFundingNonMaintainedIncome = dto.OutturnTopFundingNonMaintainedIncome,
        PlaceFundingPrimary = dto.OutturnPlaceFundingPrimary,
        PlaceFundingSecondary = dto.OutturnPlaceFundingSecondary,
        PlaceFundingSpecial = dto.OutturnPlaceFundingSpecial,
        PlaceFundingAlternativeProvision = dto.OutturnPlaceFundingAlternativeProvision,
        SenTransport = dto.OutturnSENTransport,
        HometoSchoolTransportPre16 = dto.OutturnHometoSchoolTransportPre16,
        HometoSchoolTransport1618 = dto.OutturnHometoSchoolTransport1618,
        HometoSchoolTransport1925 = dto.OutturnHometoSchoolTransport1925,
        EdPsychologyService = dto.OutturnEdPsychologyService,
        SenAdmin = dto.OutturnSENAdmin
    };
}