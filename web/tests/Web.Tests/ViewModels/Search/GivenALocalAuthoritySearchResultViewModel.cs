using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels.Search;
using Xunit;

namespace Web.Tests.ViewModels.Search;

public class GivenALocalAuthoritySearchResultViewModel
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void WhenCreateShouldMapProperties()
    {
        var localAuthoritySummary = _fixture.Create<LocalAuthoritySummary>();

        var actual = LocalAuthoritySearchResultViewModel.Create(localAuthoritySummary);

        Assert.Equal(localAuthoritySummary.Code, actual.Code);
        Assert.Equal(localAuthoritySummary.Name, actual.Name);
    }
}