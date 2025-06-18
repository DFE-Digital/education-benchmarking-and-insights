using AutoFixture;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.HighNeeds;

public class WhenHighNeedsMapperMaps
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldMapWhenMultiMapToHighNeeds()
    {
        // arrange
        var localAuthority = _fixture.Create<LocalAuthorityBase>();
        var outturn = _fixture.Create<HighNeedsBase>();
        var outturnHighNeedsAmount = _fixture.Create<HighNeedsAmount>();
        var outturnTopFundingMaintained = _fixture.Create<TopFunding>();
        var outturnTopFundingNonMaintained = _fixture.Create<TopFunding>();
        var outturnPlaceFunding = _fixture.Create<PlaceFunding>();
        var budget = _fixture.Create<HighNeedsBase>();
        var budgetHighNeedsAmount = _fixture.Create<HighNeedsAmount>();
        var budgetTopFundingMaintained = _fixture.Create<TopFunding>();
        var budgetTopFundingNonMaintained = _fixture.Create<TopFunding>();
        var budgetPlaceFunding = _fixture.Create<PlaceFunding>();
        object[] objects =
        [
            localAuthority,
            outturn,
            outturnHighNeedsAmount,
            outturnTopFundingMaintained,
            outturnTopFundingNonMaintained,
            outturnPlaceFunding,
            budget,
            budgetHighNeedsAmount,
            budgetTopFundingMaintained,
            budgetTopFundingNonMaintained,
            budgetPlaceFunding
        ];

        // act
        var actual = Mapper.MultiMapToHighNeeds(objects);

        // assert
        Assert.Equal(localAuthority.Code, actual.Code);
        Assert.Equal(localAuthority.Name, actual.Name);
        Assert.Equal(localAuthority.Population2To18, actual.Population2To18);
        Assert.Equal(localAuthority.CarriedForwardBalance, actual.CarriedForwardBalance);
        Assert.Equal(outturn.Total, actual.Outturn?.Total);
        Assert.Equal(outturnHighNeedsAmount, actual.Outturn?.HighNeedsAmount);
        Assert.Equal(outturnTopFundingMaintained, actual.Outturn?.Maintained);
        Assert.Equal(outturnTopFundingNonMaintained, actual.Outturn?.NonMaintained);
        Assert.Equal(outturnPlaceFunding, actual.Outturn?.PlaceFunding);
        Assert.Equal(budget.Total, actual.Budget?.Total);
        Assert.Equal(budgetHighNeedsAmount, actual.Budget?.HighNeedsAmount);
        Assert.Equal(budgetTopFundingMaintained, actual.Budget?.Maintained);
        Assert.Equal(budgetTopFundingNonMaintained, actual.Budget?.NonMaintained);
        Assert.Equal(budgetPlaceFunding, actual.Budget?.PlaceFunding);
    }

    [Fact]
    public void ShouldMapWhenMultiMapToHighNeedsYear()
    {
        // arrange
        var highNeedsYear = _fixture
            .Build<HighNeedsYearBase>()
            .With(h => h.RunId, _fixture.Create<int>().ToString)
            .Create();
        var outturn = _fixture.Create<HighNeedsBase>();
        var outturnHighNeedsAmount = _fixture.Create<HighNeedsAmount>();
        var outturnTopFundingMaintained = _fixture.Create<TopFunding>();
        var outturnTopFundingNonMaintained = _fixture.Create<TopFunding>();
        var outturnPlaceFunding = _fixture.Create<PlaceFunding>();
        var budget = _fixture.Create<HighNeedsBase>();
        var budgetHighNeedsAmount = _fixture.Create<HighNeedsAmount>();
        var budgetTopFundingMaintained = _fixture.Create<TopFunding>();
        var budgetTopFundingNonMaintained = _fixture.Create<TopFunding>();
        var budgetPlaceFunding = _fixture.Create<PlaceFunding>();
        object[] objects =
        [
            highNeedsYear,
            outturn,
            outturnHighNeedsAmount,
            outturnTopFundingMaintained,
            outturnTopFundingNonMaintained,
            outturnPlaceFunding,
            budget,
            budgetHighNeedsAmount,
            budgetTopFundingMaintained,
            budgetTopFundingNonMaintained,
            budgetPlaceFunding
        ];

        // act
        var (outturnYear, budgetYear) = Mapper.MultiMapToHighNeedsYear(objects);

        // assert
        Assert.Equal(highNeedsYear.Code, outturnYear.Code);
        Assert.Equal(highNeedsYear.RunId, outturnYear.Year.ToString());
        Assert.Equal(outturn.Total, outturnYear.Total);
        Assert.Equal(outturnHighNeedsAmount, outturnYear.HighNeedsAmount);
        Assert.Equal(outturnTopFundingMaintained, outturnYear.Maintained);
        Assert.Equal(outturnTopFundingNonMaintained, outturnYear.NonMaintained);
        Assert.Equal(outturnPlaceFunding, outturnYear.PlaceFunding);
        Assert.Equal(budget.Total, budgetYear.Total);
        Assert.Equal(budgetHighNeedsAmount, budgetYear.HighNeedsAmount);
        Assert.Equal(budgetTopFundingMaintained, budgetYear.Maintained);
        Assert.Equal(budgetTopFundingNonMaintained, budgetYear.NonMaintained);
        Assert.Equal(budgetPlaceFunding, budgetYear.PlaceFunding);
    }
}