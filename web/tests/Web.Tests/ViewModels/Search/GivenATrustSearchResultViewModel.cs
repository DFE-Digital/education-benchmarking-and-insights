using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels.Search;
using Xunit;

namespace Web.Tests.ViewModels.Search;

public class GivenATrustSearchResultViewModel
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void WhenCreateShouldMapProperties()
    {
        var trustSummary = _fixture.Create<TrustSummary>();

        var actual = TrustSearchResultViewModel.Create(trustSummary);

        Assert.Equal(trustSummary.CompanyNumber, actual.CompanyNumber);
        Assert.Equal(trustSummary.TrustName, actual.TrustName);
        Assert.Equal(trustSummary.TotalPupils, actual.TotalPupils);
        Assert.Equal(trustSummary.SchoolsInTrust, actual.SchoolsInTrust);
    }
}