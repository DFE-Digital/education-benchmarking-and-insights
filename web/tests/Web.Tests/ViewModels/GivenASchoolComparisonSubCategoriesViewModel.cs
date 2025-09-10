using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenASchoolComparisonSubCategoriesViewModel
{
    private readonly Fixture _fixture = new();
    private readonly Random _random = new();

    [Fact]
    public void ShouldFlattenAllSubCategoriesForSchoolItSpend()
    {
        const string urn = nameof(urn);
        var expenditures = _fixture.Build<SchoolItSpend>()
            .With(s => s.PeriodCoveredByReturn, () => _random.Next(1, 12))
            .CreateMany()
            .ToArray();

        // 'current' school should still be included even if zero
        expenditures.ElementAt(0).URN = urn;
        expenditures.ElementAt(0).AdministrationSoftwareAndSystems = 0;

        // 'other' school should be excluded if zero and sub category flagged as `HasNegativeOrZeroValues`
        expenditures.ElementAt(1).Connectivity = 0;

        var actual = new SchoolComparisonSubCategoriesViewModel(urn, expenditures, ItSpendingCategories.All);

        Assert.Equal(7, actual.Count);
        AssertSubCategory(actual.ElementAt(0), "Administration software and systems E20D", urn, s => s.AdministrationSoftwareAndSystems, expenditures);
        AssertSubCategory(actual.ElementAt(1), "Connectivity E20A", urn, s => s.Connectivity, expenditures);
        AssertSubCategory(actual.ElementAt(2), "IT learning resources E20C", urn, s => s.ItLearningResources, expenditures);
        AssertSubCategory(actual.ElementAt(3), "IT support E20G", urn, s => s.ItSupport, expenditures);
        AssertSubCategory(actual.ElementAt(4), "Laptops, desktops and tablets E20E", urn, s => s.LaptopsDesktopsAndTablets, expenditures);
        AssertSubCategory(actual.ElementAt(5), "Onsite servers E20B", urn, s => s.OnsiteServers, expenditures);
        AssertSubCategory(actual.ElementAt(6), "Other hardware E20F", urn, s => s.OtherHardware, expenditures);
    }

    private static void AssertSubCategory(
        SchoolComparisonViewModelCostSubCategory<SchoolComparisonDatum> actual,
        string name,
        string urn,
        Func<SchoolItSpend, decimal?> selector,
        SchoolItSpend[] expenditures)
    {
        Assert.Null(actual.ChartSvg);
        Assert.Equal(name, actual.SubCategory);
        Assert.NotNull(actual.Uuid);

        var expected = expenditures
            .Select(e => new SchoolComparisonDatum
            {
                Urn = e.URN,
                SchoolName = e.SchoolName,
                Expenditure = selector(e),
                LAName = e.LAName,
                SchoolType = e.SchoolType,
                TotalPupils = e.TotalPupils,
                PeriodCoveredByReturn = e.PeriodCoveredByReturn
            })
            .Where(x => x.Urn == urn || x.Expenditure > 0);

        Assert.Equivalent(expected, actual.Data);
    }
}