using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;
namespace Web.Tests.ViewModels;

public class GivenATrustSpendingViewModel
{
    private readonly string[] _categories = [Category.EducationalIct, Category.EducationalSupplies, Category.AdministrativeSupplies];
    private readonly Fixture _fixture = new();
    private readonly Trust _trust;

    public GivenATrustSpendingViewModel()
    {
        _trust = _fixture.Create<Trust>();
    }

    [Fact]
    public void WhenContainsHighPriority()
    {
        var (viewModel, schools) = BuildViewModel("high");

        Assert.Equal(_trust.CompanyNumber, viewModel.CompanyNumber);
        Assert.Equal(_trust.TrustName, viewModel.Name);
        Assert.Equal(schools.Length, viewModel.NumberSchools);
        Assert.Equal(_categories.Length, viewModel.CostCategories.Length);
        Assert.True(viewModel.IsPriorityHigh);
    }

    [Fact]
    public void WhenContainsRagRatingsByCategory()
    {
        var (viewModel, _) = BuildViewModel();

        var resultsByCategory = viewModel.RatingsByCategory.ToList();

        var administrativeSupplies = resultsByCategory.ElementAt(0);
        Assert.Equal(_categories.ElementAt(2), administrativeSupplies.CostCategory);
        Assert.Equal("red", administrativeSupplies.Statuses.ElementAt(0).Status);
        Assert.Equal(["2"], administrativeSupplies.Statuses.ElementAt(0).Schools.Select(s => s.Urn));
        Assert.Equal("green", administrativeSupplies.Statuses.ElementAt(1).Status);
        Assert.Equal(["5", "8", "11", "14", "17", "20"], administrativeSupplies.Statuses.ElementAt(1).Schools.Select(s => s.Urn));

        var educationalIct = resultsByCategory.ElementAt(1);
        Assert.Equal(_categories.ElementAt(0), educationalIct.CostCategory);
        Assert.Equal("amber", educationalIct.Statuses.ElementAt(0).Status);
        Assert.Equal(["3"], educationalIct.Statuses.ElementAt(0).Schools.Select(s => s.Urn));
        Assert.Equal("green", educationalIct.Statuses.ElementAt(1).Status);
        Assert.Equal(["6", "9", "12", "15", "18"], educationalIct.Statuses.ElementAt(1).Schools.Select(s => s.Urn));

        var educationalSupplies = resultsByCategory.ElementAt(2);
        Assert.Equal(_categories.ElementAt(1), educationalSupplies.CostCategory);
        Assert.Equal("red", educationalSupplies.Statuses.ElementAt(0).Status);
        Assert.Equal(["1"], educationalSupplies.Statuses.ElementAt(0).Schools.Select(s => s.Urn));
        Assert.Equal("amber", educationalSupplies.Statuses.ElementAt(1).Status);
        Assert.Equal(["4"], educationalSupplies.Statuses.ElementAt(1).Schools.Select(s => s.Urn));
        Assert.Equal("green", educationalSupplies.Statuses.ElementAt(2).Status);
        Assert.Equal(["7", "10", "13", "16", "19"], educationalSupplies.Statuses.ElementAt(2).Schools.Select(s => s.Urn));
    }

    [Fact]
    public void WhenContainsRagRatingsByPriority()
    {
        var (viewModel, _) = BuildViewModel();

        var resultsByPriority = viewModel.RatingsByPriority.ToList();

        var red = resultsByPriority.ElementAt(0);
        Assert.Equal("red", red.Status);
        Assert.Equal(_categories.ElementAt(1), red.Categories.ElementAt(0).Category);
        Assert.Equal(["1"], red.Categories.ElementAt(0).Schools.Select(s => s.Urn));
        Assert.Equal(_categories.ElementAt(2), red.Categories.ElementAt(1).Category);
        Assert.Equal(["2"], red.Categories.ElementAt(1).Schools.Select(s => s.Urn));

        var amber = resultsByPriority.ElementAt(1);
        Assert.Equal("amber", amber.Status);
        Assert.Equal(_categories.ElementAt(0), amber.Categories.ElementAt(0).Category);
        Assert.Equal(["3"], amber.Categories.ElementAt(0).Schools.Select(s => s.Urn));
        Assert.Equal(_categories.ElementAt(1), amber.Categories.ElementAt(1).Category);
        Assert.Equal(["4"], amber.Categories.ElementAt(1).Schools.Select(s => s.Urn));

        var green = resultsByPriority.ElementAt(2);
        Assert.Equal("green", green.Status);
        Assert.Equal(_categories.ElementAt(2), green.Categories.ElementAt(0).Category);
        Assert.Equal(["5", "8", "11", "14", "17", "20"], green.Categories.ElementAt(0).Schools.Select(s => s.Urn));
        Assert.Equal(_categories.ElementAt(0), green.Categories.ElementAt(1).Category);
        Assert.Equal(["6", "9", "12", "15", "18"], green.Categories.ElementAt(1).Schools.Select(s => s.Urn));
        Assert.Equal(_categories.ElementAt(1), green.Categories.ElementAt(2).Category);
        Assert.Equal(["7", "10", "13", "16", "19"], green.Categories.ElementAt(2).Schools.Select(s => s.Urn));
    }

    private (TrustSpendingViewModel, TrustSchool[] schools) BuildViewModel(params string[] priorities)
    {
        var i = 0;
        var schools = _fixture
            .Build<TrustSchool>()
            .With(s => s.URN, () =>
            {
                i++;
                return i.ToString();
            })
            .CreateMany(20)
            .ToArray();

        var ratings = schools.Select(s =>
        {
            var j = int.Parse(s.URN!);
            return new RagRating
            {
                URN = s.URN,
                RAG = j < 3
                    ? "red"
                    : j < 5
                        ? "amber"
                        : "green",
                Category = _categories.ElementAt(j % 3)
            };
        });

        _trust.Schools = schools;
        return (new TrustSpendingViewModel(_trust, ratings, _categories, priorities), schools);
    }
}