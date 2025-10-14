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

    public static TheoryData<string, Func<LocalAuthorityViewModel, IEnumerable<RagSchoolViewModel>>> WithOverallPhaseTestData = new()
    {
        { "Primary", x => x.PrimarySchools },
        { "Secondary", x => x.SecondarySchools },
        { "Special", x => x.Special },
        { "Alternative Provision", x => x.AlternativeProvision },
        { "All-through", x => x.AllThroughSchools },
        { "University Technical College", x => x.UniversityTechnicalColleges },
        { "Post-16", x => x.PostSixteen },
    };

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
    [MemberData(nameof(WithOverallPhaseTestData))]
    public void WhenContainsOverallPhase(string overallPhase, Func<LocalAuthorityViewModel, IEnumerable<RagSchoolViewModel>> selector)
    {
        var schools = _fixture
            .Build<LocalAuthoritySchool>()
            .With(s => s.OverallPhase, overallPhase)
            .CreateMany()
            .ToArray();

        var ratings = schools
            .Select(s => _fixture
                .Build<RagRatingSummary>()
                .With(r => r.URN, s.URN)
                .Create())
            .ToArray();

        _localAuthority.Schools = schools;

        var actual = selector
            .Invoke(new LocalAuthorityViewModel(_localAuthority, ratings))
            .OrderBy(s => s.Urn)
            .Select(s => s.Urn);

        var expected = ratings
            .Where(r => r.OverallPhase == overallPhase)
            .OrderByDescending(o => (o.Red + o.Amber + o.Green) > 0 ? o.Red / (o.Red + o.Amber + o.Green) : 0)
            .ThenByDescending(o => (o.Red + o.Amber + o.Green) > 0 ? o.Amber / (o.Red + o.Amber + o.Green) : 0)
            .ThenBy(o => o.SchoolName)
            .Take(5)
            .Select(s => s.URN);
        Assert.Equal(expected, actual);
    }
}