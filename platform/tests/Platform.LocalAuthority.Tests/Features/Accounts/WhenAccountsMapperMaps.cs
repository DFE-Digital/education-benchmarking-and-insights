using AutoFixture;
using Platform.Api.LocalAuthority.Features.Accounts;
using Platform.Api.LocalAuthority.Features.Accounts.Models;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Accounts;

public class WhenAccountsMapperMaps
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void MultiMapToHighNeedsShouldMapCorrectly()
    {
        var laBase = _fixture.Create<LocalAuthorityBase>();
        var outturnBase = _fixture.Create<HighNeedsBase>();
        var outturnHighNeedsAmount = _fixture.Create<HighNeedsAmount>();
        var outturnTopFundingMaintained = _fixture.Create<TopFunding>();
        var outturnTopFundingNonMaintained = _fixture.Create<TopFunding>();
        var outturnPlaceFunding = _fixture.Create<PlaceFunding>();
        var budgetBase = _fixture.Create<HighNeedsBase>();
        var budgetHighNeedsAmount = _fixture.Create<HighNeedsAmount>();
        var budgetTopFundingMaintained = _fixture.Create<TopFunding>();
        var budgetTopFundingNonMaintained = _fixture.Create<TopFunding>();
        var budgetPlaceFunding = _fixture.Create<PlaceFunding>();

        object[] input =
        [
            laBase, 
            outturnBase, 
            outturnHighNeedsAmount, 
            outturnTopFundingMaintained, 
            outturnTopFundingNonMaintained, 
            outturnPlaceFunding, 
            budgetBase, 
            budgetHighNeedsAmount, 
            budgetTopFundingMaintained, 
            budgetTopFundingNonMaintained, 
            budgetPlaceFunding
        ];

        var result = Mapper.MultiMapToHighNeeds(input);

        Assert.Equal(laBase.Code, result.Code);
        Assert.Equal(laBase.Name, result.Name);
        Assert.Equal(laBase.Population2To18, result.Population2To18);
        Assert.Equal(laBase.TotalPupils, result.TotalPupils);

        Assert.NotNull(result.Outturn);
        Assert.Equal(outturnBase.Total, result.Outturn.Total);
        Assert.Equal(outturnHighNeedsAmount, result.Outturn.HighNeedsAmount);
        Assert.Equal(outturnTopFundingMaintained, result.Outturn.Maintained);
        Assert.Equal(outturnTopFundingNonMaintained, result.Outturn.NonMaintained);
        Assert.Equal(outturnPlaceFunding, result.Outturn.PlaceFunding);

        Assert.NotNull(result.Budget);
        Assert.Equal(budgetBase.Total, result.Budget.Total);
        Assert.Equal(budgetHighNeedsAmount, result.Budget.HighNeedsAmount);
        Assert.Equal(budgetTopFundingMaintained, result.Budget.Maintained);
        Assert.Equal(budgetTopFundingNonMaintained, result.Budget.NonMaintained);
        Assert.Equal(budgetPlaceFunding, result.Budget.PlaceFunding);
    }

    [Fact]
    public void MultiMapToHighNeedsYearShouldMapValidYear()
    {
        var yearBase = _fixture.Build<HighNeedsYearBase>().With(x => x.RunId, "2023").Create();
        var outturnBase = _fixture.Create<HighNeedsBase>();
        var outturnHighNeedsAmount = _fixture.Create<HighNeedsAmount>();
        var outturnTopFundingMaintained = _fixture.Create<TopFunding>();
        var outturnTopFundingNonMaintained = _fixture.Create<TopFunding>();
        var outturnPlaceFunding = _fixture.Create<PlaceFunding>();
        var budgetBase = _fixture.Create<HighNeedsBase>();
        var budgetHighNeedsAmount = _fixture.Create<HighNeedsAmount>();
        var budgetTopFundingMaintained = _fixture.Create<TopFunding>();
        var budgetTopFundingNonMaintained = _fixture.Create<TopFunding>();
        var budgetPlaceFunding = _fixture.Create<PlaceFunding>();

        object[] input =
        [
            yearBase, 
            outturnBase, 
            outturnHighNeedsAmount, 
            outturnTopFundingMaintained, 
            outturnTopFundingNonMaintained, 
            outturnPlaceFunding, 
            budgetBase, 
            budgetHighNeedsAmount, 
            budgetTopFundingMaintained, 
            budgetTopFundingNonMaintained, 
            budgetPlaceFunding
        ];

        var (outturn, budget) = Mapper.MultiMapToHighNeedsYear(input);

        Assert.Equal(2023, outturn.Year);
        Assert.Equal(2023, budget.Year);
        Assert.Equal(yearBase.Code, outturn.Code);
        Assert.Equal(outturnBase.Total, outturn.Total);
        Assert.Equal(outturnHighNeedsAmount, outturn.HighNeedsAmount);
        Assert.Equal(budgetBase.Total, budget.Total);
    }

    [Fact]
    public void MultiMapToHighNeedsYearShouldHandleInvalidYear()
    {
        var yearBase = _fixture.Build<HighNeedsYearBase>().With(x => x.RunId, "not_a_year").Create();
        
        object[] input = new object[11];
        input[0] = yearBase;

        var (outturn, budget) = Mapper.MultiMapToHighNeedsYear(input);

        Assert.Null(outturn.Year);
        Assert.Null(budget.Year);
    }
}