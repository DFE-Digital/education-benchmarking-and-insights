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
        var vm = new LocalAuthorityViewModel(_localAuthority, []);

        var nurserySchools = _schools
            .Where(s => int.Parse(s.URN!) < 2)
            .OrderBy(s => s.SchoolName);
        Assert.Equal(nurserySchools, vm.GroupedSchoolNames
            .Where(g => g.Key == OverallPhaseTypes.Nursery)
            .SelectMany(g => g));

        var primarySchools = _schools
            .Where(s => int.Parse(s.URN!) >= 2 && int.Parse(s.URN!) < 6)
            .OrderBy(s => s.SchoolName);
        Assert.Equal(primarySchools, vm.GroupedSchoolNames
            .Where(g => g.Key == OverallPhaseTypes.Primary)
            .SelectMany(g => g));

        var secondarySchools = _schools
            .Where(s => int.Parse(s.URN!) >= 6)
            .OrderBy(s => s.SchoolName);
        Assert.Equal(secondarySchools, vm.GroupedSchoolNames
            .Where(g => g.Key == OverallPhaseTypes.Secondary)
            .SelectMany(g => g));

        Assert.Equal([OverallPhaseTypes.Primary, OverallPhaseTypes.Secondary, OverallPhaseTypes.Nursery], vm.GroupedSchoolNames.Select(g => g.Key));
        Assert.Equal(_localAuthority.Code, vm.Code);
        Assert.Equal(_localAuthority.Name, vm.Name);
        Assert.Equal(_schools.Length, vm.NumberOfSchools);
    }

    [Theory]
    [InlineData(OverallPhaseTypes.Primary)]
    [InlineData(OverallPhaseTypes.Secondary)]
    [InlineData(OverallPhaseTypes.Nursery)]
    public void WhenContainsOverallPhase(string overallPhase)
    {
        var random = new Random();
        var ratings = _schools
            .Select(s => _fixture
                .Build<RagRatingSummary>()
                .With(r => r.URN, s.URN)
                .With(r => r.SchoolName, s.SchoolName)
                .With(r => r.OverallPhase, s.OverallPhase)
                .With(r => r.RedCount, random.Next(0, 8))
                .With(r => r.AmberCount, random.Next(0, 8))
                .With(r => r.GreenCount, random.Next(0, 8))
                .Create())
            .ToArray();

        var actual = new LocalAuthorityViewModel(_localAuthority, ratings).GroupedSchools
            .Where(g => g.OverallPhase == overallPhase)
            .SelectMany(g => g.Schools)
            .Select(s => s.Urn);

        var expected = ratings
            .GroupBy(x => x.OverallPhase)
            .Select(x => (
                OverallPhase: x.Key,
                Schools: x
                    .Select(s => new RagSchoolViewModel(
                        s.URN,
                        s.SchoolName,
                        s.RedCount ?? 0,
                        s.AmberCount ?? 0,
                        s.GreenCount ?? 0
                    )).OrderByDescending(o => o.RedRatio)
                    .ThenByDescending(o => o.AmberRatio)
                    .ThenBy(o => o.Name)
                    .Take(5)))
            .Where(g => g.OverallPhase == overallPhase)
            .SelectMany(g => g.Schools)
            .Select(s => s.Urn);
        Assert.Equal(expected, actual);
    }
}