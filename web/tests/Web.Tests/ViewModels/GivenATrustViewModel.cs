using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;
namespace Web.Tests.ViewModels;

public class GivenATrustViewModel
{
    private readonly TrustBalance _balance;
    private readonly Fixture _fixture = new();
    private readonly Trust _trust;

    public GivenATrustViewModel()
    {
        _trust = _fixture.Create<Trust>();
        _balance = _fixture.Create<TrustBalance>();
    }

    [Fact]
    public void WhenContainsPrimarySchools()
    {
        var (viewModel, schools) = BuildViewModelForOverallPhase("Primary");

        var actual = viewModel.PrimarySchools.OrderBy(s => s.Urn).Select(s => s.Urn);
        var expected = schools.OrderBy(s => s.URN).Select(s => s.URN);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenContainsSecondarySchools()
    {
        var (viewModel, schools) = BuildViewModelForOverallPhase("Secondary");

        var actual = viewModel.SecondarySchools.OrderBy(s => s.Urn).Select(s => s.Urn);
        var expected = schools.OrderBy(s => s.URN).Select(s => s.URN);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenContainsSpecialSchools()
    {
        var (viewModel, schools) = BuildViewModelForOverallPhase("Special");

        var actual = viewModel.Special.OrderBy(s => s.Urn).Select(s => s.Urn);
        var expected = schools.OrderBy(s => s.URN).Select(s => s.URN);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenContainsAlternativeProvisionSchools()
    {
        var (viewModel, schools) = BuildViewModelForOverallPhase("Alternative Provision");

        var actual = viewModel.AlternativeProvision.OrderBy(s => s.Urn).Select(s => s.Urn);
        var expected = schools.OrderBy(s => s.URN).Select(s => s.URN);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenContainsAllThroughSchools()
    {
        var (viewModel, schools) = BuildViewModelForOverallPhase("All-through");

        var actual = viewModel.AllThroughSchools.OrderBy(s => s.Urn).Select(s => s.Urn);
        var expected = schools.OrderBy(s => s.URN).Select(s => s.URN);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenContainsUniversityTechnicalColleges()
    {
        var (viewModel, schools) = BuildViewModelForOverallPhase("University Technical College");

        var actual = viewModel.UniversityTechnicalColleges.OrderBy(s => s.Urn).Select(s => s.Urn);
        var expected = schools.OrderBy(s => s.URN).Select(s => s.URN);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenContainsPostSixteenSchools()
    {
        var (viewModel, schools) = BuildViewModelForOverallPhase("Post-16");

        var actual = viewModel.PostSixteen.OrderBy(s => s.Urn).Select(s => s.Urn);
        var expected = schools.OrderBy(s => s.URN).Select(s => s.URN);
        Assert.Equal(expected, actual);
    }

    private (TrustViewModel, TrustSchool[] schools) BuildViewModelForOverallPhase(params string[] overallPhases)
    {
        var random = new Random();
        var schools = _fixture
            .Build<TrustSchool>()
            .With(s => s.OverallPhase, () => overallPhases.ElementAt(random.Next(overallPhases.Length)))
            .CreateMany()
            .ToArray();

        var ratings = schools.Select(s => new RagRating
        {
            URN = s.URN
        });

        _trust.Schools = schools;
        return (new TrustViewModel(_trust, _balance, ratings), schools);
    }
}