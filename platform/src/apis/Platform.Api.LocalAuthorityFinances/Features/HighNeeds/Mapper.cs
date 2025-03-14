using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds;

public static class Mapper
{
    public static LocalAuthority<Models.HighNeeds> MultiMapToHighNeeds(object[] objects)
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

        return new LocalAuthority<Models.HighNeeds>
        {
            Code = localAuthority?.Code,
            Name = localAuthority?.Name,
            Outturn = new Models.HighNeeds
            {
                Total = outturn?.Total,
                HighNeedsAmount = outturnHighNeedsAmount,
                Maintained = outturnTopFundingMaintained,
                NonMaintained = outturnTopFundingNonMaintained,
                PlaceFunding = outturnPlaceFunding
            },
            Budget = new Models.HighNeeds
            {
                Total = budget?.Total,
                HighNeedsAmount = budgetHighNeedsAmount,
                Maintained = budgetTopFundingMaintained,
                NonMaintained = budgetTopFundingNonMaintained,
                PlaceFunding = budgetPlaceFunding
            }
        };
    }
}