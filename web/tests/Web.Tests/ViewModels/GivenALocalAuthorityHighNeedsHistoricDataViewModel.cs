using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenALocalAuthorityHighNeedsHistoricDataViewModel
{
    private readonly Fixture _fixture = new();
    private readonly Web.App.Domain.LocalAuthorities.LocalAuthority _localAuthority;

    public GivenALocalAuthorityHighNeedsHistoricDataViewModel()
    {
        _localAuthority = _fixture
            .Build<Web.App.Domain.LocalAuthorities.LocalAuthority>()
            .Create();
    }

    [Fact]
    public void ShouldSetHasHighNeedsToTrueWhenContainsHighNeeds()
    {
        var highNeeds = _fixture
            .Build<LocalAuthorityHighNeeds<HighNeeds>>()
            .CreateMany()
            .ToArray();
        var model = new LocalAuthorityHighNeedsHistoricDataViewModel(_localAuthority, highNeeds);

        Assert.Equal(_localAuthority.Code, model.Code);
        Assert.Equal(_localAuthority.Name, model.Name);
        Assert.True(model.HasHighNeeds);
    }

    [Fact]
    public void ShouldSetHasHighNeedsToFalseWhenContainsEmptyHighNeeds()
    {
        LocalAuthorityHighNeeds<HighNeeds>[] highNeeds = [];
        var model = new LocalAuthorityHighNeedsHistoricDataViewModel(_localAuthority, highNeeds);
        Assert.False(model.HasHighNeeds);
    }

    [Fact]
    public void ShouldSetHasHighNeedsToFalseWhenContainsMalformedHighNeeds()
    {
        var highNeeds = _fixture
            .Build<LocalAuthorityHighNeeds<HighNeeds>>()
            .With(h => h.Outturn, new HighNeeds())
            .CreateMany()
            .ToArray();
        var model = new LocalAuthorityHighNeedsHistoricDataViewModel(_localAuthority, highNeeds);
        Assert.False(model.HasHighNeeds);
    }
}