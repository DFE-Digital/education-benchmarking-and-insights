using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;
namespace Web.Tests.ViewModels;

public class GivenALocalAuthorityViewModel
{
    private readonly Fixture _fixture = new();
    private readonly LocalAuthority _localAuthority;
    private readonly LocalAuthoritySchool[] _schools;

    public GivenALocalAuthorityViewModel()
    {
        _schools = _fixture.Build<LocalAuthoritySchool>()
            .Without(x => x.OverallPhase)
            .CreateMany(20)
            .ToArray();

        for (var i = 0; i < _schools.Length; i++)
        {
            _schools[i].URN = i.ToString();
            _schools[i].OverallPhase = i < 2
                ? OverallPhaseTypes.Nursery
                : i < 6
                    ? OverallPhaseTypes.Primary
                    : OverallPhaseTypes.Secondary;
        }

        _localAuthority = _fixture
            .Build<LocalAuthority>()
            .With(l => l.Schools, _schools)
            .Create();
    }

    [Fact]
    public void WhenContainsSchools()
    {
        var vm = new LocalAuthorityViewModel(_localAuthority);

        var nurserySchools = _schools
            .Where(s => int.Parse(s.URN!) < 2)
            .OrderBy(s => s.SchoolName);
        Assert.Equal(nurserySchools, vm.GroupedSchools
            .Where(g => g.Key == OverallPhaseTypes.Nursery)
            .SelectMany(g => g));

        var primarySchools = _schools
            .Where(s => int.Parse(s.URN!) >= 2 && int.Parse(s.URN!) < 6)
            .OrderBy(s => s.SchoolName);
        Assert.Equal(primarySchools, vm.GroupedSchools
            .Where(g => g.Key == OverallPhaseTypes.Primary)
            .SelectMany(g => g));

        var secondarySchools = _schools
            .Where(s => int.Parse(s.URN!) >= 6)
            .OrderBy(s => s.SchoolName);
        Assert.Equal(secondarySchools, vm.GroupedSchools
            .Where(g => g.Key == OverallPhaseTypes.Secondary)
            .SelectMany(g => g));

        Assert.Equal([OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Nursery], vm.GroupedSchools.Select(g => g.Key));
        Assert.Equal(_localAuthority.Code, vm.Code);
        Assert.Equal(_localAuthority.Name, vm.Name);
    }
}