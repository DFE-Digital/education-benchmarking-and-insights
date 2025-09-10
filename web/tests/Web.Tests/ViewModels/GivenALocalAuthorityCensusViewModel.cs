using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenALocalAuthorityCensusViewModel
{
    private readonly Fixture _fixture = new();
    private readonly LocalAuthority _localAuthority;

    public GivenALocalAuthorityCensusViewModel()
    {
        var schools = _fixture.Build<LocalAuthoritySchool>()
            .Without(x => x.OverallPhase)
            .CreateMany(20)
            .ToArray();

        for (var i = 0; i < schools.Length; i++)
        {
            schools[i].OverallPhase = i < 2
                ? OverallPhaseTypes.Nursery
                : i < 6
                    ? OverallPhaseTypes.Primary
                    : OverallPhaseTypes.Secondary;
        }

        _localAuthority = _fixture
            .Build<LocalAuthority>()
            .With(l => l.Schools, schools)
            .Create();
    }

    [Fact]
    public void WhenContainsSchools()
    {
        var vm = new LocalAuthorityCensusViewModel(_localAuthority);

        string[] expected = ["Secondary", "Primary", "Nursery"];
        Assert.Equal(expected, vm.Phases);
        Assert.Equal(_localAuthority.Code, vm.Code);
        Assert.Equal(_localAuthority.Name, vm.Name);
    }
}