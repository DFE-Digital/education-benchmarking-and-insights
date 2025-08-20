using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenASchoolComparisonItSpendViewModel
{
    private readonly Fixture _fixture = new();
    private readonly Random _random = new();

    [Fact]
    public void ShouldReturnValuesAndFilters()
    {
        var school = _fixture.Create<School>();
        var subCategories = _fixture.Create<SchoolComparisonSubCategoriesViewModel>();
        var expenditures = _fixture.Build<SchoolItSpend>().CreateMany().ToArray();

        var actual = new SchoolComparisonItSpendViewModel(school, subCategories, expenditures);

        Assert.Equal(school.URN, actual.Urn);
        Assert.Equal(school.SchoolName, actual.Name);
        Assert.Equal(subCategories, actual.SubCategories);
        Assert.Equal(SchoolComparisonItSpendViewModel.ViewAsOptions.Chart, actual.ViewAs);
        Assert.Equal(Dimensions.ResultAsOptions.SpendPerPupil, actual.ResultAs);
        Assert.Empty(actual.SelectedSubCategories);
    }

    [Fact]
    public void ShouldReturnFlattenedTooltipData()
    {
        var school = new School();
        var subCategories = _fixture.Create<SchoolComparisonSubCategoriesViewModel>();
        var expenditures = _fixture.Build<SchoolItSpend>().CreateMany().ToArray();

        var actual = new SchoolComparisonItSpendViewModel(school, subCategories, expenditures);

        Assert.NotEmpty(actual.TooltipData);
        for (var i = 0; i < expenditures.Length; i++)
        {
            var expenditure = expenditures[i];
            var tooltip = actual.TooltipData.ElementAt(i);

            Assert.Equal(expenditure.URN, tooltip.Urn);
            Assert.Equal(expenditure.SchoolName, tooltip.SchoolName);
            Assert.Equal(expenditure.LAName, tooltip.LAName);
            Assert.Equal(expenditure.TotalPupils, tooltip.TotalPupils);
            Assert.Equal(expenditure.PeriodCoveredByReturn, tooltip.PeriodCoveredByReturn);
        }
    }

    [Fact]
    public void ShouldReturnDistinctPartYearSchoolsAcrossSubCategories()
    {
        var school = new School();
        var expenditures = _fixture.Build<SchoolItSpend>()
            .With(x => x.PeriodCoveredByReturn, () => _random.Next(1, 12))
            .CreateMany(10)
            .ToArray();
        var subCategories = new SchoolComparisonSubCategoriesViewModel(string.Empty, expenditures, []);

        var actual = new SchoolComparisonItSpendViewModel(school, subCategories, expenditures);

        var partYear = subCategories
            .SelectMany(e => e.Data!.Where(d => d.PeriodCoveredByReturn is not 12))
            .DistinctBy(x => x.Urn)
            .ToArray();

        Assert.NotEmpty(actual.PartYearData);
        Assert.Equal(partYear.Length, actual.PartYearData.Length);
        foreach (var expected in partYear)
        {
            Assert.Contains(new SchoolChartPartYearData
            {
                SchoolName = expected.SchoolName,
                PeriodCoveredByReturn = expected.PeriodCoveredByReturn
            }, actual.PartYearData);
        }
    }
}